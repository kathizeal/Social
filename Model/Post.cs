using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Social.Model
{

    public class Post
    {        
      
       public Post()
        {
            PostId = DateTime.Now.Ticks;
            CreatedTime = DateTime.UtcNow;
            Likes = 0;
            CommentCount = 0;

            
        }
        public string PostCreatedByUserName { get; set; }
        public long PostCreatedByUserId { get; set; }
        [PrimaryKey]
        public long PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Likes { get; set; }
        public int CommentCount { get; set; }
        private List<Comment> _Comments =new List<Comment>();
        [Ignore]
        public List<Comment> Comments { get { return _Comments; } set { _Comments = value; } }
        private List<long> _LikedId = new List<long>();
         [Ignore]
        public List<long> LikedId { get { return _LikedId; }  set { _LikedId = value; } }
        
    }
}
