using Social.Data;
using Social.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ChatPage : Page
    {
        User _CurrentUser;
        User _AnotherUser;
        bool Exist;
        UserManager _UserManager = UserManager.GetInstance();
        PostManager _PostManager = PostManager.GetInstance();
        ObservableCollection<Chat> chatList= new ObservableCollection<Chat>();
        public ChatPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _AnotherUser = (User)e.Parameter;

            _CurrentUser = _UserManager.Current();
            _CurrentUser = _UserManager.Find(_CurrentUser.UserId);
            chatList = _UserManager.Message(_CurrentUser,_AnotherUser);
            Exist=_UserManager.CheckExist(_CurrentUser, _AnotherUser);
            if(Exist)
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
            }

           
        }

        private void EnableEvent(object sender, RoutedEventArgs e)
        {
            _UserManager.CreateChat(_CurrentUser, _AnotherUser);
            chatList = _UserManager.Message(_CurrentUser, _AnotherUser);
            ChatListView.ItemsSource = chatList;
            Starter.Visibility = Visibility.Collapsed;
            Chaat.Visibility = Visibility.Visible;
            Commands.Visibility = Visibility.Visible;
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            chat.SenderId = _CurrentUser.UserId;
            chat.SenderName = _CurrentUser.UserName;
            chat.RecieverId = _AnotherUser.UserId;
            chat.RecieverName = _AnotherUser.UserName;
            chat.ProfilePic = _CurrentUser.ProfilePic;
            chat.Msg = CommentTextBox.Text;
            chatList.Add(chat);
            
           
            _UserManager.AddChat(chat);
        }
    }
}
