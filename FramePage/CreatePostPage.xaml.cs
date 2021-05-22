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
using System.Collections.ObjectModel;
using Social.Data;
using Social.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   /* public sealed partial class CreatePostPage : Page
    {
        User CurrentUser;
       
        PostManager postManager = PostManager.GetInstance();
        public CreatePostPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

           
            CurrentUser = (User)e.Parameter;
            

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            postManager.AddPost(new Post(TitleBox.Text, ContenBox.Text, CurrentUser.UserName, CurrentUser.UserId));
            // MainPage.MainFramePage.Navigate(typeof(PostPage));
            //Frame.Navigate(typeof(PostPage));
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // MainPage.MainFramePage.Navigate(typeof(PostPage));
            Frame.GoBack();
            //Frame.Navigate(typeof(PostPage));
        }
    }*/
    public sealed partial class CreatePostPage : Page
    {

       
        User _CurrentUser;
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        public CreatePostPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _CurrentUser = (User)e.Parameter;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) || string.IsNullOrWhiteSpace(ContentBox.Text))
            {
                TitleBox.PlaceholderText = "Not Should be empty";
                ContentBox.PlaceholderText= "Not Should be empty";
            }
                
            else
            {
                
                Post newPost = new Post();
                newPost.ProfilePic = _CurrentUser.ProfilePic;
                newPost.PostTitle = TitleBox.Text;
                newPost.PostContent = ContentBox.Text;
                newPost.PostCreatedByUserName = _CurrentUser.UserName;
                newPost.PostCreatedByUserId = _CurrentUser.UserId;
                _PostManager.AddPost(newPost);
                CreateHandler createHandler = CreateHandler.GetInstance();
                createHandler.UpdateStatus(newPost);
                this.Frame.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Visibility = Visibility.Collapsed;
        }
    }
}
