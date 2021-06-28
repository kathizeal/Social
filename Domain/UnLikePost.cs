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
    public interface IUnLikePostPresenterCallback : ICallback<UnLikePostResponse> { }
    public sealed class UnLikePostResponse
    {
        public Post Post;
        public UnLikePostResponse(Post post)
        {
            Post = post;

        }

    }
    public sealed class UnLikePostRequest
    {
        public Post Post;
        public User User;
        public UnLikePostRequest(Post post, User user)
        {
            Post = post;
            User = user;
        }

    }



    public class UnLikePost : UseCaseBase<UnLikePostResponse>
    {
        public UnLikePostRequest Request;
        UnLikePostDataManager unLikePostDataManager;
        public UnLikePost(UnLikePostRequest request, IUnLikePostPresenterCallback callback) : base(callback)
        {
            Request = request;
            unLikePostDataManager = new UnLikePostDataManager();
        }

        protected override void Action()
        {
            unLikePostDataManager.UnLikePost(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<UnLikePostResponse>
        {
            public UnLikePost UseCase;
            public UseCaseCallback(UnLikePost useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<UnLikePostResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
