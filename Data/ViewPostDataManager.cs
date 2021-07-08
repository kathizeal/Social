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
    public class ViewPostDataManager:DataManagerBaseClass
    {
        public void ViewPost(ViewPostRequest request, ICallback<ViewPostResponse> callback)
        {
            Post receivedPost = DBHandlers.ViewPost(request.PostId);
            Response<ViewPostResponse> response = new Response<ViewPostResponse>
            {
                Obj = new ViewPostResponse(receivedPost)
            };
            callback.OnSuccess(response);

        

        }
    }

}
