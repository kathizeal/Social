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
    public class GetCommentDataManager:DBHandlers
    {
        public void GetComment(GetCommentRequest request,ICallback<GetCommentResponse> callback)
        {
            callback.OnSuccess(new Response<GetCommentResponse> { Obj = new GetCommentResponse(GetComment(request.ParentId)) });
        }
    }
}
