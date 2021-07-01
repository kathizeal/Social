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
    public interface IAddUserPresenterCallback : ICallback<AddUserResponse>
    {

    }
    public class AddUserResponse
    {
        public User User;
        public AddUserResponse(User user)
        {
            User = user;
        }
    }
    public class AddUserRequest
    {
        public string UserName;
        public string LastName;
        public string Email;
        public string Password;
        public string Birthday;
        public string Gender;
        public AddUserRequest(string username, string lastname, string email, string password, string birthday, string gender)
        {
            UserName = username;
            LastName = lastname;
            Email = email;
            Password = password;
            Birthday = birthday;
            Gender = gender;

        }


    }



    public class AddUser : UseCaseBase<AddUserResponse>
    {
        AddUserDataManager addUserDataManager;
        public AddUserRequest Request { get; private set; }
        public AddUser(AddUserRequest request, IAddUserPresenterCallback callback) : base(callback)
        {
            Request = request;
            addUserDataManager = new AddUserDataManager();
        }
        protected override void Action()
        {

            addUserDataManager.AddUser(Request,new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<AddUserResponse>
        {
            private AddUser UseCase;
            public UseCaseCallback(AddUser useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<AddUserResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
