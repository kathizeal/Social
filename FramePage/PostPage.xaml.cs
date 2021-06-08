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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   
   public sealed partial class PostPage : Page 
   {
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        User _CurrentUser;
        SplitView _SplitView;
        NavigationView _NavigationView;
        private ObservableCollection<Post> _PostList;
        public  ObservableCollection<Post> PostList
        {
            get { return _PostList; }
        }
        void AddingPost(Post post)
        {
            _PostList.Add(post);
            _PostList = _PostManager.DateChange(_PostList);
        }
        private void UpdateStatus(object sender, CustomEvent e)
        {
            AddingPost(e.Post);
        }
        public PostPage()
        {
            this.InitializeComponent();
            CreateHandler createHandler = CreateHandler.GetInstance();
            createHandler.OnUpdateStatus += new CreateHandler.StatusUpdateHandler(UpdateStatus);
            _NavigationView = NavViewPostPage;
            _SplitView = MySpLitView;
            _PostList = new ObservableCollection<Post>(_PostManager.ViewAllPost());
            _PostList = _PostManager.DateChange(_PostList);
            NavViewPostPage.IsBackButtonVisible = (NavigationViewBackButtonVisible)Visibility.Visible;
            NavViewPostPage.IsSettingsVisible = false;
          

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Home.Visibility = Visibility.Collapsed;
            UserManager userManager = UserManager.GetInstance();
            _CurrentUser= userManager.Current();
            _CurrentUser = userManager.Find(_CurrentUser.UserId);
            foreach (var user in _UserList)
            {
                user.ProfilePic = _UserManager.ProfilePic(user);
            }
            MySpLitView.IsPaneOpen = true;
          
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
                    this.Frame.Navigate(typeof(AccountPage),_CurrentUser);
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
        private void AddButton(object sender, TappedRoutedEventArgs e)
        {
            ClickList.SelectedItem = null;
            SecondFrame.Visibility = Visibility.Visible;
            SecondFrame.Navigate(typeof(CreatePostPage), _CurrentUser);
            Dummy.IsSelected = true;
            Add.IsSelected = false;
        }
        private void DeleteRecord(object sender, TappedRoutedEventArgs e)
        {
            PostManager postManager = PostManager.GetInstance();
            postManager.DeleteRecord();
            _PostList.Clear();
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

    }

}   
