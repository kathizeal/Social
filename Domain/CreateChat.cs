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
    public interface ICreateChatPresenterCallback : ICallback<CreateChatResponse>
    {

    }
    public sealed class CreateChatResponse
    {
        /*
        public Chat Chat;
        public CreateChatResponse(Chat chat)
        {
            Chat = chat;
        }
        */
    }
    public sealed class CreateChatRequest
    {
        public User CurrentUser;
        public User AnotherUser;
        public CreateChatRequest(User currentUser, User anotherUser)
        {
            CurrentUser = currentUser;
            AnotherUser = anotherUser;
        }
    }



    public class CreateChat : UseCaseBase<CreateChatResponse>
    {
        CreateChatDataManager CreateChatDataManager;
        public CreateChatRequest Request;
        public CreateChat(CreateChatRequest request, ICreateChatPresenterCallback callback) : base(callback)
        {
            Request = request;
            CreateChatDataManager = new CreateChatDataManager();
        }
        protected override void Action()
        {

            CreateChatDataManager.CreateChat(Request, new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<CreateChatResponse>
        {
            private CreateChat UseCase;
            public UseCaseCallback(CreateChat useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<CreateChatResponse> response)
            {
              //  UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
