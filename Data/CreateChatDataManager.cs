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
    public class CreateChatDataManager:DataManagerBaseClass
    {
        public void CreateChat(CreateChatRequest request, ICallback<CreateChatResponse> callback)
        {
            DBHandlers.CreateChat(request.CurrentUser, request.AnotherUser);
            callback.OnSuccess(new Response<CreateChatResponse> { Obj = new CreateChatResponse() });
        }
    }
}
