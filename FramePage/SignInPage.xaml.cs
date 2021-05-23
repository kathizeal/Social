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
        public UserManager _UserManager = UserManager.GetInstance();
        User _User;
        public SignInPage()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            List<User> users = _UserManager.UsersLists();
            foreach (var i in users.ToList())
            {
                if (UserIdBlock.Text == i.UserName)
                {
                    _User = i;
                    if (PasswordBlock.Password == i.Password)
                    {
                        
                        _UserManager.SignedUser(i);
                        MainPage mainPage = new MainPage();
                        found = true;
                        this.Frame.Navigate(typeof(PostPage));
                    }
                    else
                    {
                        Warning.Text = "Password not matched.";
                        Warning.Visibility = Visibility.Visible;
                        ForgotButton.Visibility = Visibility.Visible;
                    }
                }
            }
            if (found == false)
            {
                Warning.Text = "No account found";
                Warning.Visibility = Visibility.Visible;
            }
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
        }
        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            _UserManager.DeleteUserRecord();
        }
        private void PasswordBlock_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            Warning.Visibility = Visibility.Collapsed;
        }
        private void UserIdBlock_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Warning.Visibility = Visibility.Collapsed;
        }
        private void ForgotButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PasswordChangePage), _User);
        }
    }
}

