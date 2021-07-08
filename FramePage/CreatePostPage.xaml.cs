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
using Social.Domain;
using Social.Util;
using Windows.UI.WindowManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreatePostPage : Page
    {
        private User _currentUser;
        public AppWindow MyAppWindow { get; set; }
        // PostManager _PostManager = PostManager.GetInstance();
        // UserManager _UserManager = UserManager.GetInstance();
        public CreatePostPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _currentUser = (User)e.Parameter;
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
                newPost.ProfilePic = _currentUser.ProfilePic;
                newPost.PostTitle = TitleBox.Text;
                newPost.PostContent = ContentBox.Text;
                newPost.PostCreatedByUserName = _currentUser.UserName;
                newPost.PostCreatedByUserId = _currentUser.UserId;
                AddPostRequest addPostRequest = new AddPostRequest(newPost);
                AddPost addPost = new AddPost(addPostRequest, null);
                addPost.Execute();
                //this.Frame.Visibility = Visibility.Collapsed;
                this.Frame.Navigate(typeof(SecondaryPage), newPost);
            }
        }
        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Visibility = Visibility.Collapsed;
             await MyAppWindow.CloseAsync();
        }
        public class AddPostPresenterCallback : IAddPostPresenterCallback
        {
            CreatePostPage presenter;

            public AddPostPresenterCallback(CreatePostPage view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public void OnSuccess(Response<AddPostResponse> response)
            {

            }
        }
    }
}
