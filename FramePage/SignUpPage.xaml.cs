 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Social.Model;
using Social.Data;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /*public sealed partial class SignUpPage : Page

    {
        UserManager userManager = UserManager.GetInstance();
        
        string gender;
        DateTime Birthday;
        public SignUpPage()
        {
            this.InitializeComponent();
            PostPage postPage = new PostPage();
            postPage.navigationView.Visibility = Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
           MainPage.MainFramePage.Navigate(typeof(MainPage));
           //Frame.Navigate(typeof(MainPage));
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            
            string DOB = Birthday.ToShortDateString();
            if (string.IsNullOrWhiteSpace(UserNameBox.Text) || string.IsNullOrWhiteSpace(LastNameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password) || string.IsNullOrWhiteSpace(RePasswordBox.Password) || string.IsNullOrWhiteSpace(DOB))
                Warning.Visibility = Visibility.Visible;
            else
            {
                userManager.AddUser(UserNameBox.Text, LastNameBox.Text, EmailBox.Text, PasswordBox.Password, DOB, gender);
                MainPage.MainFramePage.Navigate(typeof(MainPage));
                // MainPage.MyFrame.Navigate(typeof(MainPage));
            }
        }

       private void Dob_DateChanged(object sender, DatePickerValueChangedEventArgs args)   
        {
           Birthday = new DateTime(args.NewDate.Year, args.NewDate.Month, args.NewDate.Day);
            
        }

        private void GenderBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboxBoxItem = e.AddedItems[0] as ComboBoxItem;
            gender = comboxBoxItem.Content as string;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MainFramePage.Navigate(typeof(MainPage));
            //Frame.Navigate(typeof(MainPage));
        }
    }*/
    public sealed partial class SignUpPage : Page
    {
        UserManager _userManager = UserManager.GetInstance();
        string _Gender;
        DateTime _Birthday;
        public SignUpPage()
        {
            this.InitializeComponent();
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

            string DOB = _Birthday.ToString("dd/MM/yyyy");
            if (string.IsNullOrWhiteSpace(UserNameBox.Text) || string.IsNullOrWhiteSpace(LastNameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password) || string.IsNullOrWhiteSpace(RePasswordBox.Password) || string.IsNullOrWhiteSpace(DOB))
                Warning.Visibility = Visibility.Visible;
            else
            {
                _userManager.AddUser(UserNameBox.Text, LastNameBox.Text, EmailBox.Text, PasswordBox.Password, DOB, _Gender);
                Frame.Navigate(typeof(MainPage));
                // MainPage.MyFrame.Navigate(typeof(MainPage));
            }
        }
        private void Dob_DateChanged(object sender, DatePickerValueChangedEventArgs args)
        {
            _Birthday = new DateTime(args.NewDate.Year, args.NewDate.Month, args.NewDate.Day);

        }
        private void GenderBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboxBoxItem = e.AddedItems[0] as ComboBoxItem;
            _Gender = comboxBoxItem.Content as string;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
            //Frame.Navigate(typeof(MainPage));
        }
    }
}
