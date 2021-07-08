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
    public interface IAddChatPresenterCallback : ICallback<AddChatResponse>
    {

    }
    public sealed class AddChatResponse
    {
        /*public Chat Chat;
        public AddChatResponse(Chat chat)
        {
            Chat = chat;
        }*/
    }
    public sealed class AddChatRequest
    {

        public Chat Chat;
        public AddChatRequest(Chat chat)
        {
            Chat = chat;
        }
    }



    public class AddChat : UseCaseBase<AddChatResponse>
    {
        AddChatDataManager AddChatDataManager;
        public AddChatRequest Request;
        public AddChat(AddChatRequest request, IAddChatPresenterCallback callback) : base(callback)
        {
            Request = request;
            AddChatDataManager = new AddChatDataManager();
        }
        protected override void Action()
        {

            AddChatDataManager.AddChat(Request, new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<AddChatResponse>
        {
            private AddChat UseCase;
            public UseCaseCallback(AddChat useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<AddChatResponse> response)
            {
                
            }
        }
    }
}
