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
    public class SignOutDataManager : DBHandlers
    {
        public void SignOut(SignOutRequest request, ICallback<SignOutResponse> callback)
        {
            callback.OnSuccess(new Response<SignOutResponse> { Obj = new SignOutResponse(SignOut(request.User)) });
        }
    }
}
