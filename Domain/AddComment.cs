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
    public interface IAddCommentPresenterCallback : ICallback<AddCommentResponse> { }
    public sealed class AddCommentResponse
    {
       /* public Comment Comment;
        public AddCommentResponse(Comment comment)
        {
            Comment = comment;
        }
       */

    }
    public  class AddCommentRequest
    {
        public Post Post;
        public Comment Comment;
        public AddCommentRequest(Post post,Comment comment)
        {
            Post = post;
            Comment = comment;

        }
    }

    public class AddComment : UseCaseBase<AddCommentResponse>
    {


        public AddCommentRequest Request;
        AddCommentDataManager addCommentDataManager;
        public AddComment(AddCommentRequest request, IAddCommentPresenterCallback callback) : base(callback)
        {
            Request = request;
            addCommentDataManager = new AddCommentDataManager();

        }

        protected override void Action()
        {
            addCommentDataManager.AddComment(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<AddCommentResponse>
        {
            private AddComment UseCase;
            public UseCaseCallback(AddComment useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<AddCommentResponse> response)
            {
              
            }
        }
    }
}
