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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page

    {
        string gender;
        DateTime Birthday;
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MainFramePage.Navigate(typeof(MainPage));
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            
            string DOB = Birthday.ToShortDateString();
            MainPage.userManager.AddUser(UserNameBox.Text, LastNameBox.Text, EmailBox.Text, PasswordBox.Password,DOB,gender);
            MainPage.MainFramePage.Navigate(typeof(MainPage));
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
        }
    }
}
