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
    public interface IViewMyPostPresenterCallback : ICallback<ViewMyPostResponse> { }
    public sealed class ViewMyPostResponse
    {
        public List<Post> Posts;
        public ViewMyPostResponse(List<Post> posts)
        {
            Posts = posts;
        }

    }
    public class ViewMyPostRequest
    {
        public long UserId;
        public ViewMyPostRequest(long userId)
        {
            UserId = userId;
        }
    }

    public class ViewMyPost : UseCaseBase<ViewMyPostResponse>
    {


        public ViewMyPostRequest Request;
        ViewMyPostDataManager ViewMyPostDataManager;
        public ViewMyPost(ViewMyPostRequest request, IViewMyPostPresenterCallback callback) : base(callback)
        {
            Request = request;
            ViewMyPostDataManager = new ViewMyPostDataManager();

        }

        protected override void Action()
        {
            ViewMyPostDataManager.ViewMyPost(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<ViewMyPostResponse>
        {
            private ViewMyPost UseCase;
            public UseCaseCallback(ViewMyPost useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<ViewMyPostResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
