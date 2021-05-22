using Social.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social
{
    public class CustomEvent : EventArgs
    {
        public Post Post { get; set; }
        public CustomEvent(Post post)
        {
            Post = post;
        }
        
    }
}
