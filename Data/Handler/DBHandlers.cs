using Newtonsoft.Json;
using Social.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
        public Post LikePost(Post post, User user)
        {
            if (!post.LikedId.Contains(user.UserId))
            {
                post.Likes++;
                UserIds userIds = new UserIds
                {
                    PostId = post.PostId,
                    Userid = user.UserId,
                    UserName = user.UserName,
                    Unique = DateTime.Now.Ticks
                };
                conn.Insert(userIds);
                post.LikedId.Add(user.UserId);
            }
            conn.Update(post);
            return post;
        }
        public Post UnLikePost(Post post, User user)
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
            return post;
        }
        public Comment AddComments(Post post, Comment comment)
        {
            DateTime date = DateTime.UtcNow;
            if (comment.CreatedTime.Year == date.Year)
            {
                if (comment.CreatedTime.Month == date.Month)
                {
                    if (comment.CreatedTime.Date == date.Date)
                    {
                        if (comment.CreatedTime.Hour == date.Hour)
                        {
                            if (comment.CreatedTime.Minute == date.Minute)
                                comment.CreatedTimeString = "          Just now";
                            else
                            {
                                int diff = date.Minute - comment.CreatedTime.Minute >= 0 ? date.Minute - comment.CreatedTime.Minute : comment.CreatedTime.Minute - date.Minute;
                                if (diff == 1)
                                    comment.CreatedTimeString = "a minute ago";
                                else
                                {
                                    comment.CreatedTimeString = diff + " minutes ago";
                                }


                            }
                        }
                        else
                        {
                            int diff = date.Hour - comment.CreatedTime.Hour >= 0 ? date.Hour - comment.CreatedTime.Hour : comment.CreatedTime.Hour - date.Hour;
                            if (diff == 1)
                                comment.CreatedTimeString = "1 hour ago";
                            else
                                comment.CreatedTimeString = diff + " hours ago";
                        }
                    }
                    else
                    {
                        int diff = date.Day - comment.CreatedTime.Day >= 0 ? date.Day - comment.CreatedTime.Day : comment.CreatedTime.Day - date.Day;
                        if (diff == 1)
                           comment.CreatedTimeString = "Yesterday";
                        else
                        {
                            comment.CreatedTimeString = comment.CreatedTime.ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                {
                    int diff = date.Month - comment.CreatedTime.Month >= 0 ? date.Month - comment.CreatedTime.Month : comment.CreatedTime.Month - date.Month;
                    if (diff == 1)
                        comment.CreatedTimeString = "1 month ago";
                    else
                    {
                        comment.CreatedTimeString = diff + " month ago";
                    }
                }
            }
            else
            {
                int diff = date.Year - comment.CreatedTime.Year >= 0 ? date.Year - comment.CreatedTime.Year : comment.CreatedTime.Year - date.Year;
                if (diff == 1)
                    comment.CreatedTimeString = "1 year ago";
                else
                {
                    comment.CreatedTimeString = diff + " year ago";
                }
            }
            conn.Insert(comment);
            post.Comments.Add(comment);
            post.CommentCount = post.CommentCount + 1;
            conn.Update(post);
            return comment;
        }
        public Comment GetComment(long CommentId)
        {
            var comments = conn.Table<Comment>();
            Comment comment1 = new Comment();
            foreach (var i in comments)
            {
                if (i.CommentId == CommentId)
                    return i;
            }
            return comment1;
        }
        public List<Comment> GetReply(long ParentId)
        {
            var comments = conn.Table<Comment>();
            List<Comment> replys = new List<Comment>();
            foreach (var comment in comments)
            {
                if (comment.ParentCommentId == ParentId)
                    replys.Add(comment);
            }
            return DateChangeComment(replys);
        }
        public Comment AddReply(Comment comment)
        {
            DateTime date = DateTime.UtcNow;
            if (comment.CreatedTime.Year == date.Year)
            {
                if (comment.CreatedTime.Month == date.Month)
                {
                    if (comment.CreatedTime.Date == date.Date)
                    {
                        if (comment.CreatedTime.Hour == date.Hour)
                        {
                            if (comment.CreatedTime.Minute == date.Minute)
                                comment.CreatedTimeString = "          Just now";
                            else
                            {
                                int diff = date.Minute - comment.CreatedTime.Minute >= 0 ? date.Minute - comment.CreatedTime.Minute : comment.CreatedTime.Minute - date.Minute;
                                if (diff == 1)
                                    comment.CreatedTimeString = "a minute ago";
                                else
                                {
                                    comment.CreatedTimeString = diff + " minutes ago";
                                }


                            }
                        }
                        else
                        {
                            int diff = date.Hour - comment.CreatedTime.Hour >= 0 ? date.Hour - comment.CreatedTime.Hour : comment.CreatedTime.Hour - date.Hour;
                            if (diff == 1)
                                comment.CreatedTimeString = "1 hour ago";
                            else
                                comment.CreatedTimeString = diff + " hours ago";
                        }
                    }
                    else
                    {
                        int diff = date.Day - comment.CreatedTime.Day >= 0 ? date.Day - comment.CreatedTime.Day : comment.CreatedTime.Day - date.Day;
                        if (diff == 1)
                            comment.CreatedTimeString = "Yesterday";
                        else
                        {
                            comment.CreatedTimeString = comment.CreatedTime.ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                {
                    int diff = date.Month - comment.CreatedTime.Month >= 0 ? date.Month - comment.CreatedTime.Month : comment.CreatedTime.Month - date.Month;
                    if (diff == 1)
                        comment.CreatedTimeString = "1 month ago";
                    else
                    {
                        comment.CreatedTimeString = diff + " month ago";
                    }
                }
            }
            else
            {
                int diff = date.Year - comment.CreatedTime.Year >= 0 ? date.Year - comment.CreatedTime.Year : comment.CreatedTime.Year - date.Year;
                if (diff == 1)
                    comment.CreatedTimeString = "1 year ago";
                else
                {
                    comment.CreatedTimeString = diff + " year ago";
                }
            }
            conn.Insert(comment);
            return comment;
        }
        public Post DeletePost(Post post)
        {
            conn.Delete(post);
            return post;
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
                foreach (var comment in commentquary)
                {
                    if (comment.PostId == user.PostId && comment.ParentCommentId == null)
                        user.Comments.Add(comment);
                }
                foreach (var ids in likedidquery)
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
        public List<Post> ViewMyPost(long userid)
        {
            return ViewAllPost().Where(post => post.PostCreatedByUserId == userid).ToList();
        }
        public Post ViewPost(long postid)
        {
            var Posts = conn.Table<Post>();
            foreach (var post in Posts)
            {
                if (post.PostId == postid)
                    return post;
            }
            return ViewAllPost().Where(post => post.PostId == postid) as Post;
        }
        public User Current()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());
           
            return user;
        }
        public List<User> GetUsersLists()
        {
            List<User> users = new List<User>();
            var query = conn.Table<User>();
            foreach (var user in query)
            {
                if (user.UserId != Current().UserId)
                    users.Add(user);
            }

            return DateChange(users);
        }
        public List<User> DateChange(List<User> UserList)
        {
            DateTime date = DateTime.UtcNow;
            List<User> Users = UserList;
            foreach (var user in Users)
            {
                if (user.LogoutTime.Year == date.Year)
                {
                    if (user.LogoutTime.Month == date.Month)
                    {
                        if (user.LogoutTime.Date == date.Date)
                        {
                            if (user.LogoutTime.Hour == date.Hour)
                            {
                                if (user.LogoutTime.Minute == date.Minute)
                                    user.LogOutTimeString = " Last seen just now";
                                else
                                {
                                    int diff = date.Minute - user.LogoutTime.Minute >= 0 ? date.Minute - user.LogoutTime.Minute : user.LogoutTime.Minute - date.Minute;
                                    if (diff == 1)
                                        user.LogOutTimeString = "Last seen a minute ago";
                                    else
                                    {
                                        user.LogOutTimeString = "Last seen " + diff + " minutes ago";
                                    }
                                }
                            }
                            else
                            {
                                int diff = date.Hour - user.LogoutTime.Hour >= 0 ? date.Hour - user.LogoutTime.Hour : user.LogoutTime.Hour - date.Hour;
                                if (diff == 1)
                                    user.LogOutTimeString = "Last seen 1 hour ago";
                                else
                                    user.LogOutTimeString = "Last seen " + diff + " hours ago";
                            }
                        }
                        else
                        {
                            int diff = date.Day - user.LogoutTime.Day >= 0 ? date.Day - user.LogoutTime.Day : user.LogoutTime.Day - date.Day;
                            if (diff == 1)
                                user.LogOutTimeString = "Last seen Yesterday";
                            else
                            {
                                user.LogOutTimeString = "Last seen " + user.LogoutTime.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                    else
                    {
                        int diff = date.Month - user.LogoutTime.Month >= 0 ? date.Month - user.LogoutTime.Month : user.LogoutTime.Month - date.Month;
                        if (diff == 1)
                            user.LogOutTimeString = "Last seen 1 month ago";
                        else
                        {
                            user.LogOutTimeString = "Last seen " + diff + " month ago";
                        }
                    }
                }
                else
                {
                    int diff = date.Year - user.LogoutTime.Year >= 0 ? date.Year - user.LogoutTime.Year : user.LogoutTime.Year - date.Year;
                    if (diff == 1)
                        user.LogOutTimeString = "Last seen 1 year ago";
                    else
                    {
                        user.LogOutTimeString = "Last seen " + diff + " year ago";
                    }
                }

            }
            return Users;

        }
        public string GetProfilePic(User currentuser)
        {
           
            var posts = ViewMyPost(currentuser.UserId);
            if (posts.Count != 0)
                return posts[0].ProfilePic;
            else
                return currentuser.ProfilePic;
        }
        public void Update(User user)
        {
            var _user = conn.Table<User>();
            foreach (var i in _user)
            {
                if (i.UserId == user.UserId)
                {
                    i.ProfilePic = user.ProfilePic;
                    i.UserName = user.UserName;
                    i.BirthDay = user.BirthDay;
                    i.LastName = user.LastName;
                    i.Password = user.Password;
                    i.Gender = user.Gender;
                    i.Email = user.Email;
                    i.UserId = user.UserId;
                    conn.Update(i);
                    break;
                }
            }
        }
        public string UpdateProfilePic(User user, Profile profile)
        {
            string pic = "ms-appx:///Assets/" + user.UserName + ".jpg";
            foreach (var us in conn.Table<User>())
            {
                if (us.UserId == user.UserId)
                {
                    us.ProfilePic = profile.ImageFile;
                    Update(us);
                }
            }
            user.ProfilePic = profile.ImageFile;
            foreach (var post in ViewMyPost(user.UserId))
            {
                post.ProfilePic = profile.ImageFile;
                var comments = conn.Table<Comment>();
                foreach (var comment in comments)
                {
                    if (comment.PostId == post.PostId)
                    {
                        comment.ProfilePic = user.ProfilePic;
                        conn.Update(comment);
                    }
                }
                conn.Update(post);
            }

            return pic;

        }
        public List<Chat> Message(User Sender, User Receiever)
        {
            List<Chat> CurrentChat = new List<Chat>();
            var chats = conn.Table<Chat>();
            foreach (var chat in chats)
            {
                if ((chat.SenderId == Sender.UserId && chat.RecieverId == Receiever.UserId) || (chat.SenderId == Receiever.UserId && chat.RecieverId == Sender.UserId))
                {
                    CurrentChat.Add(chat);
                }
            }
            return CurrentChat;
        }
        public bool CheckExist(User Sender, User Receiever)
        {
            var chats = conn.Table<Chat>();
            foreach (var chat in chats)
            {
                if ((chat.SenderId == Sender.UserId && chat.RecieverId == Receiever.UserId) || (chat.SenderId == Receiever.UserId && chat.RecieverId == Sender.UserId))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
