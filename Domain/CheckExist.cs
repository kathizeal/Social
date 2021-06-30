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
    public interface ICheckExistPresenterCallback : ICallback<CheckExistResponse>
    {

    }
    public sealed class CheckExistResponse
    {
        public bool Exist;
        public CheckExistResponse(bool exist)
        {
            Exist=exist;
        }
    }
    public sealed class CheckExistRequest
    {
        public User CurrentUser;
        public User AnotherUser;
        public CheckExistRequest(User currentUser, User anotherUser)
        {
            CurrentUser = currentUser;
            AnotherUser = anotherUser;
        }
    }



    public class CheckExist : UseCaseBase<CheckExistResponse>
    {
        CheckExistDataManager CheckExistDataManager;
        public CheckExistRequest Request;
        public CheckExist(CheckExistRequest request, ICheckExistPresenterCallback callback) : base(callback)
        {
            Request = request;
            CheckExistDataManager  = new CheckExistDataManager();
        }
        protected override void Action()
        {

            CheckExistDataManager.CheckExist(Request, new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<CheckExistResponse>
        {
            private CheckExist UseCase;
            public UseCaseCallback(CheckExist useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<CheckExistResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
