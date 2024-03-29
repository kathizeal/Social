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
    public class AddReplyDataManager:DBHandlers
    {
        public void AddReply(AddReplyRequest request, ICallback<AddReplyResponse> callback)
        {
            callback.OnSuccess(new Response<AddReplyResponse> { Obj = new AddReplyResponse(AddReply(request.Reply)) });
        }
    }
}
