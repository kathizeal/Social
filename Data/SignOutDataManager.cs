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
    public class SignOutDataManager :DataManagerBaseClass
    {
        public void SignOut(SignOutRequest request, ICallback<SignOutResponse> callback)
        {
            User user = DBHandlers.SignOut(request.User);
            callback.OnSuccess(new Response<SignOutResponse> { Obj = new SignOutResponse(user) });
        }
    }
}
