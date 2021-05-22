using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
   public class UserIds
    {
        [PrimaryKey]
        public long Unique { get; set; }
        public long PostId { get; set; }
        public string UserName { get; set; }
        public long Userid { get; set; }
       
        
    }
}
