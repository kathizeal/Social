using Social.Data;
using Social.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        Post _CurrentPost;
        User _CurrentUser;
        Comment _CurrentComment;
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        private ObservableCollection<Comment> _PostComments;
        public ObservableCollection<Comment> PostComments { get { return this._PostComments; } }
        private ObservableCollection<UserIds> _LikedUser;
        public ObservableCollection<UserIds> LikedUser { get { return this._LikedUser; } }
        public ViewPost()
        {
            this.InitializeComponent();
            _CurrentUser =_UserManager.Current();
          
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _CurrentPost = (Post)e.Parameter;
            _PostComments = new ObservableCollection<Comment>(_CurrentPost.Comments);
            _PostComments = _PostManager.DateChangeComment(_PostComments);
            _LikedUser = new ObservableCollection<UserIds>(_PostManager.LikedUsers(_CurrentPost));
            if (_CurrentPost.Likes != 0)
            {
                foreach (var i in _CurrentPost.LikedId)
                {
                    if (i == _CurrentUser.UserId)
                        LikeButton.IsChecked = true;

                }
            }
            Content.Text = _CurrentPost.PostContent;
            CommentTB.Text = "Comments : " + _CurrentPost.CommentCount.ToString();
            Heading.Text = _CurrentPost.PostTitle;
            TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            time.Text = TimeZoneInfo.ConvertTimeFromUtc(_CurrentPost.CreatedTime, localZoneId).ToString("dd/MM/yyyy hh:mm tt");
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
                LikeCount.Content = _CurrentPost.Likes;

            }

        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            if (_CurrentPost.LikedId.Contains(_CurrentUser.UserId))
            {
               

                UserIds userIds = new UserIds();
                userIds.PostId = _CurrentPost.PostId;
                userIds.Userid = _CurrentUser.UserId;
                userIds.UserName = _CurrentUser.UserName;

                _PostManager.UnLikePost(_CurrentPost, _CurrentUser);
                if (_LikedUser.Contains(userIds))
                    _LikedUser.Remove(userIds);
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
                _PostManager.LikePost(_CurrentPost, _CurrentUser);
                _LikedUser.Add(userIds);

            }
            LikeCount.Content = _CurrentPost.Likes;
        }
        private void CommentList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                CommentButton.Visibility = Visibility.Collapsed;
                CommentTB.Text = "Comments : " + _CurrentPost.CommentCount.ToString();
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
                _CurrentUser.ProfilePic = _UserManager.ProfilePic(_CurrentUser);
                newComment.ProfilePic = _CurrentUser.ProfilePic;
                newComment.ParentCommentId = null;
                newComment.PostId = _CurrentPost.PostId;
                newComment.CommentContent = CommentTextBox.Text;
                newComment.CommenterName = _CurrentUser.UserName;
                newComment.CommenterId = _CurrentUser.UserId;
                _PostManager.AddComment(_CurrentPost, newComment);
                CommentStack.Visibility = Visibility.Visible;
                CommentTB.Text = "Comments : " + _CurrentPost.CommentCount.ToString();
                _PostComments.Add(newComment);
                _PostComments = _PostManager.DateChangeComment(_PostComments);
                CommentTextBox.Text = "";
            }
        }
     

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

       

    

        private async void SampleDelete_Click(object sender, RoutedEventArgs e)
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
            
        }

        
    }
}
