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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPost : Page
    {
        Post CurrentPost;
        User CurrentUser;
        Comment CurrentComment;
        PostManager postManager = PostManager.GetInstance();
        UserManager userManager = UserManager.GetInstance();
        private ObservableCollection<Comment> _PostComments;
        public ObservableCollection<Comment> PostComments { get { return this._PostComments; } }
        public ViewPost()
        {
            this.InitializeComponent();
            CurrentUser = userManager.Current();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            CurrentPost = (Post)e.Parameter;
            _PostComments = new ObservableCollection<Comment>(CurrentPost.Comments);
            if (CurrentPost.Likes != 0)
            {
                foreach (var i in CurrentPost.LikedId)
                {
                    if (i == CurrentUser.UserId)
                        LikeButton.Background = (SolidColorBrush)Resources["BlueColor"];


                }
            }
            Content.Text = CurrentPost.PostContent;
            /*Title.Text = CurrentPost.PostTitle;*/
            Heading.Text = CurrentPost.PostTitle;
            time.Text = CurrentPost.CreatedTime.ToString();
            Createdby.Text = "Created By: " + CurrentPost.PostCreatedByUserName;
            if (CurrentPost.LikedId == null)
                LikeCount.Content = "0";
            else
                LikeCount.Content = CurrentPost.Likes;
            if (CurrentPost.Comments.Count == 0)
            {
                CommentStack.Visibility = Visibility.Collapsed;

            }
            else
            {
                CommentStack.Visibility = Visibility.Visible;

            }


        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            postManager.LikePost(CurrentPost, CurrentUser);
            LikeCount.Content = CurrentPost.Likes;


        }
        private void CommentList_ItemClick(object sender, ItemClickEventArgs e)
        {



            ReplyButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            CommentButton.Visibility = Visibility.Collapsed;

            CommentTextBox.Focus(FocusState.Programmatic);
            CurrentComment = (Comment)e.ClickedItem;

        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            /*  SecondRow.Height = new GridLength(550);
              ThirdRow.Height = new GridLength(200);
             CommentTextBox.Height = 180;
              DragDownButton.Visibility = Visibility.Visible;*/
            Comment newComment = new Comment();
            newComment.ParentCommentId = null;
            newComment.PostId = CurrentPost.PostId;
            newComment.CommentContent = CommentTextBox.Text;
            newComment.CommenterName = CurrentUser.UserName;
            newComment.CommenterId = CurrentUser.UserId;
            postManager.AddComment(CurrentPost, newComment);
            //postManager.AddComment(CurrentPost, new Comment(CurrentPost.PostId, CommentTextBox.Text, CurrentUser.UserName, CurrentUser.UserId, null));
            CommentStack.Visibility = Visibility.Visible;
            this.Frame.Navigate(typeof(SecondaryPage), CurrentPost);
        }
        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            //MainPage.postManager.AddReply(CurrentPost, CurrentComment.CommentId, new Comment(CurrentPost.PostId, CommentTextBox.Text, CurrentUser.UserName, CurrentUser.UserId, CurrentComment.CommentId));
            Comment newReply = new Comment();
            newReply.PostId = CurrentPost.PostId;
            newReply.CommentContent = CommentTextBox.Text;
            newReply.CommenterName = CurrentUser.UserName;
            newReply.CommenterId = CurrentUser.UserId;
            newReply.ParentCommentId = CurrentComment.CommentId;
            postManager.AddReply(CurrentPost, CurrentComment, newReply);
            //postManager.AddReply(CurrentPost, CurrentComment, new Comment(CurrentPost.PostId, CommentTextBox.Text, CurrentUser.UserName, CurrentUser.UserId, null));


            this.Frame.Navigate(typeof(SecondaryPage), CurrentPost);

        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            postManager.RemoveComment(CurrentPost, CurrentComment);
            this.Frame.Navigate(typeof(SecondaryPage), CurrentPost);
        }
    }
}
