using Social.Data;
using Social.Model;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PasswordChangePage : Page
    {
        User _User;
        public PasswordChangePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _User = (User)e.Parameter;
            UserNameBox.Text = _User.UserName;

        }
        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LastNameBox.Text != _User.LastName)
                Warning.Visibility = Visibility.Visible;
            else
                Warning.Visibility = Visibility.Collapsed;
        }
        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(EmailBox.Text!=_User.Email)
                Warning.Visibility = Visibility.Visible;
            else
                Warning.Visibility = Visibility.Collapsed;
        }
        private void Dob_DateChanged(object sender, DatePickerValueChangedEventArgs args)
        {
            DateTime birthDay;
            birthDay = new DateTime(args.NewDate.Year, args.NewDate.Month, args.NewDate.Day);
            string DOB = birthDay.ToShortDateString();
            if (DOB!=_User.BirthDay)
                Warning.Visibility = Visibility.Visible;
            else
                Warning.Visibility = Visibility.Collapsed;
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            User newUser =_User;
            newUser.UserId = _User.UserId;
            newUser.Password = PasswordBox.Password;
            UserManager userManager = UserManager.GetInstance();
            userManager.Update(newUser);
            Frame.Navigate(typeof(MainPage));
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}

