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
    public class AddChatDataManager:DataManagerBaseClass
    {
        public void AddChat(AddChatRequest request, ICallback<AddChatResponse> callback)
        {
            DBHandlers.AddChat(request.Chat);

            callback.OnSuccess(new Response<AddChatResponse> { Obj = new AddChatResponse() });
        }
    }
}
