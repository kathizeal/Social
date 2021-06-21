using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Util
{
    public interface IRequest { }

    public class Request:IRequest
    {
        public RequestType Type { get; private set; }
        public Request() {  }
    }
    public enum RequestType
    {
        DB
    }

}
