using Social.Data.Handler;
using Social.Domain;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Data
{
    public class ViewMyPostDataManager:DBHandlers
    {
        public void ViewMyPost(ViewMyPostRequest request, ICallback<ViewMyPostResponse> callback)
        {
            callback.OnSuccess(new Response<ViewMyPostResponse> { Obj = new ViewMyPostResponse(ViewMyPost(request.UserId)) });
        }
    }
}
