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
    public interface IUpdateUserPresenterCallback : ICallback<UpdateUserResponse> { }
    public sealed class UpdateUserResponse
    {
        public User CurrentUser;
        public UpdateUserResponse(User currentUser)
        {
            CurrentUser = currentUser;
        }

    }
    public class UpdateUserRequest
    {
        public User CurrentUser;
        public UpdateUserRequest(User currentUser)
        {
            CurrentUser = currentUser;
        }
    }


    public class UpdateUser : UseCaseBase<UpdateUserResponse>
    {
        public UpdateUserRequest Request;
        UpdateUserDataManager UpdateUserDataManager;
        public UpdateUser(UpdateUserRequest request, IUpdateUserPresenterCallback callback) : base(callback)
        {
            Request = request;
            UpdateUserDataManager = new UpdateUserDataManager();

        }

        protected override void Action()
        {
            UpdateUserDataManager.UpdateUser(Request,new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<UpdateUserResponse>
        {
            private UpdateUser UseCase;
            public UseCaseCallback(UpdateUser useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<UpdateUserResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
