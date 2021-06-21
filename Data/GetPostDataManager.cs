using Social.Domain;
using Social.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Social.Data.Handler;
using Social.Util;

namespace Social.Data
{
    public class GetPostDataManager:DBHandlers
    {
        
        public void GetPosts(ICallback<GetPostsResponse> callback)
        {

          callback.OnSuccess(new Response<GetPostsResponse> { Obj = new GetPostsResponse(GetPosts()) });
        }




    }

}
