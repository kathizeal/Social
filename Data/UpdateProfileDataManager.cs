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
    public class UpdateProfileDataManager:DataManagerBaseClass
    {
        public void UpdateProfile(UpdateProfileRequest request, ICallback<UpdateProfileResponse> callback)
        {
            string profilePic = DBHandlers.UpdateProfilePic(request.User, request.Profile);
            callback.OnSuccess(new Response<UpdateProfileResponse> { Obj = new UpdateProfileResponse(profilePic) });
        }
    }
}
