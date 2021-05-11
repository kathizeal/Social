﻿using System;
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
using System.Collections.ObjectModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /* public sealed partial class AccountPage : Page
     {

         Post post;

         UserManager userManager = UserManager.GetInstance();
         PostManager postManager = PostManager.GetInstance();
         private ObservableCollection<Post> _MyPost;
         public ObservableCollection<Post> MyPost
         {
             get { return this._MyPost; }
         }
         User _CurrentUser;
         public AccountPage()
         {
             this.InitializeComponent();
         }
         protected override void OnNavigatedTo(NavigationEventArgs e)
         {
             base.OnNavigatedTo(e);
             _CurrentUser = (User)e.Parameter;
             UserNameBlock.Text ="User name : "+ _CurrentUser.UserName;
             LastNameBlock.Text = "Last name : " + _CurrentUser.LastName;
             UserIDBlock.Text = "User Id : " + _CurrentUser.UserId.ToString();
             EmailBlock.Text = "Email address : " + _CurrentUser.Email;
             BirthdayBlock.Text = "DOB : " + _CurrentUser.BirthDay;
             GenderBlock.Text = "Gender : " + _CurrentUser.Gender;
             _MyPost = new ObservableCollection<Post>(postManager.ViewMyPost(_CurrentUser.UserId));
         }

         private void SignOutButton_Click(object sender, RoutedEventArgs e)
         {

             //ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
             userManager.SignOut();

            // mainPage.mainFramePage().Navigate(typeof(MainPage));
             MainPage.MainFramePage.Navigate(typeof(MainPage));
             //Frame.Navigate(typeof(MainPage));
         }



         private void Edit_Click(object sender, RoutedEventArgs e)
         {
             postManager.DeletePost(post);
             Post SelectedPost = postManager.Edit(post);

             PostPage.SecondaryFrame.Navigate(typeof(EditPostPage), SelectedPost);
             //Frame.Navigate(typeof(EditPostPage), SelectedPost);


         }

         private void MyPost_ItemClick(object sender, ItemClickEventArgs e)
         {
             post =(Post)e.ClickedItem;


         }

         private void Delete_Click(object sender, RoutedEventArgs e)
         {
             postManager.DeletePost(post);
             Frame.Navigate(typeof(PostPage), _CurrentUser);
            // Frame.Navigate(typeof(PostPage), _CurrentUser);

         }
     }*/
    public sealed partial class AccountPage : Page
    {
        Post post;
        User _CurrentUser;
        DispatcherTimer Timer = new DispatcherTimer();
        UserManager userManager = UserManager.GetInstance();
       PostManager postManager = PostManager.GetInstance();
      private ObservableCollection<Post> _MyPost;
        public ObservableCollection<Post> MyPost
        {
            get { return this._MyPost; }
        }
        public AccountPage()
        {
            this.InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
           
           // Date.Text = DateTime.Today.ToShortDateString();
        }
        private void Timer_Tick(object sender, object e)
        {
           // Time.Text = DateTime.Now.ToString("h:mm:ss tt");
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _CurrentUser = (User)e.Parameter;
            
            UserNameBlock.Text =  _CurrentUser.UserName;
            LastNameBlock.Text = _CurrentUser.LastName;
            UserIDBlock.Text   =  _CurrentUser.UserId.ToString();
            EmailBlock.Text    =_CurrentUser.Email;
            BirthdayBlock.Text = _CurrentUser.BirthDay;
            GenderBlock.Text   = _CurrentUser.Gender;
            _MyPost = new ObservableCollection<Post>(postManager.ViewMyPost(_CurrentUser.UserId));
            if(_MyPost.Count==0)
                NoPost.Visibility = Visibility.Visible;
            else
                NoPost.Visibility = Visibility.Collapsed;
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {

            //ApplicationData.Current.LocalSettings.Values["UserClass"] = null;
            userManager.SignOut();
            MainPage mainPage = new MainPage();
            
           // mainPage.mainFrame().Navigate(typeof(SignInPage));

            // mainPage.mainFramePage().Navigate(typeof(MainPage));
            //.Navigate(typeof(SignInPage));
            Frame.Navigate(typeof(MainPage));
        }

       private void Edit_Click(object sender, RoutedEventArgs e)
        {
            postManager.DeletePost(post);
            Post SelectedPost = postManager.Edit(post);

            Frame.Navigate(typeof(EditPostPage), SelectedPost);
            //Frame.Navigate(typeof(EditPostPage), SelectedPost);


        }

        private void MyPost_ItemClick(object sender, ItemClickEventArgs e)
        {
            post = (Post)e.ClickedItem;
            SampleText.Visibility = Visibility.Collapsed;
            ViewPost.Visibility = Visibility.Visible;
            PostContent.Text = post.PostContent;    
            
           


        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            postManager.DeletePost(post);
            Frame.Navigate(typeof(AccountPage), _CurrentUser);
            // Frame.Navigate(typeof(PostPage), _CurrentUser);

        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            //Frame.GoBack();
            Frame.Navigate(typeof(PostPage), _CurrentUser);
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            Intro.Visibility = Visibility.Collapsed;
            InsideFrame.Visibility = Visibility.Visible;
            //this.Frame.Navigate(typeof(MyPostPage),_CurrentUser);
        }

        private void Editbt_Click(object sender, RoutedEventArgs e)
        {
            postManager.DeletePost(post);
            Post SelectedPost = postManager.Edit(post);

            Frame.Navigate(typeof(EditPostPage), SelectedPost);
        }

        private void Deletebt_Click(object sender, RoutedEventArgs e)
        {
            postManager.DeletePost(post);
            Frame.Navigate(typeof(AccountPage), _CurrentUser);
        }

        private void GoAccount_Click(object sender, RoutedEventArgs e)
        {

            Intro.Visibility = Visibility.Visible;
            InsideFrame.Visibility = Visibility.Collapsed;
        }

        private void ViewPost_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewPost),post);

        }

       
    }

}
