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
      public static UserManager userManager = UserManager.GetInstance();
        public static Frame MainFramePage;
        public MainPage()
        {

            this.InitializeComponent();
            MainFramePage = MyFrame;
            userManager.AddUser("Kathiravan","Kannan","kathizeal@gmail.com","1234","20/05/1999","Male");
            userManager.AddUser("Robin","jesba","RobiJesba@gmail.com", "2345","22/02/1999","Male");
            if(ApplicationData.Current.LocalSettings.Values["UserClass"] == null)
                MainFramePage.Navigate(typeof(SignInPage));
            else 
            {

                MainFramePage.Navigate(typeof(PostPage));

            }         

            PostManager postManager = PostManager.GetInstance();

           
            
        }
        







        

    }
}
