using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Social.FramePage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Social.Model;
using Social.Data;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Social.Util;
using Social.Domain;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Social
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
  
    public sealed partial class MainPage : Page
    {
        //UserManager _UserManager = UserManager.GetInstance();
        //PostManager _PostManager = PostManager.GetInstance();
        public MainPage()
        {
            this.InitializeComponent();
            var stateRequest = new StateRequest();
            State state = new State(stateRequest,new  StatePresenterCallBack(this));
            state.Execute();
           


        }
        public class StatePresenterCallBack : IStatePresenterCallback
        {
            MainPage presenter;
            public StatePresenterCallBack(MainPage view)
            {
                presenter = view;
            }

            public void OnFailed()
            {
                throw new NotImplementedException();
            }

            public async void OnSuccess(Response<StateResponse> response)
            {
                await presenter.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (response.Obj.State == false)
                    {
                        // _UserManager.CreateTable();
                        presenter.MainFrame.Navigate(typeof(SignInPage));
                    }
                    else
                    {
                        //_UserManager.CreateTable();
                        //_PostManager.CreateTable();
                        presenter.MainFrame.Navigate(typeof(PostPage));
                    }


                }
                );
            }


        }

    }
}
