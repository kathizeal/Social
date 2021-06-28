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
        User CurrentUser;
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register("_CurrentUser", typeof(User), typeof(PostViewControl), new PropertyMetadata(null));
        public User _CurrentUser
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        

        public static readonly DependencyProperty PostProperty = DependencyProperty.Register("_CurrentPost", typeof(Post), typeof(PostViewControl), new PropertyMetadata(null));
        public Post _CurrentPost
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
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        Comment _CurrentComment;
        private ObservableCollection<Comment> _PostComment;
        public ObservableCollection<Comment> PostComment { get { return this._PostComment; }  }
        private ObservableCollection<UserIds> _LikedUser;
        public ObservableCollection<UserIds> LikedUser { get { return this._LikedUser; } }
        public PostViewControl()
        {
            this.InitializeComponent();
        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            if (_CurrentPost.LikedId.Contains(_CurrentUser.UserId))
            {

                // _PostManager.UnLikePost(_CurrentPost, _CurrentUser);
                var UnLikePostRequest = new UnLikePostRequest(_CurrentPost, _CurrentUser);
                UnLikePost unLikePost = new UnLikePost(UnLikePostRequest, new UnLikePostPresenterCallback(this));
                unLikePost.Execute();
                foreach(var id in _LikedUser)
                {
                    if (id.Userid == _CurrentUser.UserId)
                    {
                        _LikedUser.Remove(id);
                        break;
                    }

                }
                LikeButton.IsChecked = false;
                if (_CurrentPost.Likes == 0)
                {
                    CommentTextBox.Width = 700;
                    LikeCount.Visibility = Visibility.Collapsed;
                }
               
            }
            else
            {
                UserIds userIds = new UserIds();
                userIds.PostId = _CurrentPost.PostId;
                userIds.Userid = _CurrentUser.UserId;
                userIds.UserName = _CurrentUser.UserName;
                //_PostManager.LikePost(_CurrentPost, _CurrentUser);
                var LikePostRequest = new LikePostRequest(_CurrentPost, _CurrentUser);
                LikePost likePost = new LikePost(LikePostRequest, new LikePostPresenterCallback(this));
                likePost.Execute();
                _LikedUser.Add(userIds);
            }
            LikeCount.Content = _CurrentPost.Likes;
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
                _CurrentUser.ProfilePic = _UserManager.ProfilePic(_CurrentUser);
                newComment.ProfilePic = _CurrentUser.ProfilePic;
                newComment.ParentCommentId = null;
                newComment.PostId = _CurrentPost.PostId;
                newComment.CommentContent = CommentTextBox.Text;
                newComment.CommenterName = _CurrentUser.UserName;
                newComment.CommenterId = _CurrentUser.UserId;
              //  _PostManager.AddComment(_CurrentPost, newComment);
                var AddCommentRequest = new AddCommentRequest(_CurrentPost, newComment);
                AddComment addComment = new AddComment(AddCommentRequest, new AddCommentPresenterCallback(this));
                addComment.Execute();
               // _PostComment.Add(newComment);
              //  _PostComment = _PostManager.DateChangeComment(_PostComment);
                CommentStack.Visibility = Visibility.Visible;
                CommentTextBox.Text = "";
                CommentTB.Text = "Comments : " + _CurrentPost.CommentCount.ToString();
            }
        }
        private void PostDetails_Loaded(object sender, RoutedEventArgs e)
        {
          
            _PostComment = PostComments;
            CurrentUser = _CurrentUser;
            var getLikedUserRequest = new GetLikedUsersRequest(_CurrentPost);
            GetLikedUsers getLikedUsers = new GetLikedUsers(getLikedUserRequest, new GetLikedUsersPresenterCallback(this));
            getLikedUsers.Execute();
            if (_CurrentPost.Likes != 0)
            {
                foreach (var i in _CurrentPost.LikedId)
                {
                    if (i == _CurrentUser.UserId)
                        LikeButton.IsChecked = true;
                }
            }
            Content.Text = _CurrentPost.PostContent;
            Heading.Text = _CurrentPost.PostTitle;
            TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            time.Text= TimeZoneInfo.ConvertTimeFromUtc(_CurrentPost.CreatedTime, localZoneId).ToString("dd/MM/yyyy hh:mm tt");
            CommentTB.Text = "Comments : " + _CurrentPost.CommentCount.ToString();
            Createdby.Text = "Created By: " + _CurrentPost.PostCreatedByUserName;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(_CurrentPost.ProfilePic));
            Icon.ImageSource = img.Source;
            
            if (_CurrentPost.Comments.Count == 0)
            {
                CommentStack.Visibility = Visibility.Collapsed;
            }
            else
            {
                CommentStack.Visibility = Visibility.Visible;
            }
            if (_CurrentPost.Likes == 0)
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
                LikeCount.Content = _CurrentPost.Likes;

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
                    presenter._LikedUser = new ObservableCollection<UserIds>(response.Obj.LikedUsers);
                    presenter.LikedUserList.ItemsSource = presenter._LikedUser;
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
                    presenter._PostComment.Add(response.Obj.Comment);
                });
            }
        }

    }
}
