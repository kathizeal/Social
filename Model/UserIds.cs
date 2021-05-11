using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
   class UserIds
    {
        
        public long PostId { get; set; }
        public String UserName { get; set; }
        [PrimaryKey]
        public long Userid { get; set; }
        
    }
}
