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
    public interface IGetMessagePresenterCallback : ICallback<GetMessageResponse>
    {

    }
    public sealed class GetMessageResponse
    {
        public List<Chat> Chats;
        public GetMessageResponse(List<Chat> chats)
        {
            Chats = chats;
        }
    }
    public sealed class GetMessageRequest 
    {
        public User CurrentUser;
        public User AnotherUser;
        public GetMessageRequest(User currentUser, User anotherUser)
        {
            CurrentUser = currentUser;
            AnotherUser = anotherUser;
        }
    }



    public class GetMessage : UseCaseBase<GetMessageResponse>
    {
        GetMessageDataManager GetMessageDataManager;
        public GetMessageRequest Request;
        public GetMessage(GetMessageRequest request, IGetMessagePresenterCallback callback) : base(callback)
        {
            Request = request;
            GetMessageDataManager GetMessageDataManager = new GetMessageDataManager();
        }
        protected override void Action()
        {

            GetMessageDataManager.GetMessage(Request,new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<GetMessageResponse>
        {
            private GetMessage UseCase;
            public UseCaseCallback(GetMessage useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetMessageResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
