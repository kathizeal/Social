using Social.Data;
using Social.Model;
using Social.UseCase;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Social.Domain
{
    public interface IUpdateProfilePresenterCallback : ICallback<UpdateProfileResponse> { }
    public sealed class UpdateProfileResponse
    {
        public string ProfilePic;
        public UpdateProfileResponse(string profilePic)
        {
            ProfilePic = profilePic;
        }
    }
    public sealed class UpdateProfileRequest
    {
        public User User;
        public Profile Profile;
        public UpdateProfileRequest(User user,Profile profile)
        {
            User = user;
            Profile = profile;
        }
    }

    public class UpdateProfile : UseCaseBase<UpdateProfileResponse>
    {
        public UpdateProfileRequest Request;
        UpdateProfileDataManager UpdateProfileDataManager;
        public UpdateProfile(UpdateProfileRequest request, IUpdateProfilePresenterCallback callback) : base(callback)
        {
            Request = request;
            UpdateProfileDataManager = new UpdateProfileDataManager();
        }
        protected override void Action()
        {
            UpdateProfileDataManager.UpdateProfile(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<UpdateProfileResponse>
        {
            private UpdateProfile UseCase;
            public UseCaseCallback(UpdateProfile useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<UpdateProfileResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
