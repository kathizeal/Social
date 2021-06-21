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
    public class AddPostDataManager:DBHandlers
    {
        public void AddPost(AddPostRequest request,ICallback<AddPostResponse> callback)
        {

            callback.OnSuccess(new Response<AddPostResponse> { Obj = new AddPostResponse(AddorUpdatePost(request.Post)) });
            SocialNotification.NotifyPostAdded(request.Post);
        }
    }
}
