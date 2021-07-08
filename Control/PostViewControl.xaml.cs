using Social.Data;
using Social.Domain;
using Social.FramePage;
using Social.Model;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Social.Control
{
    public sealed partial class PostViewControl : UserControl 
    {
        private User _currentUser;
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register("CurrentUser", typeof(User), typeof(PostViewControl), new PropertyMetadata(null));
        public User CurrentUser
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        

        public static readonly DependencyProperty PostProperty = DependencyProperty.Register("CurrentPost", typeof(Post), typeof(PostViewControl), new PropertyMetadata(null));
        public Post CurrentPost
        {
            get { return (Post)GetValue(PostProperty); }
            set { SetValue(PostProperty, value); }
        }
       public static readonly DependencyProperty PostCommentProp = DependencyProperty.Register("PostComments", typeof(ObservableCollection<Comment>), typeof(PostViewControl), new PropertyMetadata(null));
        public ObservableCollection<Comment> PostComments
        {
            get { return (ObservableCollection<Comment>)GetValue(PostCommentProp); }
            set { SetValue(PostCommentProp, value); }
        }
      //  PostManager _PostManager = PostManager.GetInstance();
        //UserManager _UserManager = UserManager.GetInstance();
        Comment _CurrentComment;
        private ObservableCollection<Comment> _postComment;
        public ObservableCollection<Comment> PostComment { get { return this._postComment; }  }
        private ObservableCollection<UserIds> _likedUser;
        public ObservableCollection<UserIds> LikedUser { get { return this._likedUser; } }
        public PostViewControl()
        {
            this.InitializeComponent();
        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            if (CurrentPost.LikedId.Contains(CurrentUser.UserId))
            {

                // _PostManager.UnLikePost(_CurrentPost, _CurrentUser);
                var UnLikePostRequest = new UnLikePostRequest(CurrentPost, CurrentUser);
                UnLikePost unLikePost = new UnLikePost(UnLikePostRequest, null);
                unLikePost.Execute();
                foreach(var id in _likedUser)
                {
                    if (id.Userid == CurrentUser.UserId)
                    {
                        _likedUser.Remove(id);
                        break;
                    }

                }
                LikeButton.IsChecked = false;
                if (CurrentPost.Likes == 0)
                {
                    CommentTextBox.Width = 700;
                    LikeCount.Visibility = Visibility.Collapsed;
                }
               
            }
            else
            {
                UserIds userIds = new UserIds();
                userIds.PostId = CurrentPost.PostId;
                userIds.Userid = CurrentUser.UserId;
                userIds.UserName = CurrentUser.UserName;
                //_PostManager.LikePost(_CurrentPost, _CurrentUser);
                var LikePostRequest = new LikePostRequest(CurrentPost, CurrentUser);
                LikePost likePost = new LikePost(LikePostRequest, null);
                likePost.Execute();
                _likedUser.Add(userIds);
            }
            LikeCount.Content = CurrentPost.Likes;
        }
        private void CommentList_ItemClick(object sender, ItemClickEventArgs e)
        {
           
          
           CommentButton.Visibility = Visibility.Collapsed;
          _CurrentComment = (Comment)e.ClickedItem;

        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                Comment newComment = new Comment();
                var getProfilePicRequest = new GetProfilePicRequest(CurrentUser);
                GetProfilePic getProfilePic = new GetProfilePic(getProfilePicRequest, new GetProfilePicPresenterCallback(this));
                getProfilePic.Execute();
                //CurrentUser.ProfilePic = _UserManager.ProfilePic(_CurrentUser);
                newComment.ProfilePic = CurrentUser.ProfilePic;
                newComment.ParentCommentId = null;
                newComment.PostId = CurrentPost.PostId;
                newComment.CommentContent = CommentTextBox.Text;
                newComment.CommenterName = CurrentUser.UserName;
                newComment.CommenterId = CurrentUser.UserId;
              //  _PostManager.AddComment(_CurrentPost, newComment);
                var AddCommentRequest = new AddCommentRequest(CurrentPost, newComment);
                // AddComment addComment = new AddComment(AddCommentRequest, new AddCommentPresenterCallback(this));
                AddComment addComment = new AddComment(AddCommentRequest,null);
                addComment.Execute();
               _postComment.Add(newComment);
              //  _PostComment = _PostManager.DateChangeComment(_PostComment);
                CommentStack.Visibility = Visibility.Visible;
                CommentTextBox.Text = "";
                CommentTB.Text = "Comments : " + CurrentPost.CommentCount.ToString();
            }
        }
        private void PostDetails_Loaded(object sender, RoutedEventArgs e)
        {
          
            _postComment = PostComments;
            _currentUser = CurrentUser;
            var getProfilePicRequest = new GetProfilePicRequest(CurrentUser);
            GetProfilePic getProfilePic = new GetProfilePic(getProfilePicRequest, new GetProfilePicPresenterCallback(this));
            getProfilePic.Execute();
            var getLikedUserRequest = new GetLikedUsersRequest(CurrentPost);
            GetLikedUsers getLikedUsers = new GetLikedUsers(getLikedUserRequest, new GetLikedUsersPresenterCallback(this));
            getLikedUsers.Execute();
            if (CurrentPost.Likes != 0)
            {
                foreach (var i in CurrentPost.LikedId)
                {
                    if (i == CurrentUser.UserId)
                        LikeButton.IsChecked = true;
                }
            }
            Content.Text = CurrentPost.PostContent;
            Heading.Text = CurrentPost.PostTitle;
            TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            time.Text= TimeZoneInfo.ConvertTimeFromUtc(CurrentPost.CreatedTime, localZoneId).ToString("dd/MM/yyyy hh:mm tt");
            CommentTB.Text = "Comments : " + CurrentPost.CommentCount.ToString();
            Createdby.Text = "Created By: " + CurrentPost.PostCreatedByUserName;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(CurrentPost.ProfilePic));
            Icon.ImageSource = img.Source;
            
            if (CurrentPost.Comments.Count == 0)
            {
                CommentStack.Visibility = Visibility.Collapsed;
            }
            else
            {
                CommentStack.Visibility = Visibility.Visible;
            }
            if (CurrentPost.Likes == 0)
            {
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                if (width > 700)
                    CommentTextBox.Width = 700;
                else
                    CommentTextBox.Width = 310;
                LikeCount.Content = "0";
            }
            else
            {
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                if (width > 700)
                    CommentTextBox.Width = 675;
                else
                    CommentTextBox.Width = 280;
                LikeCount.Visibility = Visibility.Visible;
                LikeCount.Content = CurrentPost.Likes;

            }
        }
        public class GetProfilePicPresenterCallback : IGetProfilePicPresenterCallback
        {
            PostViewControl presenter;

            public GetProfilePicPresenterCallback(PostViewControl view)
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
                    presenter.CurrentUser.ProfilePic = response.Obj.ProfilePic;

                });
            }
        }
        public class GetLikedUsersPresenterCallback : IGetLikedUsersPresenterCallback
        {
            PostViewControl presenter;
            public GetLikedUsersPresenterCallback(PostViewControl view )
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetLikedUsersResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._likedUser = new ObservableCollection<UserIds>(response.Obj.LikedUsers);
                    presenter.LikedUserList.ItemsSource = presenter._likedUser;
                });
            }
        }
        public class LikePostPresenterCallback : ILikePostPresenterCallback
        {
            PostViewControl presenter;
            public LikePostPresenterCallback(PostViewControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<LikePostResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                   
                });
            }
        }
        public class UnLikePostPresenterCallback : IUnLikePostPresenterCallback
        {
            PostViewControl presenter;
            public UnLikePostPresenterCallback(PostViewControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<UnLikePostResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                });
            }
        }
        public class AddCommentPresenterCallback : IAddCommentPresenterCallback
        {
            PostViewControl presenter;
            public AddCommentPresenterCallback(PostViewControl view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<AddCommentResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    //presenter._PostComment.Add(response.Obj.Comment);
                });
            }
        }

    }
}
