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
    public interface ILikePostPresenterCallback:ICallback<LikePostResponse> { }
    public sealed class LikePostResponse
    {

        /*
       public Post Post;
       public LikePostResponse(Post post)
        {
            Post = post;
           
        }
        */

    }
    public sealed class LikePostRequest
    {
        public Post Post;
        public User User;
        public LikePostRequest(Post post, User user)
        {
            Post = post;
            User = user;
        }

    }



    public class LikePost:UseCaseBase<LikePostResponse>
    {
        public LikePostRequest Request;
        LikePostDataManager likePostDataManager;
        public LikePost(LikePostRequest request,ILikePostPresenterCallback callback):base(callback)
        {
            Request = request;
            likePostDataManager = new LikePostDataManager();
        }

        protected override void Action()
        {
            likePostDataManager.LikePost(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<LikePostResponse>
        {
            public LikePost UseCase;
            public UseCaseCallback(LikePost useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<LikePostResponse> response)
            {
              //  UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
