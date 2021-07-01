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
    public class GetAllUsersListDataManager : DBHandlers
    {
        public void GetAllUsersList(ICallback<GetAllUsersListResponse> callback)
        {
            callback.OnSuccess(new Response<GetAllUsersListResponse> { Obj = new GetAllUsersListResponse(GetAllUsersLists()) });
        }
    }
}
