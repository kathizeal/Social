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
    public class UpdateProfileDataManager:DBHandlers
    {
        public void UpdateProfile(UpdateProfileRequest request, ICallback<UpdateProfileResponse> callback)
        {
            callback.OnSuccess(new Response<UpdateProfileResponse> { Obj = new UpdateProfileResponse(UpdateProfilePic(request.User,request.Profile)) });
        }
    }
}
