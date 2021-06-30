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
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewView : Page
    {
        public AppWindow MyAppWindow { get; set; }
        public NewView()
        {
            this.InitializeComponent();
        }
        private ObservableCollection<Post> _PostList;
        public ObservableCollection<Post> PostList
        {
            get { return _PostList; }
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var getPostRequest = new GetPostRequest();
            GetPost getPost = new GetPost(getPostRequest, new GetPostPresenterCallBack(this));
            getPost.Execute();
            SocialNotification.PostAdded += HandlePostAdded;
            SocialNotification.PostDeleted += HandlePostDeleted;
        }
        private async void HandlePostAdded(Post post)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                _PostList.Add(post);
            });
        }
        private async void HandlePostDeleted(Post post)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                _PostList.Remove(post);
                var getPostRequest = new GetPostRequest();
                GetPost getPost = new GetPost(getPostRequest, new GetPostPresenterCallBack(this));
                getPost.Execute();
            });
        }

        private void StackPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            SocialNotification.PostAdded -= HandlePostAdded;
            SocialNotification.PostDeleted -= HandlePostDeleted;
        }
        public class GetPostPresenterCallBack : IGetPostsPresenterCallback
        {
            NewView presenter;
            public GetPostPresenterCallBack(NewView view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<GetPostsResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {

                    presenter._PostList = new ObservableCollection<Post>(response.Obj.Posts);
                    presenter.ClickList.ItemsSource = presenter._PostList;
                }
                );
            }


        }

    }
}
