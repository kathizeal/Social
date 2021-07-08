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
    public class GetMessageDataManager:DataManagerBaseClass
    {
        public void GetMessage(GetMessageRequest request, ICallback<GetMessageResponse> callback)
        {
            List<Chat> chats = DBHandlers.Message(request.CurrentUser, request.AnotherUser);
            callback.OnSuccess(new Response<GetMessageResponse> { Obj = new GetMessageResponse(chats) });
        }
    }
}
