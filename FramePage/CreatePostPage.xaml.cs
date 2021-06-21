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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                AddPostRequest addPostRequest = new AddPostRequest(newPost);
                AddPost addPost = new AddPost(addPostRequest, new AddPostPresenterCallback(this));
                addPost.Execute();
                this.Frame.Visibility = Visibility.Collapsed;
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Visibility = Visibility.Collapsed;
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
