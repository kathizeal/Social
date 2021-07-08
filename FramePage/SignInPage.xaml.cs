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
using Social.Domain;
using Social.Util;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        public UserManager UserManager = UserManager.GetInstance();
        private User _user;
        private List<User> _usersList;
        public SignInPage()
        {
            this.InitializeComponent();
            var getAllUsersListRequest = new GetAllUsersListRequest();
            GetAllUsersList getAllUsersList = new GetAllUsersList(getAllUsersListRequest, new GetAllUsersListPresenterCallBack(this));
            getAllUsersList.Execute();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            foreach (var i in _usersList.ToList())
            {
                if (UserIdBlock.Text == i.UserName)
                {
                    _user = i;
                    if (PasswordBlock.Password == i.Password)
                    {
                        
                        //_UserManager.SignedUser(i);
                        var signedUserRequest = new SignedUserRequest(i);
                        SignedUser signedUser = new SignedUser(signedUserRequest, new SignedUserPresenterCallBack(this));
                        signedUser.Execute();
                        found = true;
                        //this.Frame.Navigate(typeof(PostPage));
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
            UserManager.DeleteUserRecord();
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
            this.Frame.Navigate(typeof(PasswordChangePage), _user);
        }
        public class GetAllUsersListPresenterCallBack : IGetAllUsersListPresenterCallback
        {
            SignInPage presenter;
            public GetAllUsersListPresenterCallBack(SignInPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetAllUsersListResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    presenter._usersList = response.Obj.Users;
                    
                }
                );
            }


        }
        public class SignedUserPresenterCallBack : ISignedUserPresenterCallback
        {
            SignInPage presenter;
            public SignedUserPresenterCallBack(SignInPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<SignedUserResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    presenter.Frame.Navigate(typeof(PostPage));

                }
                );
            }


        }
    }
}

