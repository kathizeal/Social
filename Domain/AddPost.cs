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
    public interface IAddPostPresenterCallback : ICallback<AddPostResponse> { }
    public sealed class AddPostResponse
    {
        public Post Post;
        public AddPostResponse(Post post)
        {
            Post = post;
        }
    
    }
    public class AddPostRequest
    {
        public Post Post;
        public AddPostRequest(Post post)
        {
            Post = post;
        }
    }

    public class AddPost : UseCaseBase<AddPostResponse>
    {
       
        
        public AddPostRequest Request;
        AddPostDataManager addPostDataManager = new AddPostDataManager();
        public AddPost(AddPostRequest request, IAddPostPresenterCallback callback) : base(callback)
        {
            Request = request;
            AddPostDataManager addPostDataManager = new AddPostDataManager();

        }

        protected override void Action()
        {
            addPostDataManager.AddPost(Request,new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<AddPostResponse>
        {
            private AddPost UseCase;
            public UseCaseCallback(AddPost useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<AddPostResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
