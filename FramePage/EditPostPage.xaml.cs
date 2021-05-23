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
using Social.Data;
using Social.Model;
using Newtonsoft.Json;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /* public sealed partial class EditPostPage : Page
     {
         Post NewPost;
         Post EditingPost;
         User CurrentUser;

         PostManager postManager = PostManager.GetInstance();
         public EditPostPage()
         {
             this.InitializeComponent();
         }
         protected override void OnNavigatedTo(NavigationEventArgs e)
         {
             base.OnNavigatedTo(e);
             EditingPost = (Post)e.Parameter;
             NewPost = (Post)e.Parameter;
             TitleBox.Text = NewPost.PostTitle;
             ContentBox.Text = NewPost.PostContent;
             UserManager userManager = UserManager.GetInstance();

             CurrentUser = userManager.Current();
         }

         private void CancelButton_Click(object sender, RoutedEventArgs e)
         {
             postManager.AddPost(EditingPost);
            //MainPage.MainFramePage.Navigate(typeof(PostPage));
            Frame.Navigate(typeof(PostPage));
         }

         private void EditButton_Click(object sender, RoutedEventArgs e)
         {
             NewPost.Comments = EditingPost.Comments;
             NewPost.PostCreatedByUserId = EditingPost.PostCreatedByUserId;
             NewPost.PostCreatedByUserName = EditingPost.PostCreatedByUserName;
             NewPost.PostId = EditingPost.PostId;
             NewPost.CreatedTime = EditingPost.CreatedTime;
             NewPost.PostContent = ContentBox.Text;
             NewPost.PostTitle = TitleBox.Text ;
             NewPost.Likes = EditingPost.Likes;
             NewPost.LikedId = EditingPost.LikedId;



             postManager.DeletePost(EditingPost);
             postManager.AddPost(NewPost);
             //MainPage.MainFramePage.Navigate(typeof(AccountPage),CurrentUser);
             Frame.Navigate(typeof(AccountPage), CurrentUser);



         }
     }*/
    public sealed partial class EditPostPage : Page
    {
        Post _NewPost;
        Post _EditingPost;
        User _CurrentUser;
        PostManager postManager = PostManager.GetInstance();
        public EditPostPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _EditingPost = (Post)e.Parameter;
            _NewPost = (Post)e.Parameter;
            TitleBox.Text = _NewPost.PostTitle;
            ContentBox.Text = _NewPost.PostContent;
            UserManager userManager = UserManager.GetInstance();
            _CurrentUser =userManager.Current();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            postManager.AddPost(_EditingPost);
            Frame.GoBack();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _NewPost.Comments = _EditingPost.Comments;
            _NewPost.PostCreatedByUserId = _EditingPost.PostCreatedByUserId;
            _NewPost.PostCreatedByUserName = _EditingPost.PostCreatedByUserName;
            _NewPost.PostId = _EditingPost.PostId;
            _NewPost.CreatedTime = _EditingPost.CreatedTime;
            _NewPost.PostContent = ContentBox.Text;
            _NewPost.PostTitle = TitleBox.Text;
            _NewPost.Likes = _EditingPost.Likes;
            _NewPost.LikedId = _EditingPost.LikedId;
            postManager.UpdateEdit(_NewPost);
            Frame.GoBack();
        }
    }
}
