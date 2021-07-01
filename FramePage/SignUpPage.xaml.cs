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
using Social.Domain;
using Social.Util;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        //UserManager _UserManager = UserManager.GetInstance();
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
                //_UserManager.AddUser(UserNameBox.Text, LastNameBox.Text, EmailBox.Text, PasswordBox.Password, DOB, _Gender);
                var addUserRequest = new AddUserRequest(UserNameBox.Text, LastNameBox.Text, EmailBox.Text, PasswordBox.Password, DOB, _Gender);
                AddUser addUser = new AddUser(addUserRequest, new AddUserPresenterCallBack(this));
                Frame.Navigate(typeof(MainPage));
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
        }
        public class AddUserPresenterCallBack : IAddUserPresenterCallback
        {
            SignUpPage presenter;
            public AddUserPresenterCallBack(SignUpPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<AddUserResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {



                }
                );
            }


        }
    }
}
