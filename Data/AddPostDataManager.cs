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
    public class AddPostDataManager:DataManagerBaseClass
    {
        public void AddPost(AddPostRequest request,ICallback<AddPostResponse> callback)
        {
            DBHandlers.AddorUpdatePost(request.Post);
            callback.OnSuccess(new Response<AddPostResponse> { Obj = new AddPostResponse() });
            SocialNotification.NotifyPostAdded(request.Post);
        }
    }
}
