using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Social.Model
{
    public class Comment
    {
       

        public Comment()
        {
            CommentId = DateTime.Now.Ticks;
            CreatedTime = DateTime.UtcNow;
            ProfilePic = "ms-appx:///Assets/male-01.png";


        }
        [ForeignKey(typeof(Comment))]
        public long? ParentCommentId { get; set; }
        public string CommenterName { get; set; }
        public long CommenterId { get; set; }
        [PrimaryKey]
        public long CommentId { get; set; }
        public string Title { get; set; }
        public string ProfilePic { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedTimeString {get;set;} 
        [ForeignKey(typeof(Post))]
        public long PostId { get; set; }
        [Ignore]
        public Comment CurrentReply { get; set; }
        private ObservableCollection<Comment> _Reply = new ObservableCollection<Comment>();
        [Ignore]
        public ObservableCollection<Comment> Reply { get { return _Reply; } set { _Reply = value; } }
       

    }

}
