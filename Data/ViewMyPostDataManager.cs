using Social.Data.Handler;
using Social.Domain;
using Social.Model;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Data
{
    public class ViewMyPostDataManager:DataManagerBaseClass
    {
        public void ViewMyPost(ViewMyPostRequest request, ICallback<ViewMyPostResponse> callback)
        {
            List<Post> posts= DBHandlers.ViewMyPost(request.UserId);
            callback.OnSuccess(new Response<ViewMyPostResponse> { Obj = new ViewMyPostResponse(posts) });
        }
    }
}
