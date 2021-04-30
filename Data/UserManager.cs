using System;
using System.Collections.Generic;
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
            return _UserLists;
        }
        public void AddUser(String username,string lastname,string email, String password,string birthday,string gender)
        {

            _UserLists.Add(new User(username,lastname,email, password,birthday,gender));

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
            
            foreach(var user in _UserLists)
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

    }
}
