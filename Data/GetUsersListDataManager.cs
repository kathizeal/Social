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
    public class GetUsersListDataManager:DBHandlers
    {
        public void GetUsersList(ICallback<GetUsersListResponse> callback)
        {
            callback.OnSuccess(new Response<GetUsersListResponse> { Obj = new GetUsersListResponse(GetUsersLists()) });
        }
    }
}
