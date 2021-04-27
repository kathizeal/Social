﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
    public class Comment
    {

        public Comment(long postid, string content, string username, long userid,long? parentid)
        {
            PostId = postid;
            CommentContent = content;
            CommenterName = username;
            CommenterId = userid;
            CommentId = DateTime.Now.Ticks;
            CreatedTime = DateTime.UtcNow;
            ParentCommentId = parentid;
        }        
        public long? ParentCommentId { get; set; }
        public string CommenterName { get; set; }
        public long CommenterId { get; set; }
        public long CommentId { get; set; }
        public string Title { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedTime { get; set; }
        public long PostId { get; set; }
       private List<Comment> _Reply = new List<Comment>();
        public List<Comment> Reply { get { return _Reply; } set { _Reply = value; } }
    } 

}
