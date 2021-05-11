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
  
    public sealed partial class MainPage : Page
    {
        UserManager userManager = UserManager.GetInstance();
        PostManager postManager = PostManager.GetInstance();
       
       
        public Frame mainFrame()
        {
            return MainFrame;
        }

        public MainPage()
        {
            this.InitializeComponent();

           /* userManager.AddUser("Kathiravan", "Kannan", "kathizeal@gmail.com", "1234", "20/05/1999", "Male");
            userManager.AddUser("Robin", "jesba", "RobiJesba@gmail.com", "2345", "22/02/1999", "Male");
            //_PostList = new ObservableCollection<Post>(postManager.ViewAllPost());
            /*  postManager.AddPost(new Post("User1 Post", "This is my  First Post", "user1", 12345689));
             postManager.AddPost(new Post("User2 Post", "This is my second Post", "user2", 12345689));
             postManager.AddPost(new Post("User3 Post", "This is my third Post", "user2", 12345689));*/
            if (userManager.State() == false)
            {
                userManager.CreateTable();
                MainFrame.Navigate(typeof(SignInPage));
            }
            else
            {
                postManager.CreateTable();
                MainFrame.Navigate(typeof(PostPage));
            }


        }
      
    }
}
