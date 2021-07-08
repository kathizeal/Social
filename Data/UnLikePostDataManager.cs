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
    public class UnLikePostDataManager:DataManagerBaseClass
    {
        public void UnLikePost(UnLikePostRequest request, ICallback<UnLikePostResponse> callback)
        {
            DBHandlers.UnLikePost(request.Post, request.User);
            callback.OnSuccess(new Response<UnLikePostResponse> { Obj = new UnLikePostResponse() });
        }
    }
}
