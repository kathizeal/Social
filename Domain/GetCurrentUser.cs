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
    public interface IGetCurrentUserPresenterCallback : ICallback<GetCurrentUserResponse> { }
    public sealed class GetCurrentUserResponse
    {
        public User CurrentUser;
        public GetCurrentUserResponse(User currentUser)
        {
            CurrentUser = currentUser;
        }

    }
    public class GetCurrentUserRequest
    {
    }

   
    public class GetCurrentUser:UseCaseBase<GetCurrentUserResponse>
    {
        public GetCurrentUserRequest Request;
        GetCurrentUserDataManager getCurrentUserDataManager;
        public GetCurrentUser(GetCurrentUserRequest request, IGetCurrentUserPresenterCallback callback) : base(callback)
        {
            Request = request;
            getCurrentUserDataManager = new GetCurrentUserDataManager();

        }

        protected override void Action()
        {
            getCurrentUserDataManager.GetCurrentUser(new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<GetCurrentUserResponse>
        {
            private GetCurrentUser UseCase;
            public UseCaseCallback(GetCurrentUser useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetCurrentUserResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
