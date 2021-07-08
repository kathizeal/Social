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
    public class GetReplyDataManager:DataManagerBaseClass
    {
        public void GetReply(GetReplyRequest request, ICallback<GetReplyResponse> callback)
        {
            List<Comment> replys = DBHandlers.GetReply(request.ParentId);
            callback.OnSuccess(new Response<GetReplyResponse> { Obj = new GetReplyResponse(replys) });
        }
    }
}
