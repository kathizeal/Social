using Newtonsoft.Json;
using Social.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Social.Data.Handler
{
    public class DBHandlers
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        private static DBHandlers _Instance = null;
        public DBHandlers()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
              "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new
               SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();
            conn.CreateTable<Chat>();
            conn.CreateTable<Post>();
        }
        public static DBHandlers GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new DBHandlers();
            }
            return _Instance;
        }
        public List<Post> GetPosts()
        {
            var commentquary = conn.Table<Comment>();
            var likedidquery = conn.Table<UserIds>();
            DBHandlers.GetInstance();
            List<Post> PostList = new List<Post>();
            var query = conn.Table<Post>();
            foreach (var post in query)
            {
                foreach (var comment in commentquary)
                {
                    if (comment.PostId == post.PostId && comment.ParentCommentId == null)
                        post.Comments.Add(comment);
                }
                foreach (var ids in likedidquery)
                {
                    if (post.PostId == ids.PostId)
                        post.LikedId.Add(ids.Userid);
                }
                post.Comments = DateChangeComment(post.Comments);
                PostList.Add(post);
            }
            return ChangePostDate(PostList);
        }
        public Post AddorUpdatePost(Post post)
        {
            DBHandlers.GetInstance();
            DateTime date = DateTime.UtcNow;
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
            conn.Insert(post);
            return post;
        }
        public List<Post> ChangePostDate(List<Post> posts)
        {
            
            DateTime date = DateTime.UtcNow;
            
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
                                    post.CreatedTimeString = "          Just now";
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
            }
            return posts;

        }
        public List<Comment> DateChangeComment(List<Comment> CommentList)
        {

            DateTime date = DateTime.UtcNow;
            List<Comment> posts = CommentList;

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
            }

            return posts;

        }
        public User GetCurrentUser()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());
            //_CurrentUser = user;
            return user;
        }
        public List<UserIds> LikedUsers(Post post)
        {
            List<UserIds> userIds = new List<UserIds>();
            var ids = conn.Table<UserIds>();
            foreach (var id in ids)
            {
                if (id.PostId == post.PostId)
                {
                    userIds.Add(id);
                }
            }
            return userIds;

        }

    }
}
