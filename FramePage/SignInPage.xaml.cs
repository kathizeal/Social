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
using static Social.MainPage;
using Social.Model;
using Social.Data;
using Windows.Storage;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    /* public sealed partial class SignInPage : Page
     {

         public UserManager userManager = UserManager.GetInstance(); 
         public SignInPage()
         {
             this.InitializeComponent();



         }

         private void Button_Click(object sender, RoutedEventArgs e)
         {
             List<User> users =userManager.UsersLists();
            foreach(var i in users)
             {
                 if (UserIdBlock.Text == i.UserName && PasswordBlock.Password == i.Password)
                 {

                     userManager.SignedUser(i);

                     Frame.Navigate(typeof(PostPage), i);
                     //Frame.Navigate(typeof(PostPage), i);
                 }

             }      

         }

         private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
         {
             Frame.Navigate(typeof(SignUpPage));
             //Frame.Navigate(typeof(SignUpPage));
         }
     }*/
    public sealed partial class SignInPage : Page
    {

        public UserManager userManager = UserManager.GetInstance();
        public SignInPage()
        {
            this.InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = userManager.UsersLists();
            foreach (var i in users.ToList())
            {
                if (UserIdBlock.Text == i.UserName && PasswordBlock.Password == i.Password)
                {

                    userManager.SignedUser(i);
                    MainPage mainPage = new MainPage();
                    this.Frame.Navigate(typeof(PostPage));
                    //Frame.GoBack();
                    //Frame.Navigate(typeof(MainPage), i);
                    //Frame.Navigate(typeof(PostPage), i);
                }

            }

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
            //Frame.Navigate(typeof(SignUpPage));
        }
    }
}

