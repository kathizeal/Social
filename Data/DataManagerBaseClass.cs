using Social.Data.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Data
{
    public class DataManagerBaseClass
    {
        public DBHandlers DBHandlers;
        public DataManagerBaseClass()
        {
            DBHandlers = DBHandlers.GetInstance();
        }
    }
}
