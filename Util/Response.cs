using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Util
{
    public interface IResponse<O> { }


    public class Response<O>:IResponse<O>
    {
       public O Obj { get; set; } 
    }
}
