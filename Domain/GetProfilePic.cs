using Social.Data;
using Social.Model;
using Social.UseCase;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain
{
    public interface IGetProfilePicPresenterCallback:ICallback<GetProfilePicResponse> { }
    public sealed class GetProfilePicResponse
    {
        public string ProfilePic;
        public GetProfilePicResponse(string profilePic)
        {
            ProfilePic = profilePic;
        }
    }
    public sealed class GetProfilePicRequest
    {
        public User User;
        public GetProfilePicRequest(User user)
        {
            User = user;
        }
    }

    public class GetProfilePic : UseCaseBase<GetProfilePicResponse>
    {
        public GetProfilePicRequest Request;
        GetProfileDataManager GetProfileDataManager;
        public GetProfilePic(GetProfilePicRequest request,IGetProfilePicPresenterCallback callback):base(callback)
        {
            Request = request;
            GetProfileDataManager = new GetProfileDataManager();
        }
        protected override void Action()
        {
            GetProfileDataManager.GetProfilePic(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<GetProfilePicResponse>
        {
            private GetProfilePic UseCase;
            public UseCaseCallback(GetProfilePic useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetProfilePicResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
