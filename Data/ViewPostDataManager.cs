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
    public class ViewPostDataManager:DBHandlers
    {
        public void ViewPost(ViewPostRequest request, ICallback<ViewPostResponse> callback)
        {
            callback.OnSuccess(new Response<ViewPostResponse> { Obj = new ViewPostResponse(ViewPost(request.PostId)) });
        }
    }

}
