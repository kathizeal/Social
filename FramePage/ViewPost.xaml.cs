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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPost : Page
    {
        private Post _currentPost;
        private User _currentUser;
        private Comment _CurrentComment;
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        private ObservableCollection<Comment> _postComments;
        public ObservableCollection<Comment> PostComments { get { return this._postComments; } }
        private ObservableCollection<UserIds> _likedUser;
        public ObservableCollection<UserIds> LikedUser { get { return this._likedUser; } }
        public ViewPost()
        {
            this.InitializeComponent();
            _currentUser =_UserManager.Current();
          
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentPost = (Post)e.Parameter;
            _postComments = new ObservableCollection<Comment>(_currentPost.Comments);
            // _PostComments = _PostManager.DateChangeComment(_PostComments);
            //_LikedUser = new ObservableCollection<UserIds>(_PostManager.LikedUsers(_CurrentPost));
            var getLikedUserRequest = new GetLikedUsersRequest(_currentPost);
            GetLikedUsers getLikedUsers = new GetLikedUsers(getLikedUserRequest, new GetLikedUsersPresenterCallback(this));
            getLikedUsers.Execute();
            if (_currentPost.Likes != 0)
            {
                foreach (var i in _currentPost.LikedId)
                {
                    if (i == _currentUser.UserId)
                        LikeButton.IsChecked = true;

                }
            }
            Content.Text = _currentPost.PostContent;
            CommentTB.Text = "Comments : " + _currentPost.CommentCount.ToString();
            Heading.Text = _currentPost.PostTitle;
            TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            time.Text = TimeZoneInfo.ConvertTimeFromUtc(_currentPost.CreatedTime, localZoneId).ToString("dd/MM/yyyy hh:mm tt");
            Createdby.Text = "Created By: " + _currentPost.PostCreatedByUserName;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(_currentPost.ProfilePic));
            Icon.ImageSource = img.Source;
            if (_currentPost.Comments.Count == 0)
            {
                CommentStack.Visibility = Visibility.Collapsed;
            }
            else
            {
                CommentStack.Visibility = Visibility.Visible;
            }
            if (_currentPost.Likes == 0)
            {
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                if (width > 700)
                    CommentTextBox.Width = 700;
                else
                    CommentTextBox.Width = 330;
                LikeCount.Content = "0";
            }
            else
            {
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                if (width > 700)
                    CommentTextBox.Width = 675;
                else
                    CommentTextBox.Width = 300;
                LikeCount.Visibility = Visibility.Visible;
                LikeCount.Content = _currentPost.Likes;

            }

        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            if (_currentPost.LikedId.Contains(_currentUser.UserId))
            {
                //_PostManager.UnLikePost(_CurrentPost, _CurrentUser);
                var UnLikePostRequest = new UnLikePostRequest(_currentPost, _currentUser);
                UnLikePost unLikePost = new UnLikePost(UnLikePostRequest, null);
                unLikePost.Execute();
                foreach (var id in _likedUser)
                {
                    if (id.Userid == _currentUser.UserId)
                    {
                        _likedUser.Remove(id);
                        break;
                    }

                }
                LikeButton.IsChecked = false;
                if (_currentPost.Likes == 0)
                {
                    CommentTextBox.Width = 700;
                    LikeCount.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                UserIds userIds = new UserIds();
                userIds.PostId = _currentPost.PostId;
                userIds.Userid = _currentUser.UserId;
                userIds.UserName = _currentUser.UserName;
                //_PostManager.LikePost(_CurrentPost, _CurrentUser);
                var LikePostRequest = new LikePostRequest(_currentPost, _currentUser);
                LikePost likePost = new LikePost(LikePostRequest, null);
                likePost.Execute();
                _likedUser.Add(userIds);

            }
            LikeCount.Content = _currentPost.Likes;
        }
        private void CommentList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                CommentButton.Visibility = Visibility.Collapsed;
                CommentTB.Text = "Comments : " + _currentPost.CommentCount.ToString();
                CommentTextBox.Focus(FocusState.Programmatic);
                _CurrentComment = (Comment)e.ClickedItem;
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                if (width > 700)
                    CommentTextBox.Width = 670;
                else
                    CommentTextBox.Width = 320;
            }
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                Comment newComment = new Comment();
                _currentUser.ProfilePic = _UserManager.ProfilePic(_currentUser);
                newComment.ProfilePic = _currentUser.ProfilePic;
                newComment.ParentCommentId = null;
                newComment.PostId = _currentPost.PostId;
                newComment.CommentContent = CommentTextBox.Text;
                newComment.CommenterName = _currentUser.UserName;
                newComment.CommenterId = _currentUser.UserId;
                //_PostManager.AddComment(_CurrentPost, newComment);
                var AddCommentRequest = new AddCommentRequest(_currentPost, newComment);
                AddComment addComment = new AddComment(AddCommentRequest,null);
                addComment.Execute();
                CommentStack.Visibility = Visibility.Visible;
                CommentTB.Text = "Comments : " + _currentPost.CommentCount.ToString();
               _postComments.Add(newComment);
                //_PostComments = _PostManager.DateChangeComment(_PostComments);
                CommentTextBox.Text = "";
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        /*private async void SampleDelete_Click(object sender, RoutedEventArgs e)
        {
            CommentList.SelectedItem = ((HyperlinkButton)sender).DataContext;
            MessageDialog showDialog = new MessageDialog("Do you want delete the Comment");
            showDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            showDialog.Commands.Add(new UICommand("No")
            {
                Id = 1
            });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                _CurrentComment = (Comment)CommentList.SelectedItem;
                _PostManager.RemoveComment(_CurrentPost, _CurrentComment);
                _PostComments.Remove(_CurrentComment);
                CommentTB.Text = "Comments : " + _CurrentPost.CommentCount.ToString();
                CommentButton.Visibility = Visibility.Visible;
            }
            
        }*/
        public class GetLikedUsersPresenterCallback : IGetLikedUsersPresenterCallback
        {
            ViewPost presenter;
            public GetLikedUsersPresenterCallback(ViewPost view)
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
            ViewPost presenter;
            public LikePostPresenterCallback(ViewPost view)
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
            ViewPost presenter;
            public UnLikePostPresenterCallback(ViewPost view)
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
            ViewPost presenter;
            public AddCommentPresenterCallback(ViewPost view)
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
                    //presenter._PostComments.Add(response.Obj.Comment);
                });
            }
        }


    }
}
