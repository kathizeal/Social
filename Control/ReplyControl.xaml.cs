using Social.Data;
using Social.Domain;
using Social.Model;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Social.Control
{
    public sealed partial class ReplyControl : UserControl
    {
        //PostManager _PostManager = PostManager.GetInstance();
        //UserManager _UserManager = UserManager.GetInstance();
        private User _currentUser;
        private Post _currentPost;
        private Comment _currentComment;

        public static readonly DependencyProperty ParentIdProp = DependencyProperty.Register("ParentId", typeof(long), typeof(ReplyControl), new PropertyMetadata(null));
        public long ParentId
        {
            get { return (long)GetValue(ParentIdProp); }
            set { SetValue(ParentIdProp, value); }
        }
        private ObservableCollection<Comment> _postComment;
        public ObservableCollection<Comment> PostComment { get { return this._postComment; } }
        public ReplyControl()
        {
            this.InitializeComponent();
          
        }
        private void ReplyStack_Loaded(object sender, RoutedEventArgs e)
        {

            //_CurrentComment = _PostManager.GetComment(ParentId);
            var GetCommentRequest = new GetCommentRequest(ParentId);
           GetComment GetComment = new GetComment(GetCommentRequest, new GetCommentPresenterCallback(this));
            GetComment.Execute();
            //  _PostComment = _PostManager.GetReply(_CurrentComment.CommentId);
            // _PostComment = _PostManager.DateChangeComment(_PostComment);

            // _CurrentUser = _UserManager.Current();
            // _CurrentUser = _UserManager.Find(_CurrentUser.UserId);
            //  _CurrentPost = _PostManager.ViewPost(_CurrentComment.PostId);
            GetCurrentUserRequest getCurrentUserRequest = new GetCurrentUserRequest();
            GetCurrentUser getCurrentUser = new GetCurrentUser(getCurrentUserRequest, new GetCurrentUserPresenterCallback(this));
            getCurrentUser.Execute();
            //_CurrentUser.ProfilePic = _UserManager.ProfilePic(_CurrentUser);
            ReplyStack.Visibility = Visibility.Visible;
               
           
            
        }
        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                Comment newReply = new Comment();
                newReply.ProfilePic = _currentUser.ProfilePic;
                newReply.PostId = _currentComment.PostId;
                newReply.CommentContent = CommentTextBox.Text;
                newReply.CommenterName = _currentUser.UserName;
                newReply.CommenterId = _currentUser.UserId;
                newReply.ParentCommentId = _currentComment.CommentId;
                _currentComment.CurrentReply = newReply;
                _postComment.Add(newReply);
                // _PostManager.AddReply(_CurrentPost, _CurrentComment, newReply);
                var addReplyRequest = new AddReplyRequest(newReply);
                AddReply addReply = new AddReply(addReplyRequest, null);
                addReply.Execute();
                //_PostComment = _PostManager.DateChangeComment(_PostComment);
                CommentTextBox.Text = "";
               
            }
        }
        public class GetCommentPresenterCallback : IGetCommentPresenterCallback
        {
            ReplyControl presenter;
            public GetCommentPresenterCallback(ReplyControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetCommentResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._currentComment = response.Obj.Comment;
                    var GetReplyRequest = new GetReplyRequest(response.Obj.Comment.CommentId);
                    GetReply getReply = new GetReply(GetReplyRequest, new GetReplyPresenterCallback(presenter));
                    getReply.Execute();
                    var ViewPostRequest = new ViewPostRequest(response.Obj.Comment.PostId);
                    ViewPost viewPost = new ViewPost(ViewPostRequest, new ViewPostPresenterCallback(presenter));
                    viewPost.Execute();
                   // presenter._PostComment = presenter._PostManager.GetReply(response.Obj.Comment.CommentId);
                   //presenter._CurrentPost = presenter._PostManager.ViewPost(response.Obj.Comment.PostId);
                });
            }
        }
        public class ViewPostPresenterCallback : IViewPostPresenterCallback
        {
            ReplyControl presenter;

            public ViewPostPresenterCallback(ReplyControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<ViewPostResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    presenter._currentPost = response.Obj.Post;
                });
            }
        }
        public class GetReplyPresenterCallback : IGetReplyPresenterCallback
        {
            ReplyControl presenter;
            public GetReplyPresenterCallback(ReplyControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetReplyResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._postComment = new ObservableCollection<Comment>(response.Obj.Replys);
                    presenter.ReplyList.ItemsSource = presenter._postComment;
                });
            }
        }
        public class AddReplyPresenterCallback : IAddReplyPresenterCallback
        {
            ReplyControl presenter;
            public AddReplyPresenterCallback(ReplyControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<AddReplyResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                  
                });
            }
        }
        public class GetCurrentUserPresenterCallback : IGetCurrentUserPresenterCallback
        {

            ReplyControl presenter;
            public GetCurrentUserPresenterCallback(ReplyControl view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetCurrentUserResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._currentUser = response.Obj.CurrentUser;
                    var getProfilePicRequest = new GetProfilePicRequest(presenter._currentUser);
                    GetProfilePic getProfilePic = new GetProfilePic(getProfilePicRequest, new GetProfilePicPresenterCallback(presenter));
                    getProfilePic.Execute();
                  //  presenter._CurrentUser.ProfilePic = presenter._UserManager.ProfilePic(presenter._CurrentUser);

                });
            }


        }
        public class GetProfilePicPresenterCallback : IGetProfilePicPresenterCallback
        {
            ReplyControl presenter;

            public GetProfilePicPresenterCallback(ReplyControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetProfilePicResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._currentUser.ProfilePic = response.Obj.ProfilePic;

                });
            }
        }
    }
}
