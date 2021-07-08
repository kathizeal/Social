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
using Social.Data;
using Social.Model;
using Social.FramePage;
using Windows.Storage;
using Newtonsoft.Json;
using Windows.ApplicationModel.Core;
using Social.Domain;
using Social.Util;
using Windows.UI.Core;
using Windows.UI.WindowManagement;
using System.Threading.Tasks;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   
   public sealed partial class PostPage : Page 
   {
       // PostManager _PostManager = PostManager.GetInstance();
        //UserManager _UserManager = UserManager.GetInstance();
        private User _currentUser;
        SplitView _splitView;
        private NavigationView _navigationView;
        private ObservableCollection<Post> _postList;
        public  ObservableCollection<Post> PostList
        {
            get { return _postList; }
        }
        private ObservableCollection<User> _UserList;
        public ObservableCollection<User> UserList
        {
            get { return _UserList; }
        }
        public PostPage()
        {
            this.InitializeComponent();
            _navigationView = NavViewPostPage;
            _splitView = MySpLitView;
            NavViewPostPage.IsBackButtonVisible = (NavigationViewBackButtonVisible)Visibility.Visible;
            NavViewPostPage.IsSettingsVisible = false;
           // _UserList = new ObservableCollection<User>(_UserManager.ALLUsersLists());
           // _UserList = _UserManager.DateChange(_UserList);
           
            ListV.Visibility = Visibility.Collapsed;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ListV.Visibility = Visibility.Collapsed;
            Home.Visibility = Visibility.Collapsed;
            GetCurrentUserRequest getCurrentUserRequest = new GetCurrentUserRequest();
            GetCurrentUser getCurrentUser = new GetCurrentUser(getCurrentUserRequest, new GetCurrentUserPresenterCallback(this));
            getCurrentUser.Execute();
            //UserManager userManager = UserManager.GetInstance();
            //_CurrentUser= userManager.Current();
            //_CurrentUser = userManager.Find(_CurrentUser.UserId);
            /*foreach (var user in _UserList)
            {
                user.ProfilePic = _UserManager.ProfilePic(user);
            }*/
            MySpLitView.IsPaneOpen = true;
            ClickList.Visibility = Visibility.Visible;

        }       
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SecondFrame.Visibility = Visibility.Visible;
            var selectedPost = (Post)e.ClickedItem;
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;
            Home.Visibility = Visibility.Visible;
            if (width > 800)
            {
                SecondFrame.Navigate(typeof(SecondaryPage), selectedPost);
            }
            else
            {
                SecondFrame.Navigate(typeof(SecondaryPage), selectedPost);
                ListV.Visibility = Visibility.Visible;
                MySpLitView.IsPaneOpen = false;
            }
        }
        private void NavViewPostPage_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            switch(item.Tag.ToString())
            {
                case "Account":                   
                    MySpLitView.IsPaneOpen = false;                    
                    this.Frame.Navigate(typeof(AccountPage),_currentUser);
                    break;
            }
          }
        private void NavViewPostPage_Loaded(object sender, RoutedEventArgs e)
        {
           NavViewPostPage.IsPaneOpen = false;
        }
        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserListView.Visibility = Visibility.Visible;
            MySpLitView.IsPaneOpen = true;
            UserListView.Visibility = Visibility.Collapsed;
            ClickList.Visibility = Visibility.Visible;
            ListV.Visibility = Visibility.Collapsed;
            Dummy.IsSelected = true;
            StackHeading.Text = "Feeds";

        }
        private async void AddButton(object sender, TappedRoutedEventArgs e)
        {
            /*ClickList.SelectedItem = null;
            SecondFrame.Visibility = Visibility.Visible;
            SecondFrame.Navigate(typeof(CreatePostPage), _currentUser);
            Dummy.IsSelected = true;
            Add.IsSelected = false;
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;
            if (width > 800)
            {
                SecondFrame.Navigate(typeof(CreatePostPage), _currentUser);
            }
            else
            {
                SecondFrame.Navigate(typeof(CreatePostPage), _currentUser);
                ListV.Visibility = Visibility.Visible;
                MySpLitView.IsPaneOpen = false;
            }*/
            Dummy.IsSelected = true;
            Add.IsSelected = false;
            AppWindow appWindow = await AppWindow.TryCreateAsync();
            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(CreatePostPage),_currentUser);
            CreatePostPage page = (CreatePostPage)appWindowContentFrame.Content;
            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
            page.MyAppWindow = appWindow;
            await appWindow.TryShowAsync();
        }
        private async void DeleteRecord(object sender, TappedRoutedEventArgs e)
        {
            // PostManager postManager = PostManager.GetInstance();
            // postManager.DeleteRecord();
            // _PostList.Clear();
            AppWindow appWindow = await AppWindow.TryCreateAsync();
            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(NewView));
            NewView page = (NewView)appWindowContentFrame.Content;
            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
            page.MyAppWindow= appWindow;
            await appWindow.TryShowAsync();

        }
        private void HomeButton(object sender, TappedRoutedEventArgs e)
        {
            SecondFrame.Visibility = Visibility.Collapsed;
            Dummy.IsSelected = true;
            Home.Visibility = Visibility.Collapsed;
            MySpLitView.IsPaneOpen = true;
        }

        private void ChatButton(object sender, TappedRoutedEventArgs e)
        {
            UserListView.Visibility = Visibility.Visible;
            ClickList.Visibility = Visibility.Collapsed;
            ListV.Visibility = Visibility.Visible;
            MySpLitView.IsPaneOpen = true;
            StackHeading.Text = "People";
        }

        private void UserListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedUser = (User)e.ClickedItem;
            ListV.Visibility = Visibility.Visible;
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;
            Home.Visibility = Visibility.Visible;
            if (width > 800)
            {
                SecondFrame.Navigate(typeof(ChatPage), selectedUser);
            }
            else
            {
                SecondFrame.Navigate(typeof(ChatPage), selectedUser);
                MySpLitView.IsPaneOpen = false;
            }

        }
        public class GetPostPresenterCallBack : IGetPostsPresenterCallback
        {
            PostPage presenter;
            public GetPostPresenterCallBack(PostPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetPostsResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    presenter._postList = new ObservableCollection<Post>(response.Obj.Posts);
                    presenter.ClickList.ItemsSource = presenter._postList;
                }
                );
            }


        }
        public class GetUsersListPresenterCallBack : IGetUsersListPresenterCallback
        {
            PostPage presenter;
            public GetUsersListPresenterCallBack(PostPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetUsersListResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    presenter._UserList = new ObservableCollection<User>(response.Obj.Users);
                    presenter.UserListView.ItemsSource = presenter._UserList;
                    
                }
                );
            }


        }
        public class GetCurrentUserPresenterCallback : IGetCurrentUserPresenterCallback
        {

            PostPage presenter;
            public GetCurrentUserPresenterCallback(PostPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetCurrentUserResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._currentUser = response.Obj.CurrentUser;
                    
                });
            }


        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            var getPostRequest = new GetPostRequest();
            GetPost getPost = new GetPost(getPostRequest, new GetPostPresenterCallBack(this));
            getPost.Execute();
            var getUsersListRequest = new GetUsersListRequest();
            GetUsersList getUsersList = new GetUsersList(getUsersListRequest, new GetUsersListPresenterCallBack(this));
            getUsersList.Execute();
            RegisterNotification();
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            UnRegisterNotification();
        }
        private async void HandlePostAdded(Post post)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _postList.Add(post);
            });
        }
        private void RegisterNotification()
        {
            UnRegisterNotification();
            SocialNotification.PostAdded += HandlePostAdded;

        }
        private void UnRegisterNotification()
        {
            SocialNotification.PostAdded -= HandlePostAdded;
        }

        private void Dummy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PostManager postManager = PostManager.GetInstance();
            postManager.DeleteRecord();
           _postList.Clear();
        }
        public class GetProfilePicPresenterCallback : IGetProfilePicPresenterCallback
        {
            PostPage presenter;

            public GetProfilePicPresenterCallback(PostPage view)
            {
                presenter = view;
            }
            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetProfilePicResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                });
            }
        }
    }

}   
