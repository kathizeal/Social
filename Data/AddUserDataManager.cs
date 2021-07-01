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
    public class AddUserDataManager:DBHandlers
    {
        public void AddUser(AddUserRequest request, ICallback<AddUserResponse> callback)
        {
            callback.OnSuccess(new Response<AddUserResponse> { Obj = new AddUserResponse(AddUser(request.UserName,request.LastName,request.Email,request.Password,request.Birthday,request.Gender)) });
        }
    }
}
