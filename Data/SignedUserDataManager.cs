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
    public class SignedUserDataManager : DataManagerBaseClass
    {
        public void SignedUser(SignedUserRequest request, ICallback<SignedUserResponse> callback)
        {
           User user= DBHandlers.SignedUser(request.User);
            callback.OnSuccess(new Response<SignedUserResponse> { Obj = new SignedUserResponse(user) });
        }
    }
}
