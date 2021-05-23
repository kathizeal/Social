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
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage.AccessCache;
using Windows.UI.Popups;
using Windows.Storage.Streams;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class AccountPage : Page
    {
        Post _Post;
        User _CurrentUser;
        UserManager _UserManager = UserManager.GetInstance();
        PostManager _PostManager = PostManager.GetInstance();
        bool _Clicked = false;
        ObservableCollection<Profile> Profiles;
        private ObservableCollection<Post> _MyPost;
        public ObservableCollection<Post> MyPost
        {
            get { return this._MyPost; }
        }
        public AccountPage()
        {
            this.InitializeComponent();
          

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _CurrentUser = (User)e.Parameter;
            UserNameBlock.Text = _CurrentUser.UserName;
            LastNameBlock.Text = _CurrentUser.LastName;
            UserIDBlock.Text = _CurrentUser.UserId.ToString();
            EmailBlock.Text = _CurrentUser.Email;
            BirthdayBlock.Text = _CurrentUser.BirthDay.ToString();
            GenderBlock.Text = _CurrentUser.Gender;
            _MyPost = new ObservableCollection<Post>(_PostManager.ViewMyPost(_CurrentUser.UserId));
            _MyPost = _PostManager.DateChange(_MyPost);
            if (_MyPost.Count == 0)
                NoPost.Visibility = Visibility.Visible;
            else
                NoPost.Visibility = Visibility.Collapsed;
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Avatar");
            IReadOnlyList<StorageFile> sortedItems = await assets.GetFilesAsync();
            Profiles = new ObservableCollection<Profile>();
            foreach (var file in sortedItems)
            {
                Profiles.Add(new Profile(file.Name, file.Path));
            }
            MyAssets.ItemsSource = Profiles;
            Image img = new Image();
            if (_MyPost.Count != 0)
            {
                img.Source = new BitmapImage(new Uri(_MyPost[0].ProfilePic));
                br.ImageSource = img.Source;
            }
            else
            {
                img.Source = new BitmapImage(new Uri(_UserManager.ProfilePic(_CurrentUser)));
                br.ImageSource = img.Source;
            }
                    
             
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
                Post SelectedPost = _Post;
                Frame.Navigate(typeof(EditPostPage), SelectedPost);
            }
        }
        private void MyPost_ItemClick(object sender, ItemClickEventArgs e)
        {
            _Post = (Post)e.ClickedItem;
            _Clicked = true;
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;
            if(width<800)
                 Frame.Navigate(typeof(ViewPost), _Post);
            SampleText.Visibility = Visibility.Collapsed;
            ViewPost.Visibility = Visibility.Visible;
            PostContent.Text = _Post.PostContent;    
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                 MessageDialog showDialog = new MessageDialog("Do you want delete the post");
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
                   _PostManager.DeletePost(_Post);
                   _MyPost.Remove(_Post);
                 }
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PostPage), _CurrentUser);
        }
        private void View_Click(object sender, RoutedEventArgs e)
        {
            
                Editbt.Visibility = Visibility.Visible;
                Deletebt.Visibility = Visibility.Visible;
                ButtonStack.Visibility = Visibility.Visible;
                Intro.Visibility = Visibility.Collapsed;
                InsideFrame.Visibility = Visibility.Visible;
            
        }
        private void Editbt_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
                Post selectedPost = _Post;
                Frame.Navigate(typeof(EditPostPage), selectedPost);
            }
        }
        private void Deletebt_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
            }
        }
        private void GoAccount_Click(object sender, RoutedEventArgs e)
        {

            Intro.Visibility = Visibility.Visible;
            InsideFrame.Visibility = Visibility.Collapsed;
            GoAccount.Visibility = Visibility.Collapsed;
            GoBack.Visibility = Visibility.Visible;
        }
        private void ViewPost_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                Frame.Navigate(typeof(ViewPost), _Post);
            }
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            _UserManager.SignOut();
            Frame.Navigate(typeof(MainPage));
        }
        private void MyAssets_ItemClick(object sender, ItemClickEventArgs e)
        {
            Profile profile;
            profile = (Profile)e.ClickedItem;
            _UserManager.UpateProfile(_CurrentUser, profile);
            _PostManager.UpdateProfile(_CurrentUser, profile);
            br.ImageSource = _UserManager.UpateProfile(_CurrentUser, profile).Source;
            Frame.Navigate(typeof(AccountPage), _CurrentUser);


        }
    }

}
