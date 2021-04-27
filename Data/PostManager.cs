    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social.Model;

namespace Social.Data
{
    public sealed class PostManager  
    {
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
            _Posts.Add(post);

        }
        public void CreatePost(Post post)
        {
            _Posts.Add(post);
        }
        public void DeletePost(Post post)
        {
            _Posts.Remove(post);
        }

        public List<Post> ViewAllPost() { return _Posts; }
        public Post ViewPost(long postid)
        {
            return _Posts.Where(post => post.PostId == postid) as Post;
        }
        public List<Post> ViewMyPost(long userid)
        {
            return _Posts.Where(post => post.PostCreatedByUserId == userid).ToList();
        }
        public void AddComment(Post post, Comment comment)
        {
            //_Comments.Add(comment);
            post.Comments.Add(comment);
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
            comment.ParentCommentId = parentComment.CommentId;
            /*comment.Reply.Add(comment);*/
            /*post.Comments.Add(comment);*/
            /*post.Reply.Add(comment);*/
            Comment reply;
            reply = GetComment(post.Comments, parentComment);
            post.Comments.Remove(reply);
            reply.Reply.Add(comment);
           
            post.Comments.Add(reply);


        }        
        public void LikePost(Post post,User user)
        {
            if (!post.LikedId.Contains(user.UserId))
            {
                ++post.Likes;
                post.LikedId.Add(user.UserId);
            }
        }
        public Post Edit(Post post)
        {

            Post newPost = new Post(post.PostTitle, post.PostContent, post.PostCreatedByUserName, post.PostCreatedByUserId);
            newPost.CreatedTime = post.CreatedTime;
            newPost.PostId = post.PostId;
            newPost.Likes = post.Likes;
            newPost.Comments = post.Comments;
            newPost.LikedId = post.LikedId;
            return newPost;
        }
        public void RemoveComment(Post post,Comment comment)
        {
            post.Comments.Remove(comment);
        }
       
    }

}
