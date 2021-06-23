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
    public interface IGetLikedUsersPresenterCallback : ICallback<GetLikedUsersResponse> { }
    public sealed class GetLikedUsersResponse
    {
        public List<UserIds> LikedUsers;
        public GetLikedUsersResponse(List<UserIds> likedUsers)
        {
            LikedUsers = likedUsers;
        }
    }
    public sealed class GetLikedUsersRequest
    {

        public Post CurrentPost;
        public GetLikedUsersRequest(Post currentPost)
        {
           CurrentPost=currentPost;
        }
    }

    public class GetLikedUsers:UseCaseBase<GetLikedUsersResponse>
    {
        public GetLikedUsersRequest Request;
        GetLikedUserDataManager getLikedUserDataManager;
        public GetLikedUsers(GetLikedUsersRequest request, IGetLikedUsersPresenterCallback callback):base(callback)
        {
            Request = request;
            getLikedUserDataManager = new GetLikedUserDataManager();
        }

        protected override void Action()
        {
            getLikedUserDataManager.GetLiked(Request, new UseCaseCallback(this));
        }
        public class UseCaseCallback : CallbackBase<GetLikedUsersResponse>
        {
            private GetLikedUsers UseCase;
            public UseCaseCallback(GetLikedUsers useCase)
            {
                UseCase = useCase;
            }
            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<GetLikedUsersResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
