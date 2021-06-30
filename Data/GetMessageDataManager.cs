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
    public class GetMessageDataManager:DBHandlers
    {
        public void GetMessage(GetMessageRequest request, ICallback<GetMessageResponse> callback)
        {
            callback.OnSuccess(new Response<GetMessageResponse> { Obj = new GetMessageResponse(Message(request.CurrentUser,request.AnotherUser)) });
        }
    }
}
