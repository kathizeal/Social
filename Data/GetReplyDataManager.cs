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
    public class GetReplyDataManager:DBHandlers
    {
        public void GetReply(GetReplyRequest request, ICallback<GetReplyResponse> callback)
        {
            callback.OnSuccess(new Response<GetReplyResponse> { Obj = new GetReplyResponse(GetReply(request.ParentId)) });
        }
    }
}
