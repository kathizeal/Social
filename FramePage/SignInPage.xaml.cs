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
    
    public sealed partial class SignInPage : Page
    {
       /* UserManager userManager = UserManager.GetInstance();*/
        
         
        public SignInPage()
        {
            this.InitializeComponent();
          /*  userManager.AddUser("user1", "1234");
            userManager.AddUser("user2", "2345");*/
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users =MainPage.userManager.UsersLists();
           foreach(var i in users)
            {
                if (UserIdBlock.Text == i.UserName && PasswordBlock.Password == i.Password)
                {
                    
                    string json = JsonConvert.SerializeObject(i);
                    ApplicationData.Current.LocalSettings.Values["UserClass"] = json;



                    MainFramePage.Navigate(typeof(PostPage), i);
                }

            }
           


            
            
            
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            MainFramePage.Navigate(typeof(SignUpPage));
        }
    }
}
