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
    public class GetLikedUserDataManager : DataManagerBaseClass
    {
        public void GetLiked(GetLikedUsersRequest request,ICallback<GetLikedUsersResponse> callback)
        {
            List<UserIds> userIds = DBHandlers.LikedUsers(request.CurrentPost);
            callback.OnSuccess(new Response<GetLikedUsersResponse> { Obj = new GetLikedUsersResponse(userIds) });
        }
    }
}
