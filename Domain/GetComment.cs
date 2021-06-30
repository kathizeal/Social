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
    public interface IGetCommentPresenterCallback:ICallback<GetCommentResponse> { }
    public class GetCommentResponse
    {
        public Comment Comment;
        public GetCommentResponse(Comment comment)
        {
            Comment = comment;
        }
    }
    public class GetCommentRequest
    {
        public long ParentId;
        public GetCommentRequest(long parentId)
        {
            ParentId = parentId;
        }
    }


    public class GetComment : UseCaseBase<GetCommentResponse>
    {
        GetCommentDataManager GetCommentDataManager;
        public GetCommentRequest Request;
        public GetComment(GetCommentRequest request,IGetCommentPresenterCallback callback):base(callback)
        {
            Request = request;
            GetCommentDataManager = new GetCommentDataManager();
        }
        protected override void Action()
        {
            GetCommentDataManager.GetComment(Request,new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<GetCommentResponse>
        {
            private GetComment UseCase;
            public UseCaseCallback(GetComment useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetCommentResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
