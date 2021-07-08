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
    public class UpdateUserDataManager:DataManagerBaseClass
    {
        public void UpdateUser(UpdateUserRequest request, ICallback<UpdateUserResponse> callback)
        {
            DBHandlers.UpdateUser(request.CurrentUser);
            callback.OnSuccess(new Response<UpdateUserResponse> { Obj = new UpdateUserResponse() });
        }
    }
}
