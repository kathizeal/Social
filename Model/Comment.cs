using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
    public class Comment
    {

       
        public Comment()
        {
            CommentId = DateTime.Now.Ticks;
            CreatedTime = DateTime.UtcNow.ToShortDateString();
           
            
        }
        [ForeignKey(typeof(Comment))]
        public long? ParentCommentId { get; set; }
        public string CommenterName { get; set; }
        public long CommenterId { get; set; }
        [PrimaryKey]
        public long CommentId { get; set; }
        public string Title { get; set; }
       
        public string CommentContent { get; set; }
        public string CreatedTime { get; set; }
        [ForeignKey(typeof(Post))]
        public long PostId { get; set; } 
        
       
       
       private List<Comment> _Reply = new List<Comment>();
        [Ignore]
        public List<Comment> Reply { get { return _Reply; } set { _Reply = value; } }
    } 

}
