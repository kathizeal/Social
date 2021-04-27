using System;
using System.Collections.Generic;
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
using Social.Model;
using Social.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondaryPage : Page
    {
        /*public Model.Post Post { get { return this.DataContext as Model.Post; } }*/
        Post CurrentPost;
        User CurrentUser;
        Comment CurrentComment;


        private ObservableCollection<Comment> _PostComments;
        public ObservableCollection<Comment> PostComments { get { return this._PostComments; } }
        public SecondaryPage()
        {
            this.InitializeComponent();
            object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());
            CurrentUser = user;
            /**/


        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            CurrentPost = (Post)e.Parameter;
            if (CurrentPost.Likes != 0)
            {
                foreach (var i in CurrentPost.LikedId)
                {
                    if (i == CurrentUser.UserId)
                        LikeButton.Background = (SolidColorBrush)Resources["BlueColor"];


                }
            }
            
            _PostComments = new ObservableCollection<Comment>(CurrentPost.Comments);
            
            Content.Text = CurrentPost.PostContent;
            Title.Text = CurrentPost.PostTitle;
            Heading.Text = CurrentPost.PostTitle;
            time.Text = CurrentPost.CreatedTime.ToString();
            Createdby.Text = "Created By: " + CurrentPost.PostCreatedByUserName;
            if (CurrentPost.LikedId == null)
                LikeCount.Content = "Likes :0";
            else
                LikeCount.Content = "Likes :" + CurrentPost.Likes;
            if (CurrentPost.Comments.Count == 0)
            {
                CommentStack.Visibility = Visibility.Collapsed;

            }
            else
            {
                CommentStack.Visibility = Visibility.Visible;

            }








        }

        /* private void CommandTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
         {

            SecondRow.Height = new GridLength(550);
             ThirdRow.Height = new GridLength(200);
             CommandTextBox.Height = 180;
             DragDownButton.Visibility = Visibility.Visible;

         }*/

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            MainPage.postManager.LikePost(CurrentPost, CurrentUser);
            LikeCount.Content = "Likes :" + CurrentPost.Likes;


        }

        private void DragDownButton_Click(object sender, RoutedEventArgs e)
        {
            DragDownButton.Visibility = Visibility.Collapsed;
            SecondRow.Height = new GridLength(650);
            ThirdRow.Height = new GridLength(35);
            CommentTextBox.Height = 35;

        }

        private void CommentTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            SecondRow.Height = new GridLength(550);
            ThirdRow.Height = new GridLength(200);
            CommentTextBox.Height = 180;
            DragDownButton.Visibility = Visibility.Visible;
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            SecondRow.Height = new GridLength(550);
            ThirdRow.Height = new GridLength(200);
            CommentTextBox.Height = 180;
            DragDownButton.Visibility = Visibility.Visible;
            MainPage.postManager.AddComment(CurrentPost, new Comment(CurrentPost.PostId, CommentTextBox.Text, CurrentUser.UserName, CurrentUser.UserId, null));
            CommentStack.Visibility = Visibility.Visible;
            this.Frame.Navigate(typeof(SecondaryPage), CurrentPost);
        }

        private void CommentList_ItemClick(object sender, ItemClickEventArgs e)
        {



            ReplyButton.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Visible;
            CommentButton.Visibility = Visibility.Collapsed;

            CommentTextBox.Focus(FocusState.Programmatic);
            CurrentComment = (Comment)e.ClickedItem;

        }

       private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            //MainPage.postManager.AddReply(CurrentPost, CurrentComment.CommentId, new Comment(CurrentPost.PostId, CommentTextBox.Text, CurrentUser.UserName, CurrentUser.UserId, CurrentComment.CommentId));
            MainPage.postManager.AddReply(CurrentPost, CurrentComment, new Comment(CurrentPost.PostId, CommentTextBox.Text, CurrentUser.UserName, CurrentUser.UserId, null));
            

            this.Frame.Navigate(typeof(SecondaryPage), CurrentPost);

        }

       /* private ObservableCollection<Comment> Reply(long id)
        {
            ObservableCollection<Comment> CurrentReply = new ObservableCollection<Comment>();
            foreach(var i in PostComments)
            {
                if(i.ParentCommentId==id)
                {
                    CurrentReply.Add(i);
                }
            }
          
            return;
        }*/
        





        /*private void CommentList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Comment currentComment = (Comment)e.ClickedItem;
        }*/
    }

    
   
}
