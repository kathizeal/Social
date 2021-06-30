using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Social.Model;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Social.Data
{
    public sealed class UserManager
    {
        private User _CurrentUser;
        private static UserManager _Instance = null;
        private List<User> _UserLists = new List<User>();
        string path;
        SQLite.Net.SQLiteConnection conn;
        public void CreateTable()
        {

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
               "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new
               SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();
            conn.CreateTable<Chat>();
        }
        private UserManager() { }
        public static UserManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new UserManager();

            }
            return _Instance;
        }
        public User CurrentUser
        {
            get
            {
                return _CurrentUser;
            }
            private set
            {
                _CurrentUser = value;
            }
        }
        public List<User> UsersLists()
        {
            var query = conn.Table<User>();
            foreach (var user in query)
            {
                _UserLists.Add(user);
            }
            return _UserLists;
        }
        public void AddUser(string username, string lastname, string email, string password, string birthday, string gender)
        {
            CreateTable();
            User newUser = new User();
            newUser.UserName = username;
            newUser.BirthDay = birthday;
            newUser.LastName = lastname;
            newUser.Password = password;
            newUser.Gender = gender;
            newUser.Email = email;
            conn.Insert(newUser);

        }
        public bool LoginUser(string username, string password)
        {
            foreach (var user in _UserLists)
            {
                if (user.UserName == username && user.Password == password)
                {
                    CurrentUser = user;
                    return true;
                }

            }
            return false;
        }
        public User FindUser(long id)
        {
            foreach (var user in UsersLists())
            {
                if (user.UserId == id)
                    return user;

            }
            return null ;
        }
        public bool State()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            if (value == null)
                return false;
            else
                return true;
        }
        public void SignedUser(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            ApplicationData.Current.LocalSettings.Values["UserClass"] = json;

        }
        public void SignOut()
        {
            _CurrentUser = Current();
            _CurrentUser.LogoutTime = DateTime.UtcNow;
            conn.Update(_CurrentUser);
            ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
            _CurrentUser = null;
        }
        public string ProfilePic(User currentuser)
        {
            PostManager postManager = PostManager.GetInstance();
            var posts = postManager.ViewMyPost(currentuser.UserId);
            if (posts.Count != 0)
                return posts[0].ProfilePic;
            else
                return currentuser.ProfilePic;
        }
        public User Current()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());
            _CurrentUser = user;
            return user;
        }
        public User Find(long id)
        {
            conn.CreateTable<User>();
            var userlist = conn.Table<User>();
            foreach (var user in userlist)
            {
                if (user.UserId == id)
                    return user;

            }
            return null;

        }
        public void Update(User user)
        {
            var _user = conn.Table<User>();
            foreach(var i in _user)
            {
                if(i.UserId==user.UserId)
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
        public void DeleteUserRecord()
        {
            conn.DropTable<User>();
            conn.DropTable<Chat>();
            conn.CreateTable<User>();
            conn.CreateTable<Chat>();
        }
        public Image UpateProfile(User user, Profile profile)
        { 
            string pic = "ms-appx:///Assets/" + user.UserName + ".jpg";
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(pic));
            foreach (var us in conn.Table<User>())
            {
                if (us.UserId == user.UserId)
                {
                    us.ProfilePic = profile.ImageFile;
                    Update(us);
                }
            }
          
            return img;
           
        }
        public List<User> ALLUsersLists()
        {
            List<User> users = new List<User>();
            var query = conn.Table<User>();
            foreach (var user in query)
            {
                if (user.UserId != Current().UserId)
                    users.Add(user);
            }

            return users;
        }
        public void CreateChat(User Sender,User Receiever)
        {
            Chat chat = new Chat();
            chat.SenderId = Sender.UserId;
            chat.SenderName = Sender.UserName;
            chat.RecieverId = Receiever.UserId;
            chat.RecieverName = Receiever.UserName;
            chat.ProfilePic = Sender.ProfilePic;
            chat.Msg = "Hi " + Receiever.UserName;
            conn.Insert(chat);
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
        public ObservableCollection<Chat> Message(User Sender, User Receiever)
        {
            ObservableCollection<Chat> CurrentChat = new ObservableCollection<Chat>();
            var chats = conn.Table<Chat>();
            foreach(var chat in chats )
            {
                if((chat.SenderId==Sender.UserId&&chat.RecieverId==Receiever.UserId)||(chat.SenderId == Receiever.UserId && chat.RecieverId == Sender.UserId))
                {
                    CurrentChat.Add(chat);
                }
            }
            return CurrentChat;
        }
        public void AddChat(Chat chat)
        {
            conn.Insert(chat);
        }
        public ObservableCollection<User> DateChange(ObservableCollection<User> UserList)
        {
            DateTime date = DateTime.UtcNow;
            ObservableCollection<User> Users = UserList;
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
                                        user.LogOutTimeString = "Last seen " + diff +  " minutes ago";
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
                            user.LogOutTimeString = "Last seen "+ diff + " month ago";
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

    }
}
