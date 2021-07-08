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
    class DeletePostDataManager:DataManagerBaseClass

    {
        public void DeletePost(DeletePostRequest request, ICallback<DeletePostResponse> callback)
        {
            DBHandlers.DeletePost(request.Post);
            callback.OnSuccess(new Response<DeletePostResponse> { Obj = new DeletePostResponse() });
            SocialNotification.NotifyPostDeleted(request.Post);
        }
    }
}
