using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Social.Model;
using Windows.Storage;

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
            foreach( var user in query)
            {
                _UserLists.Add(user);
            }
            
            return _UserLists;
        }
        public void AddUser(string username,string lastname,string email, string password,string birthday,string gender)
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
                if (user.UserName ==username  && user.Password == password)
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
        public string FindUser(long id)
        {
            
            foreach(var user in UsersLists())
            {
                if (user.UserId == id)
                    return user.UserName;
                
            }
            return "No User found";
        }
       
        public bool State()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            if (value==null)
            return false;
            else 
            return true;
        }

        public void  SignedUser(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            ApplicationData.Current.LocalSettings.Values["UserClass"] = json;
           
            
        }
        public void SignOut()
        {
            ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
            _CurrentUser = null;
        }
        public User Current()
        {
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());
            return user;
        }
        public void Update(User user)
        {
            CreateTable();
            User newUser = new User();
            newUser.UserName = user.UserName;
            newUser.BirthDay =user.BirthDay;
            newUser.LastName = user.LastName;
            newUser.Password = user.Password;
            newUser.Gender =user.Gender;
            newUser.Email =user.Email;
            newUser.UserId = user.UserId;
            

            conn.Insert(newUser);
        }

        public void DeleteUserRecord()
        {
            conn.DropTable<User>();
            conn.CreateTable<User>();
        }

    }
}
