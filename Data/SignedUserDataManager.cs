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
    public class SignedUserDataManager : DBHandlers
    {
        public void SignedUser(SignedUserRequest request, ICallback<SignedUserResponse> callback)
        {
            callback.OnSuccess(new Response<SignedUserResponse> { Obj = new SignedUserResponse(SignedUser(request.User)) });
        }
    }
}
