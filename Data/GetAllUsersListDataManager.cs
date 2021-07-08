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
    public class GetAllUsersListDataManager : DataManagerBaseClass
    {
        public void GetAllUsersList(ICallback<GetAllUsersListResponse> callback)
        {
            List<User> allUsers = DBHandlers.GetAllUsersLists();
            callback.OnSuccess(new Response<GetAllUsersListResponse> { Obj = new GetAllUsersListResponse(allUsers) });
        }
    }
}
