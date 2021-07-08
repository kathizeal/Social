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
    public class GetUsersListDataManager:DataManagerBaseClass
    {
        public void GetUsersList(ICallback<GetUsersListResponse> callback)
        {
            List<User> user = DBHandlers.GetUsersLists();

            callback.OnSuccess(new Response<GetUsersListResponse> { Obj = new GetUsersListResponse(user) });
        }
    }
}
