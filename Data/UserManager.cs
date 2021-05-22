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
            // _UserLists.Add(newUser);

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
        public void Logout()
        {
            _CurrentUser = null;
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
            ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
            _CurrentUser = null;
        }
        public string ProfilePic(User currentuser)
        {
            var userlist = conn.Table<User>();
            foreach (var user in userlist)
            {
                if (user.UserId == currentuser.UserId)
                    return user.ProfilePic;

            }
            return currentuser.ProfilePic; 
        }
        public User Current()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());
            
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
                    // newUser.ProfilePicSource = user.ProfilePicSource;
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
            conn.CreateTable<User>();
        }
        public Image UpateProfile(User user, Profile profile)
        {
            string pic = "ms-appx:///Assets/" + user.UserName + ".jpg";
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(pic));
            user.ProfilePic = profile.ImageFile;
            Update(user);
            return img;
           
        }
       



    }
}
