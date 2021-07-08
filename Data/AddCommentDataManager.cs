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
    public class AddCommentDataManager:DataManagerBaseClass
    {
        public void AddComment(AddCommentRequest request, ICallback<AddCommentResponse> callback)
        {
            DBHandlers.AddComments(request.Post, request.Comment);
            callback.OnSuccess(new Response<AddCommentResponse> { Obj = new AddCommentResponse() });
        }
    }
}
