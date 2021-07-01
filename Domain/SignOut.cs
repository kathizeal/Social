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

    public interface ISignOutPresenterCallback : ICallback<SignOutResponse>
    {

    }
    public sealed class SignOutResponse
    {
        public User User;
        public SignOutResponse(User user)
        {
            User = user;
        }
    }
    public sealed class SignOutRequest
    {
        public User User;
        public SignOutRequest(User user)
        {
            User = user;
        }
    }



    public class SignOut : UseCaseBase<SignOutResponse>
    {
        SignOutDataManager signOutDataManager;
        public SignOutRequest Request { get; private set; }
        public SignOut(SignOutRequest request, ISignOutPresenterCallback callback) : base(callback)
        {
            Request = request;
            signOutDataManager = new SignOutDataManager();
        }
        protected override void Action()
        {

            signOutDataManager.SignedUser(Request, new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<SignOutResponse>
        {
            private SignOut UseCase;
            public UseCaseCallback(SignOut useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<SignOutResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
