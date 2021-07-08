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
    public class GetCommentDataManager:DataManagerBaseClass
    {
        public void GetComment(GetCommentRequest request,ICallback<GetCommentResponse> callback)
        {
            Comment comment = DBHandlers.GetComment(request.ParentId);
            callback.OnSuccess(new Response<GetCommentResponse> { Obj = new GetCommentResponse(comment) });
        }
    }
}
