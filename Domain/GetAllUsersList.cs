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
    public interface IGetAllUsersListPresenterCallback : ICallback<GetAllUsersListResponse>
    {

    }
    public sealed class GetAllUsersListResponse
    {
        public List<User> Users;
        public GetAllUsersListResponse(List<User> users)
        {
            Users = users;
        }
    }
    public sealed class GetAllUsersListRequest
    {

    }



    public class GetAllUsersList : UseCaseBase<GetAllUsersListResponse>
    {
        GetAllUsersListDataManager getAllUsersListDataManager;
        public GetAllUsersListRequest Request { get; private set; }
        public GetAllUsersList(GetAllUsersListRequest request, IGetAllUsersListPresenterCallback callback) : base(callback)
        {
            Request = request;
            getAllUsersListDataManager = new GetAllUsersListDataManager();
        }
        protected override void Action()
        {

            getAllUsersListDataManager.GetAllUsersList(new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<GetAllUsersListResponse>
        {
            private GetAllUsersList UseCase;
            public UseCaseCallback(GetAllUsersList useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetAllUsersListResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
