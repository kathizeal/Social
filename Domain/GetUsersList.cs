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
    public interface IGetUsersListPresenterCallback : ICallback<GetUsersListResponse>
    {

    }
    public sealed class GetUsersListResponse
    {
        public List<User> Users;
        public GetUsersListResponse(List<User> users)
        {
            Users = users;
        }
    }
    public sealed class GetUsersListRequest
    {

    }



    public class GetUsersList : UseCaseBase<GetUsersListResponse>
    {
        GetUsersListDataManager getUsersListDataManager;
        public GetUsersListRequest Request { get; private set; }
        public GetUsersList(GetUsersListRequest request, IGetUsersListPresenterCallback callback) : base(callback)
        {
            Request = request;
            getUsersListDataManager = new GetUsersListDataManager();
        }
        protected override void Action()
        {

            getUsersListDataManager.GetUsersList(new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<GetUsersListResponse>
        {
            private GetUsersList UseCase;
            public UseCaseCallback(GetUsersList useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetUsersListResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
