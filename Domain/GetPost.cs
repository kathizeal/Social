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
    

    public interface IGetPostsPresenterCallback : ICallback<GetPostsResponse>
    {
        
    }
    public sealed class GetPostsResponse
    {
        public List<Post> Posts;
        public GetPostsResponse(List<Post> posts)
        {
            Posts = posts;
        }
    }
    public sealed class GetPostRequest : Request { }
   


    public class GetPost : UseCaseBase<GetPostsResponse>
    {
        GetPostDataManager postDataManager = new GetPostDataManager();
        public GetPostRequest Request { get;private set; }
        public GetPost(GetPostRequest request, IGetPostsPresenterCallback callback) : base(callback)
        {
            Request = request;
            GetPostDataManager postDataManager = new GetPostDataManager();
        }
        protected override void Action()
        {
            
            postDataManager.GetPosts(new UseCaseCallback(this));
            
        }
        public class UseCaseCallback : CallbackBase<GetPostsResponse>
        {
            private GetPost UseCase;
            public UseCaseCallback(GetPost useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetPostsResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }



}
