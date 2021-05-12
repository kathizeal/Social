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
        
        private ObservableCollection<Post> _MyPost;
        public ObservableCollection<Post> MyPost
        {
            get { return this._MyPost; }
        }
        public AccountPage()
        {
            this.InitializeComponent();
            
           
           // Date.Text = DateTime.Today.ToShortDateString();
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
                if (_MyPost.Count == 0)
                    NoPost.Visibility = Visibility.Visible;
                else
                    NoPost.Visibility = Visibility.Collapsed;
                //string pic = "ms-appx:///Assets/"+ _CurrentUser.UserName + ".jpg";
            
                
           
                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
                IReadOnlyList<StorageFile> sortedItems = await assets.GetFilesAsync();
                
                
                foreach (var file in sortedItems)
                {
                  string filename= _CurrentUser.UserName + ".jpg";
                  if (file.Name==filename)
                  {
                    string pic = "ms-appx:///Assets/" + _CurrentUser.UserName + ".jpg";
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(pic));
                    ProfilePic.ProfilePicture = img.Source;
                  }
                }
            
         }
       

        /* private void SignOutButton_Click(object sender, RoutedEventArgs e)
         {

             //ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
             userManager.SignOut();
             MainPage mainPage = new MainPage();

            // mainPage.mainFrame().Navigate(typeof(SignInPage));

             // mainPage.mainFramePage().Navigate(typeof(MainPage));
             //.Navigate(typeof(SignInPage));
             Frame.Navigate(typeof(MainPage));
         }*/
       
      

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
                Post SelectedPost = _Post;
                Frame.Navigate(typeof(EditPostPage), SelectedPost);
                //Frame.Navigate(typeof(EditPostPage), SelectedPost);
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
                Frame.Navigate(typeof(AccountPage), _CurrentUser);
            }
            // Frame.Navigate(typeof(PostPage), _CurrentUser);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            //Frame.GoBack();
            Frame.Navigate(typeof(PostPage), _CurrentUser);
        }
        private void View_Click(object sender, RoutedEventArgs e)
        {
            
                Editbt.Visibility = Visibility.Visible;
                Deletebt.Visibility = Visibility.Visible;
                ButtonStack.Visibility = Visibility.Visible;
                Intro.Visibility = Visibility.Collapsed;
                InsideFrame.Visibility = Visibility.Visible;
            
            //this.Frame.Navigate(typeof(MyPostPage),_CurrentUser);
        }

        private void Editbt_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
                Post SelectedPost = _Post;
                Frame.Navigate(typeof(EditPostPage), SelectedPost);
            }
        }
        private void Deletebt_Click(object sender, RoutedEventArgs e)
        {
            if (_Clicked == true)
            {
                _PostManager.DeletePost(_Post);
                Frame.Navigate(typeof(AccountPage), _CurrentUser);
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
            // mainPage.mainFrame().Navigate(typeof(SignInPage));
            // mainPage.mainFramePage().Navigate(typeof(MainPage));
            //.Navigate(typeof(SignInPage));
            Frame.Navigate(typeof(MainPage));
        }
    }

}
