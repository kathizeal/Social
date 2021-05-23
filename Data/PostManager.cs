    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social.Model;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
        }
        public void DeletePost(Post post)
        {
            conn.Delete(post);
        }
        public List<Post> ViewAllPost()
        {
            List<Post> _Posts = new List<Post>();
            _Posts.Clear();
            var query = conn.Table<Post>();
            var commentquary = conn.Table<Comment>();
            var likedidquery = conn.Table<UserIds>();
            foreach (var user in query)
            {
               foreach(var comment in commentquary)
                {
                   /* foreach(var reply in commentquary)
                    {
                        if (reply.ParentCommentId == comment.CommentId)
                            comment.Reply.Add(reply);
                    }*/
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
            DateTime date = DateTime.UtcNow;

            foreach (var post in _Posts)
            {
                if (post.CreatedTime.Year == date.Year)
                {
                    if (post.CreatedTime.Month == date.Month)
                    {
                        if (post.CreatedTime.Date == date.Date)
                        {
                            if (post.CreatedTime.Hour == date.Hour)
                            {
                                if (post.CreatedTime.Minute == date.Minute)
                                    post.CreatedTimeString = "Just now";
                                else
                                {
                                    int diff = date.Minute - post.CreatedTime.Minute >= 0 ? date.Minute - post.CreatedTime.Minute : post.CreatedTime.Minute - date.Minute;
                                    if (diff == 1)
                                        post.CreatedTimeString = "one minute ago";
                                    else
                                    {
                                        post.CreatedTimeString = diff + " minutes ago";
                                    }


                                }
                            }
                            else
                            {
                                int diff = date.Hour - post.CreatedTime.Hour >= 0 ? date.Hour - post.CreatedTime.Hour : post.CreatedTime.Hour - date.Hour;
                                if (diff == 1)
                                    post.CreatedTimeString = "1 hour ago";
                                else
                                    post.CreatedTimeString = diff + " hours ago";
                            }
                        }
                        else
                        {
                            int diff = date.Day - post.CreatedTime.Day >= 0 ? date.Day - post.CreatedTime.Day : post.CreatedTime.Day - date.Day;
                            if (diff == 1)
                                post.CreatedTimeString = "Yesterday";
                            else
                            {
                                post.CreatedTimeString = post.CreatedTime.ToShortDateString();
                            }
                        }
                    }
                    else
                    {
                        int diff = date.Month - post.CreatedTime.Month >= 0 ? date.Month - post.CreatedTime.Month : post.CreatedTime.Month - date.Month;
                        if (diff == 1)
                            post.CreatedTimeString = "1 month ago";
                        else
                        {
                            post.CreatedTimeString = diff + " month ago";
                        }
                    }
                }
                else
                {
                    int diff = date.Year - post.CreatedTime.Year >= 0 ? date.Year - post.CreatedTime.Year : post.CreatedTime.Year - date.Year;
                    if (diff == 1)
                        post.CreatedTimeString = "1 year ago";
                    else
                    {
                        post.CreatedTimeString = diff + " year ago";
                    }
                }

            }
            
            return _Posts;
        }
        public Post ViewPost(long postid)
        {
            var Posts = conn.Table<Post>();
            foreach(var post in Posts)
            {
                if (post.PostId == postid)
                    return post;
            }
            return ViewAllPost().Where(post => post.PostId == postid) as Post;
        }
        public List<Post> ViewMyPost(long userid)
        {
            return ViewAllPost().Where(post => post.PostCreatedByUserId == userid).ToList();
        }
        public void AddComment(Post post, Comment comment)
        {
            conn.Insert(comment);
            post.Comments.Add(comment);
            post.CommentCount = post.CommentCount + 1;
            conn.Update(post);
        }
        public List<Comment> ViewPostComment(Post post, long postid)
        {
            return post.Comments.Where(comments => comments.PostId == postid).ToList();
        }
        public Comment GetComment(long CommentId)
        {
            var comments = conn.Table<Comment>();
            Comment comment1= new Comment();
            foreach(var i in comments)
            {
                if (i.CommentId == CommentId)
                    return i;
            }
            return comment1;
        }
       
        public ObservableCollection<Comment> GetReply(long ParentId)
        {
            var comments = conn.Table<Comment>();
            ObservableCollection<Comment> replys = new ObservableCollection<Comment>();
            foreach(var comment in comments)
            {
                if (comment.ParentCommentId == ParentId)
                    replys.Add(comment);
            }
            return replys;
        }
       public void AddReply(Post post, Comment parentComment ,Comment comment)
        {
            conn.Insert(comment);
            conn.Update(post);
        }
       
        public void LikePost(Post post,User user)
        {
            if (!post.LikedId.Contains(user.UserId))
            {
                post.Likes++;
                UserIds userIds = new UserIds
                {
                    PostId = post.PostId,
                    Userid = user.UserId,
                    UserName = user.UserName,
                    Unique= DateTime.Now.Ticks


                };
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
                if (id.PostId == post.PostId)
                {
                    if (id.Userid == user.UserId)
                    {
                        post.Likes--;
                        conn.Delete(id);
                        post.LikedId.Remove(user.UserId);
                    }
                }
            }

           
            conn.Update(post);
        }
        public void UpdateEdit(Post post)
        {
            conn.Insert(post);
        }
        public void RemoveComment(Post post,Comment comment)
        {
            conn.Delete(comment);
            post.CommentCount--;
            conn.Update(post);
            post.Comments.Remove(comment);
        }
        public void DeleteRecord()
        {
            conn.DropTable<Post>();
            conn.DropTable<Comment>();
            conn.DropTable<UserIds>();
            conn.CreateTable<Post>();
            conn.CreateTable<Comment>();
            conn.CreateTable<UserIds>();
        }
        public ObservableCollection<UserIds> LikedUsers(Post post)
        {
            ObservableCollection<UserIds> userIds = new ObservableCollection<UserIds>();
            var ids = conn.Table<UserIds>();
            foreach (var id in ids)
            {
                if (id.PostId==post.PostId)
                {
                    userIds.Add(id);
                }
            }
            return userIds;

        }
        public ObservableCollection<Post> DateChange(ObservableCollection<Post> PostList)
        {
           // TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
             //DateTime date= TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localZoneId);
            DateTime date = DateTime.UtcNow;
            ObservableCollection<Post> posts = PostList;
           
            foreach (var post in PostList)
            {
                if (post.CreatedTime.Year == date.Year)
                {
                    if (post.CreatedTime.Month == date.Month)
                    {
                        if (post.CreatedTime.Date == date.Date)
                        {
                            if (post.CreatedTime.Hour == date.Hour)
                            {
                                if (post.CreatedTime.Minute == date.Minute)
                                    post.CreatedTimeString = "          Just now";
                                else
                                {
                                    int diff = date.Minute - post.CreatedTime.Minute >= 0 ? date.Minute - post.CreatedTime.Minute : post.CreatedTime.Minute - date.Minute;
                                    if (diff == 1)
                                        post.CreatedTimeString = "a minute ago";
                                    else
                                    {
                                        post.CreatedTimeString =diff + " minutes ago";
                                    }


                                }
                            }
                            else
                            {
                                int diff = date.Hour - post.CreatedTime.Hour >= 0 ? date.Hour - post.CreatedTime.Hour : post.CreatedTime.Hour - date.Hour;
                                if (diff == 1)
                                    post.CreatedTimeString = "1 hour ago";
                                else
                                    post.CreatedTimeString = diff + " hours ago";
                            }
                        }
                        else
                        {
                            int diff = date.Day - post.CreatedTime.Day >= 0 ? date.Day - post.CreatedTime.Day : post.CreatedTime.Day - date.Day;
                            if (diff ==1)
                                post.CreatedTimeString= "Yesterday";
                            else
                            {
                               post.CreatedTimeString=post.CreatedTime.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                    else
                    {
                        int diff = date.Month - post.CreatedTime.Month >= 0 ? date.Month - post.CreatedTime.Month : post.CreatedTime.Month - date.Month;
                        if (diff== 1)
                            post.CreatedTimeString = "1 month ago";
                        else
                        {
                            post.CreatedTimeString = diff + " month ago";
                        }
                    }
                }
                else
                {
                    int diff = date.Year - post.CreatedTime.Year >= 0 ? date.Year - post.CreatedTime.Year : post.CreatedTime.Year - date.Year;
                    if (diff == 1)
                        post.CreatedTimeString = "1 year ago";
                    else
                    {
                        post.CreatedTimeString = diff + " year ago";
                    }
                }

            }
            return posts;

        }

        public ObservableCollection<Comment> DateChangeComment(ObservableCollection<Comment> CommentList)
        {
            // TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            //DateTime date= TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localZoneId);
            DateTime date = DateTime.UtcNow;
            ObservableCollection<Comment> posts = CommentList;

            foreach (var post in posts)
            {

                if (post.CreatedTime.Year == date.Year)
                {
                    if (post.CreatedTime.Month == date.Month)
                    {
                        if (post.CreatedTime.Date == date.Date)
                        {
                            if (post.CreatedTime.Hour == date.Hour)
                            {
                                if (post.CreatedTime.Minute == date.Minute)
                                    post.CreatedTimeString = "Just now";
                                else
                                {
                                    int diff = date.Minute - post.CreatedTime.Minute >= 0 ? date.Minute - post.CreatedTime.Minute : post.CreatedTime.Minute - date.Minute;
                                    if (diff == 1)
                                        post.CreatedTimeString = "a minute ago";
                                    else
                                    {
                                        post.CreatedTimeString = diff + " minutes ago";
                                    }


                                }
                            }
                            else
                            {
                                int diff = date.Hour - post.CreatedTime.Hour >= 0 ? date.Hour - post.CreatedTime.Hour : post.CreatedTime.Hour - date.Hour;
                                if (diff == 1)
                                    post.CreatedTimeString = "1 hour ago";
                                else
                                    post.CreatedTimeString = diff + " hours ago";
                            }
                        }
                        else
                        {
                            int diff = date.Day - post.CreatedTime.Day >= 0 ? date.Day - post.CreatedTime.Day : post.CreatedTime.Day - date.Day;
                            if (diff == 1)
                                post.CreatedTimeString = "Yesterday";
                            else
                            {
                                post.CreatedTimeString = post.CreatedTime.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                    else
                    {
                        int diff = date.Month - post.CreatedTime.Month >= 0 ? date.Month - post.CreatedTime.Month : post.CreatedTime.Month - date.Month;
                        if (diff == 1)
                            post.CreatedTimeString = "1 month ago";
                        else
                        {
                            post.CreatedTimeString = diff + " month ago";
                        }
                    }
                }
                else
                {
                    int diff = date.Year - post.CreatedTime.Year >= 0 ? date.Year - post.CreatedTime.Year : post.CreatedTime.Year - date.Year;
                    if (diff == 1)
                        post.CreatedTimeString = "1 year ago";
                    else
                    {
                        post.CreatedTimeString = diff + " year ago";
                    }
                }
               // if(post.Reply.Count!=0)
               // post.Reply = DateChangeComment(post.Reply);
            }

            return posts;

        }
        
        public void UpdateProfile(User user,Profile profile)
        {
            string pic = "ms-appx:///Assets/" + user.UserName + ".jpg";
            user.ProfilePic = profile.ImageFile;
            foreach (var post in ViewMyPost(user.UserId))
            {
                post.ProfilePic = profile.ImageFile;
                var comments = conn.Table<Comment>();
                foreach(var comment in comments)
                {
                    if(comment.PostId==post.PostId)
                    {
                        comment.ProfilePic = user.ProfilePic;
                        conn.Update(comment);
                    }
                }
                conn.Update(post);
            }
        }
    }
}
