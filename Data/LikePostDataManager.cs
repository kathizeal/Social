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
    public class LikePostDataManager:DBHandlers
    {
        public void LikePost(LikePostRequest request,ICallback<LikePostResponse> callback)
        {
            callback.OnSuccess(new Response<LikePostResponse> { Obj = new LikePostResponse(LikePost(request.Post, request.User)) });
        }
    }
}
