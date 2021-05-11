using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
    public class User
    {
        /*public User(string userName,string lastName,string email, string password,string birthday,string gender)
        {
            this.UserId = DateTime.Now.Ticks;
            this.UserName = userName;
            this.Password = password;
            this.LastName = lastName;
            this.Email = email;
            this.BirthDay = birthday;
            this.Gender = gender;
        }*/
        public User()
        {
            this.UserId = DateTime.Now.Ticks;
        }
        public string UserName { get; set; }
        public string LastName { get; set; }
        [PrimaryKey]
        public long UserId { get; set; }
        public string Password { get; set; }
        
        public string Email { get; set; }
        public string BirthDay { get; set; }
        public string Gender { get; set; }
       


    }
   

    
}
