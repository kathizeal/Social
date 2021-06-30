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
    class DeletePostDataManager:DBHandlers

    {
        public void DeletePost(DeletePostRequest request, ICallback<DeletePostResponse> callback)
        {

            callback.OnSuccess(new Response<DeletePostResponse> { Obj = new DeletePostResponse(DeletePost(request.Post)) });
            SocialNotification.NotifyPostDeleted(request.Post);
        }
    }
}
