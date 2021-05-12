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
    /// 
    /// </summary>
   
   public sealed partial class PostPage : Page 
   {
       
        User _CurrentUser;
        public NavigationView navigationView;
        private  ObservableCollection<Post> _PostList=new ObservableCollection<Post>();  
        public  ObservableCollection<Post> PostList
        {
            get { return _PostList; }
       
        }
        public PostPage()
        {
            this.InitializeComponent();
            navigationView = NavViewPostPage;
            PostManager postManager = PostManager.GetInstance();
            _PostList = new ObservableCollection<Post>(postManager.ViewAllPost());
            NavViewPostPage.IsBackButtonVisible = (NavigationViewBackButtonVisible)Visibility.Visible;
            NavViewPostPage.IsSettingsVisible = false;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Home.Visibility = Visibility.Collapsed;
            // Instead of hard coded items, the data could be pulled 
            // asynchronously from a database or the internet.
            UserManager userManager = UserManager.GetInstance();
            _CurrentUser = userManager.Current();
        }       
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPost = (Post)e.ClickedItem;
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;
            if (width > 800)
            {

                Home.Visibility = Visibility.Visible;
                SecondFrame.Navigate(typeof(SecondaryPage), selectedPost);
            }
            else
            {
                   
                Home.Visibility = Visibility.Visible;
                SecondFrame.Navigate(typeof(SecondaryPage), selectedPost);
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
            MySpLitView.IsPaneOpen = !MySpLitView.IsPaneOpen;
        }
        private void AddButton(object sender, TappedRoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(CreatePostPage), _CurrentUser);
              
        }
        private void DeleteRecord(object sender, TappedRoutedEventArgs e)
        {
            PostManager postManager = PostManager.GetInstance();
            postManager.DeleteRecord();
            this.Frame.Navigate(typeof(PostPage));
        }
        private void HomeButton(object sender, TappedRoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(PostPage), _CurrentUser);
        }
       /* private void MaxiButton_Click(object sender, RoutedEventArgs e)
         {
             Max = true;
             Home.Visibility = Visibility.Visible;
             SecondFrame.Navigate(typeof(SecondaryPage), CurrentPost);
         }*/
        /*  private void CloseButton_Click(object sender, RoutedEventArgs e)
          {
              FloatWindow.Height = 300;
              FloatWindow.Width = 400;
              IntroStack.Visibility = Visibility.Visible;
              floatPost.Visibility = Visibility.Collapsed;

          }*/
        /*  private void MaxiButtonCreate_Click(object sender, RoutedEventArgs e)
          {
              Max = true;
              MySpLitView.IsPaneOpen = false;
              this.Frame.Navigate(typeof(CreatePostPage), CurrentUser);
          }*/
        /* private void CloseButtonCreate_Click(object sender, RoutedEventArgs e)
         {

             CreateFloat.Visibility = Visibility.Collapsed;
             FloatWindow.Visibility = Visibility.Visible;
         }*/
        /*private void CreateButton_Click(object sender, RoutedEventArgs e)
         {
             PostManager postManager = PostManager.GetInstance();
             Post newPost = new Post();
             newPost.PostTitle = TitleBox.Text;
             newPost.PostContent = ContenBox.Text;
             newPost.PostCreatedByUserName = CurrentUser.UserName;
             newPost.PostCreatedByUserId = CurrentUser.UserId;
             // postManager.AddPost(new Post(TitleBox.Text, ContenBox.Text, CurrentUser.UserName, CurrentUser.UserId));
             postManager.AddPost(newPost);
             // MainPage.MainFramePage.Navigate(typeof(PostPage));
             this.Frame.Navigate(typeof(PostPage));
             //Frame.Navigate(typeof(MainPage));

         }*/
        /*   private void CancelButton_Click(object sender, RoutedEventArgs e)
           {
               // MainPage.MainFramePage.Navigate(typeof(PostPage));
               //this.Frame.GoBack();

               CreateFloat.Visibility = Visibility.Collapsed;
               FloatWindow.Visibility = Visibility.Visible;


           }*/
        /*private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (ClickList.IsItemClickEnabled)
            {
                string state = NarrowStateOnlyFrame.ToString();
                VisualStateManager.GoToState(this, state, true);
            }
            else
            {
                string state = NarrowStateOnlyList.ToString();
                VisualStateManager.GoToState(this, state, true);
            }
        }*/
   }

}   
