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
    public class GetLikedUserDataManager:DBHandlers
    {
        public void GetLiked(GetLikedUsersRequest request,ICallback<GetLikedUsersResponse> callback)
        {
            callback.OnSuccess(new Response<GetLikedUsersResponse> { Obj = new GetLikedUsersResponse(LikedUsers(request.CurrentPost)) });
        }
    }
}
