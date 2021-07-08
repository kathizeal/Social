using Social.Data;
using Social.Domain;
using Social.Model;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class ChatPage : Page
    {
        User _currentUser;
        User _anotherUser;
        bool _Exist;
       // UserManager _UserManager = UserManager.GetInstance();
       // PostManager _PostManager = PostManager.GetInstance();
        private ObservableCollection<Chat> _chatList= new ObservableCollection<Chat>();
        public ChatPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _anotherUser = (User)e.Parameter;
            // _CurrentUser = _UserManager.Current();
            // _CurrentUser = _UserManager.Find(_CurrentUser.UserId);
            GetCurrentUserRequest getCurrentUserRequest = new GetCurrentUserRequest();
            GetCurrentUser getCurrentUser = new GetCurrentUser(getCurrentUserRequest, new GetCurrentUserPresenterCallback(this));
            getCurrentUser.Execute();
          //  chatList = _UserManager.Message(_CurrentUser,_AnotherUser);
           /* _Exist=_UserManager.CheckExist(_CurrentUser, _AnotherUser);
            if(_Exist)
            {
                Starter.Visibility = Visibility.Collapsed;
                Chaat.Visibility = Visibility.Visible;
                Commands.Visibility = Visibility.Visible;

            }
            else
            {
                Starter.Visibility = Visibility.Visible;
                Chaat.Visibility = Visibility.Collapsed;
                Commands.Visibility = Visibility.Collapsed;
            }*/
        }
        private void EnableEvent(object sender, RoutedEventArgs e)
        {
           //_UserManager.CreateChat(_CurrentUser, _AnotherUser);
            var createChatRequest = new CreateChatRequest(_currentUser, _anotherUser);
            CreateChat createChat = new CreateChat(createChatRequest, new CreateChatPresenterCallback(this));
            createChat.Execute();
            //chatList = _UserManager.Message(_CurrentUser, _AnotherUser);
            ChatListView.ItemsSource = _chatList;
            Starter.Visibility = Visibility.Collapsed;
            Chaat.Visibility = Visibility.Visible;
            Commands.Visibility = Visibility.Visible;
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            chat.SenderId = _currentUser.UserId;
            chat.SenderName = _currentUser.UserName;
            chat.RecieverId = _anotherUser.UserId;
            chat.RecieverName = _anotherUser.UserName;
            chat.ProfilePic = _currentUser.ProfilePic;
            chat.Msg = CommentTextBox.Text;
            _chatList.Add(chat);
            //_UserManager.AddChat(chat);
            var addChatRequest = new AddChatRequest(chat);
            AddChat addChat = new AddChat(addChatRequest, null);
            addChat.Execute();
            CommentTextBox.Text = "";
        }
        public class GetCurrentUserPresenterCallback : IGetCurrentUserPresenterCallback
        {

            ChatPage presenter;
            public GetCurrentUserPresenterCallback(ChatPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetCurrentUserResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._currentUser = response.Obj.CurrentUser;

                    var CheckExistRequest = new CheckExistRequest(presenter._currentUser, presenter._anotherUser);
                    CheckExist checkExist = new CheckExist(CheckExistRequest, new CheckExistPresenterCallback(presenter));
                    checkExist.Execute();

                });
            }


        }
        public class CreateChatPresenterCallback : ICreateChatPresenterCallback
        {

            ChatPage presenter;
            public CreateChatPresenterCallback(ChatPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<CreateChatResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var getMessageRequest = new GetMessageRequest(presenter._currentUser, presenter._anotherUser);
                    GetMessage getMessage = new GetMessage(getMessageRequest, new GetMessagePresenterCallback(presenter));
                    getMessage.Execute();

                });
            }


        }
        public class AddChatPresenterCallback : IAddChatPresenterCallback
        {

            ChatPage presenter;
            public AddChatPresenterCallback(ChatPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<AddChatResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                   

                });
            }


        }
        public class GetMessagePresenterCallback : IGetMessagePresenterCallback
        {

            ChatPage presenter;
            public GetMessagePresenterCallback(ChatPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetMessageResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._chatList = new ObservableCollection<Chat>(response.Obj.Chats);
                    presenter.ChatListView.ItemsSource = presenter._chatList;


                });
            }


        }
        public class CheckExistPresenterCallback : ICheckExistPresenterCallback
        {

            ChatPage presenter;
            public CheckExistPresenterCallback(ChatPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<CheckExistResponse> response)
            {

                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    presenter._Exist = response.Obj.Exist;
                    if (presenter._Exist)
                    {
                        presenter.Starter.Visibility = Visibility.Collapsed;
                        presenter.Chaat.Visibility = Visibility.Visible;
                        presenter.Commands.Visibility = Visibility.Visible;
                        var getMessageRequest = new GetMessageRequest(presenter._currentUser, presenter._anotherUser);
                        GetMessage getMessage = new GetMessage(getMessageRequest, new GetMessagePresenterCallback(presenter));
                        getMessage.Execute();

                    }
                    else
                    {
                        presenter.Starter.Visibility = Visibility.Visible;
                        presenter.Chaat.Visibility = Visibility.Collapsed;
                        presenter.Commands.Visibility = Visibility.Collapsed;
                    }

                });
            }


        }
    }
}
