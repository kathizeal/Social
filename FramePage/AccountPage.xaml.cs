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
using Windows.Storage;
using System.Collections.ObjectModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        Post post;
        private ObservableCollection<Post> _MyPost;
        public ObservableCollection<Post> MyPost
        {
            get { return this._MyPost; }
        }
        User _CurrentUser;
        public AccountPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _CurrentUser = (User)e.Parameter;
            UserNameBlock.Text ="User name : "+ _CurrentUser.UserName;
            LastNameBlock.Text = "Last name : " + _CurrentUser.LastName;
            UserIDBlock.Text = "User Id : " + _CurrentUser.UserId.ToString();
            EmailBlock.Text = "Email address : " + _CurrentUser.Email;
            BirthdayBlock.Text = "DOB : " + _CurrentUser.BirthDay;
            GenderBlock.Text = "Gender : " + _CurrentUser.Gender;
            _MyPost = new ObservableCollection<Post>(MainPage.postManager.ViewMyPost(_CurrentUser.UserId));
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
            MainPage.MainFramePage.Navigate(typeof(MainPage));
        }

        

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainPage.postManager.DeletePost(post);
            Post SelectedPost = MainPage.postManager.Edit(post);

            PostPage.SecondaryFrame.Navigate(typeof(EditPostPage), SelectedPost);


        }

        private void MyPost_ItemClick(object sender, ItemClickEventArgs e)
        {
            post =(Post)e.ClickedItem;
            
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MainPage.postManager.DeletePost(post);
            MainPage.MainFramePage.Navigate(typeof(PostPage), _CurrentUser);
        }
    }
}
