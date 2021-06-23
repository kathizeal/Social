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
   public class GetCurrentUserDataManager:DBHandlers
    {
        public void GetCurrentUser(ICallback<GetCurrentUserResponse> callback)
        {
            callback.OnSuccess(new Response<GetCurrentUserResponse> { Obj = new GetCurrentUserResponse(GetCurrentUser())});
        }

    }
}
