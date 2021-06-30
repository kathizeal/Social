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
    public interface IViewPostPresenterCallback : ICallback<ViewPostResponse> { }
    public sealed class ViewPostResponse
    {
        public Post Post;
        public ViewPostResponse(Post post)
        {
            Post = post;
        }

    }
    public class ViewPostRequest
    {
        public long PostId;
        public ViewPostRequest(long postId)
        {
            PostId = postId;
        }
    }

    public class ViewPost : UseCaseBase<ViewPostResponse>
    {


        public ViewPostRequest Request;
        ViewPostDataManager ViewPostDataManager;
        public ViewPost(ViewPostRequest request, IViewPostPresenterCallback callback) : base(callback)
        {
            Request = request;
            ViewPostDataManager = new ViewPostDataManager();

        }

        protected override void Action()
        {
            ViewPostDataManager.ViewPost(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<ViewPostResponse>
        {
            private ViewPost UseCase;
            public UseCaseCallback(ViewPost useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<ViewPostResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
