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
    public interface ISignedUserPresenterCallback : ICallback<SignedUserResponse>
    {

    }
    public sealed class SignedUserResponse
    {
        public User User;
        public SignedUserResponse(User user)
        {
            User = user;
        }
    }
    public sealed class SignedUserRequest
    {
        public User User;
        public SignedUserRequest(User user)
        {
            User = user;
        }
    }



    public class SignedUser : UseCaseBase<SignedUserResponse>
    {
        SignedUserDataManager signedUserDataManager;
        public SignedUserRequest Request { get; private set; }
        public SignedUser(SignedUserRequest request, ISignedUserPresenterCallback callback) : base(callback)
        {
            Request = request;
            signedUserDataManager = new SignedUserDataManager();
        }
        protected override void Action()
        {

            signedUserDataManager.SignedUser(Request,new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<SignedUserResponse>
        {
            private SignedUser UseCase;
            public UseCaseCallback(SignedUser useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<SignedUserResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
