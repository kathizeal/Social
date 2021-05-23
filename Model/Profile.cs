using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
    public class Profile
    {
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public Profile(string name,string imageFile)
        {
            Name = name;
            ImageFile = imageFile;
        }
    }
}
