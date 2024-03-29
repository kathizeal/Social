﻿using Social.Data.Handler;
using Social.Domain;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Data
{
    public class AddCommentDataManager:DBHandlers
    {
        public void AddComment(AddCommentRequest request, ICallback<AddCommentResponse> callback)
        {
            callback.OnSuccess(new Response<AddCommentResponse> { Obj = new AddCommentResponse(AddComments(request.Post, request.Comment)) });
        }
    }
}
