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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// 
    /// </summary>
   
   public sealed partial class PostPage : Page
    {
       
        User CurrentUser;
        public NavigationView navigationView;
        

        private  ObservableCollection<Post> _PostList;  
        public  ObservableCollection<Post> PostList
        {
            get { return _PostList; }
            
          
        }
        public PostPage()
        {
            this.InitializeComponent();
            navigationView = NavViewPostPage;


            
            NavViewPostPage.IsBackButtonVisible = (NavigationViewBackButtonVisible)Visibility.Visible;
            

        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Instead of hard coded items, the data could be pulled 
            // asynchronously from a database or the internet.
            /* PostList.Add(new Post("User1 Post", "This is my First Post", "user1", 12345689, "/Assets/male-01.png"));
             PostList.Add(new Post("User2 Post", "This is my Second Post", "user2", 12345689, "/Assets/male-02.png"));
             PostList.Add(new Post("User3 Post", "This is my third Post", "user3", 12345689, "/Assets/male-03.png"));*/

            PostManager postManager = PostManager.GetInstance();
            _PostList = new ObservableCollection<Post>(postManager.ViewAllPost());

            /*object value = ApplicationData.Current.LocalSettings.Values["UserClass"];
            var user = JsonConvert.DeserializeObject<User>(value.ToString());*/
            UserManager userManager = UserManager.GetInstance();
            CurrentUser = userManager.Current();



        }       

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPost = (Post)e.ClickedItem;
            SecondFrame.Navigate(typeof(SecondaryPage),selectedPost);

        }
        

        private void NavViewPostPage_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            switch(item.Tag.ToString())
            {
                case "Account":                   

                    MySpLitView.IsPaneOpen = false;                    
                    this.Frame.Navigate(typeof(AccountPage),CurrentUser);
                    break;
               /* case "List":

                    if (MySpLitView.IsPaneOpen == false)
                        MySpLitView.IsPaneOpen = true;
                    else
                        MySpLitView.IsPaneOpen = false;

                    break;*/
                case "NewPost":
                    MySpLitView.IsPaneOpen = false;
                    this.Frame.Navigate(typeof(CreatePostPage), CurrentUser);
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
