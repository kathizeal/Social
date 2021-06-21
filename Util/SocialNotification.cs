using Social.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Util
{
    public static class SocialNotification
    {
        public static event Action<Post> PostAdded;
        public static void NotifyPostAdded(Post post)
        {
            PostAdded?.Invoke(post);
        }

    }
} 
