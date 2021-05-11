    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social.Model;

namespace Social.Data
{
    public sealed class PostManager  
    {
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
        SQLite.Net.SQLiteConnection conn;



        public void  CreateTable()
        {
           conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Post>();
            conn.CreateTable<Comment>();
            conn.CreateTable<UserIds>();

        }

        private List<Post> _Posts = new List<Post>();
        private static PostManager _Instance = null;
        private PostManager() { }
        
        public static PostManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new PostManager();

            }
            return _Instance;
        }
        public void AddPost(Post post)
        {
           
            conn.Insert(post);
           // _Posts.Add(post);

        }
       /* public void CreatePost(Post post)
        {
           
            _Posts.Add(post);
        }*/
        public void DeletePost(Post post)
        {
            conn.Delete(post);
            //_Posts.Remove(post);
        }

        public List<Post> ViewAllPost()
        {

            _Posts.Clear();
            var query = conn.Table<Post>();
            var commentquary = conn.Table<Comment>();
            var likedidquery = conn.Table<UserIds>();
            
            foreach (var user in query)
            {
               foreach(var comment in commentquary)
                {
                    foreach(var reply in commentquary)
                    {
                        if (reply.ParentCommentId == comment.CommentId)
                            comment.Reply.Add(reply);
                    }

                    if (comment.PostId == user.PostId && comment.ParentCommentId==null)
                        user.Comments.Add(comment);

                }
               foreach(var ids in likedidquery)
                {
                    if (user.PostId == ids.PostId)
                        user.LikedId.Add(ids.Userid);
                }
                    
                _Posts.Add(user);
            }
             
           
            return _Posts;
            

            
        
        }
        public Post ViewPost(long postid)
        {
            return ViewAllPost().Where(post => post.PostId == postid) as Post;
        }
        public List<Post> ViewMyPost(long userid)
        {
            return ViewAllPost().Where(post => post.PostCreatedByUserId == userid).ToList();
        }
        public void AddComment(Post post, Comment comment)
        {
            //_Comments.Add(comment);

            conn.Insert(comment);
            post.Comments.Add(comment);
            post.CommentCount = post.CommentCount + 1;
            conn.Update(post);
        }
        public List<Comment> ViewPostComment(Post post, long postid)
        {
            return post.Comments.Where(comments => comments.PostId == postid).ToList();
        }
        public Comment GetComment(List<Comment> comments,Comment ParentComment)
        {
            //return comments.Where(_comment => _comment.CommentId == ParentComment.CommentId) as Comment;
            foreach(var i in comments)
            {
                if (i.CommentId == ParentComment.CommentId)
                    return i;
            }
            return ParentComment;
        }
        public void AddReply(Post post, Comment parentComment ,Comment comment)
        {
            // comment.ParentCommentId = parentComment.CommentId;

            Comment reply;
             reply = GetComment(post.Comments, parentComment);
            post.Comments.Remove(reply);
            // conn.Delete(parentComment);

            reply.Reply.Add(comment);
            post.CommentCount = post.CommentCount + 1;
            post.Comments.Add(reply);
            //parentComment.Reply.Add(comment);
            conn.Insert(comment);
            conn.Update(post);

            /*{comment.Reply.Add(comment);
            post.Comments.Add(comment);
            post.Reply.Add(comment);*/


        }
        public void LikePost(Post post,User user)
        {

            if (!post.LikedId.Contains(user.UserId))
            {
                post.Likes++;
                UserIds userIds = new UserIds();
                userIds.PostId = post.PostId;
                userIds.Userid = user.UserId;
                userIds.UserName = user.UserName;
                conn.Insert(userIds);
                post.LikedId.Add(user.UserId);
            }
            conn.Update(post);
        }
        public void UnLikePost(Post post, User user)
        {
            var ids = conn.Table<UserIds>();
            foreach (var id in ids)
            {

                if (post.LikedId.Contains(id.Userid))
                {
                    
                    post.Likes--;
                    post.LikedId.Remove(user.UserId);
                    conn.Update(post);
                    break;



                }

               
            }
        }
        public Post Edit(Post post)
        {

            //Post newPost = new Post(post.PostTitle, post.PostContent, post.PostCreatedByUserName, post.PostCreatedByUserId);
            /*Post newPost = post;
            newPost.CreatedTime = post.CreatedTime;
            newPost.PostId = post.PostId;
            newPost.Likes = post.Likes;
            newPost.Comments = post.Comments;
            newPost.LikedId = post.LikedId;*/
            return post;
        }
        public void UpdateEdit(Post post)
        {
            conn.Insert(post);
            //conn.Update(post);
        }
        public void RemoveComment(Post post,Comment comment)
        {
            conn.Delete(comment);
            post.Comments.Remove(comment);
        }
        public void DeleteRecord()
        {
            conn.DropTable<Post>();
            conn.DropTable<Comment>();
            conn.CreateTable<Post>();
            conn.CreateTable<Comment>();
        }
       
    }

}
