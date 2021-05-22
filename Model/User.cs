
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Social.Model
{
    public class User
    {
       
        public User()
        {
            this.UserId = DateTime.Now.Ticks;
            ProfilePic = "ms-appx:///Assets/male-01.png";
           // image = new BitmapImage (new Uri(ProfilePic));
         
        }
        public string ProfilePic { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        [Ignore]
        public BitmapImage image { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BirthDay { get; set; }
        public string Gender { get; set; }
        //  [Ignore]
        // public Image ProfilePicSource { get; set; }
        [PrimaryKey]
        public long UserId { get; set; }



    }
   

    
}
