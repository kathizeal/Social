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
    public interface IDeletePostPresenterCallback : ICallback<DeletePostResponse> { }
    public sealed class DeletePostResponse
    {
        /*
        public Post Post;
        public DeletePostResponse(Post post)
        {
            Post = post;
        }
        */

    }
    public class DeletePostRequest
    {
        public Post Post;
        public DeletePostRequest(Post post)
        {
            Post = post;
        }
    }

    public class DeletePost : UseCaseBase<DeletePostResponse>
    {


        public DeletePostRequest Request;
        DeletePostDataManager deletePostDataManager;
        public DeletePost(DeletePostRequest request, IDeletePostPresenterCallback callback) : base(callback)
        {
            Request = request;
            deletePostDataManager = new DeletePostDataManager();

        }

        protected override void Action()
        {
            deletePostDataManager.DeletePost(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<DeletePostResponse>
        {
            private DeletePost UseCase;
            public UseCaseCallback(DeletePost useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<DeletePostResponse> response)
            {
                //UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
