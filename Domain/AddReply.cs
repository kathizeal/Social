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
    public interface IAddReplyPresenterCallback : ICallback<AddReplyResponse> { }
    public sealed class AddReplyResponse
    {
      /*  public Comment Reply;
        public AddReplyResponse(Comment reply)
        {
            Reply = reply;
        }
      */
    }
    public class AddReplyRequest
    {
        //public Post Post;
        public Comment Reply;
        public AddReplyRequest(Comment reply)
        {
            
            Reply= reply;

        }
    }

    public class AddReply : UseCaseBase<AddReplyResponse>
    {


        public AddReplyRequest Request;
        AddReplyDataManager AddReplyDataManager;
        public AddReply(AddReplyRequest request, IAddReplyPresenterCallback callback) : base(callback)
        {
            Request = request;
            AddReplyDataManager = new AddReplyDataManager();

        }

        protected override void Action()
        {
            AddReplyDataManager.AddReply(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<AddReplyResponse>
        {
            private AddReply UseCase;
            public UseCaseCallback(AddReply useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<AddReplyResponse> response)
            {
                //UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
