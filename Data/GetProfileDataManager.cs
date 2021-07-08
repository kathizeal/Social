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
    public class GetProfileDataManager : DataManagerBaseClass
    {
        public void GetProfilePic(GetProfilePicRequest request, ICallback<GetProfilePicResponse> callback)
        {
            string profilePic =DBHandlers.GetProfilePic(request.User);
            callback.OnSuccess(new Response<GetProfilePicResponse> { Obj = new GetProfilePicResponse(profilePic) });
        }
    }
}
