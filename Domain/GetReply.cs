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
    public interface IGetReplyPresenterCallback : ICallback<GetReplyResponse>
    {

    }
    public sealed class GetReplyResponse
    {
        public List<Comment> Replys;
        public GetReplyResponse(List<Comment> replys)
        {
            Replys = replys;
        }
    }
    public class GetReplyRequest
    {
        public long ParentId;
        public GetReplyRequest(long parentId)
        {
            ParentId = parentId;
        }
    
    }




    public class GetReply : UseCaseBase<GetReplyResponse>
    {
        GetReplyDataManager GetReplyDataManager;
        public GetReplyRequest Request;
        public GetReply(GetReplyRequest request, IGetReplyPresenterCallback callback) : base(callback)
        {
            Request = request;
            GetReplyDataManager = new GetReplyDataManager();
        }
        protected override void Action()
        {

            GetReplyDataManager.GetReply(Request, new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<GetReplyResponse>
        {
            private GetReply UseCase;
            public UseCaseCallback(GetReply useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetReplyResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
