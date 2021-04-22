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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        User CurrentUser;
        public AccountPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CurrentUser = (User)e.Parameter;
            UserName.Text ="User name : "+ CurrentUser.UserName;
            LastName.Text = "Last name : " + CurrentUser.LastName;
            UserID.Text = "User Id : " + CurrentUser.UserId.ToString();
            Email.Text = "Email address : " + CurrentUser.Email;
            Birthday.Text = "DOB : " + CurrentUser.BirthDay;
            Gender.Text = "Gender : " + CurrentUser.Gender;

        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            ApplicationData.Current.LocalSettings.Values["UserClass"] = null;


            MainPage.MainFramePage.Navigate(typeof(MainPage));
        }
    }
}
