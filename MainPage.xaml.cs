using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Social.FramePage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Social.Model;
using Social.Data;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Social
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   /* public sealed partial class MainPage : Page
    {
      public  UserManager userManager = UserManager.GetInstance();
        public  PostManager postManager = PostManager.GetInstance();
        
        
        public static Frame MainFramePage;

        public MainPage()
        {
            this.InitializeComponent();
            MainFramePage = MyFrame;
            userManager.AddUser("Kathiravan","Kannan","kathizeal@gmail.com","1234","20/05/1999","Male");
            userManager.AddUser("Robin","jesba","RobiJesba@gmail.com", "2345","22/02/1999","Male");
            /*  postManager.AddPost(new Post("User1 Post", "This is my  First Post", "user1", 12345689));
             postManager.AddPost(new Post("User2 Post", "This is my second Post", "user2", 12345689));
             postManager.AddPost(new Post("User3 Post", "This is my third Post", "user2", 12345689));
            if (userManager.State() == false)
                MyFrame.Navigate(typeof(SignInPage));
            else
            {
                MyFrame.Navigate(typeof(PostPage));
            }
            
              
        }
       
    }*/
    public sealed partial class MainPage : Page
    {
        UserManager userManager = UserManager.GetInstance();
        PostManager postManager = PostManager.GetInstance();
        User CurrentUser;
       
        public Frame mainFrame()
        {
            return MainFrame;
        }




        public MainPage()
        {
            this.InitializeComponent();

            userManager.AddUser("Kathiravan", "Kannan", "kathizeal@gmail.com", "1234", "20/05/1999", "Male");
            userManager.AddUser("Robin", "jesba", "RobiJesba@gmail.com", "2345", "22/02/1999", "Male");
            //_PostList = new ObservableCollection<Post>(postManager.ViewAllPost());
            /*  postManager.AddPost(new Post("User1 Post", "This is my  First Post", "user1", 12345689));
             postManager.AddPost(new Post("User2 Post", "This is my second Post", "user2", 12345689));
             postManager.AddPost(new Post("User3 Post", "This is my third Post", "user2", 12345689));*/
            if (userManager.State() == false)
                MainFrame.Navigate(typeof(SignInPage));
            else
                MainFrame.Navigate(typeof(PostPage));


        }
      /*  protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            NavViewPostPage.IsPaneOpen = true;

            CurrentUser = userManager.Current();
        }
        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //_PostList = new ObservableCollection<Post>(postManager.ViewAllPost());
            MySpLitView.IsPaneOpen = !MySpLitView.IsPaneOpen;
        }
        private void NavViewPostPage_Loaded(object sender, RoutedEventArgs e)
        {

            NavViewPostPage.IsPaneOpen = false;

        }
        private void NavViewPostPage_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            switch (item.Tag.ToString())
            {
                case "Account":

                    MySpLitView.IsPaneOpen = false;
                    CurrentUser = userManager.Current();
                    SecondFrame.Navigate(typeof(AccountPage), CurrentUser);
                    break;
                /* case "List":

                     if (MySpLitView.IsPaneOpen == false)
                         MySpLitView.IsPaneOpen = true;
                     else
                         MySpLitView.IsPaneOpen = false;

                     break;
                case "NewPost":
                    MySpLitView.IsPaneOpen = false;
                    CurrentUser = userManager.Current();
                    SecondFrame.Navigate(typeof(CreatePostPage), CurrentUser);
                    break;


            }
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPost = (Post)e.ClickedItem;
            SecondFrame.Navigate(typeof(SecondaryPage), selectedPost);

        }*/
    }
}
