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
   public class GetCurrentUserDataManager:DataManagerBaseClass
    {
        public void GetCurrentUser(ICallback<GetCurrentUserResponse> callback)
        {
            User user = DBHandlers.GetCurrentUser();
            callback.OnSuccess(new Response<GetCurrentUserResponse> { Obj = new GetCurrentUserResponse(user)});
        }

    }
}
