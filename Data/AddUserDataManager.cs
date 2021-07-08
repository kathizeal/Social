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
    public class AddUserDataManager:DataManagerBaseClass
    {
        public void AddUser(AddUserRequest request, ICallback<AddUserResponse> callback)
        {
            DBHandlers.AddUser(request.UserName, request.LastName, request.Email, request.Password, request.Birthday, request.Gender);
            callback.OnSuccess(new Response<AddUserResponse> { Obj = new AddUserResponse() });
        }
    }
}
