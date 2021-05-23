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
using Social.Model;
using Social.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Social.FramePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
  
    public sealed partial class SecondaryPage : Page 
    {
      
        Post  _CurrentPost;
        User _CurrentUser;
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        private ObservableCollection<Comment> _PostComments;
        public ObservableCollection<Comment> PostComments { get { return this._PostComments; }  }
        public SecondaryPage()
        {
            this.InitializeComponent();
            _CurrentUser = _UserManager.Current();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           _CurrentPost = (Post)e.Parameter;        
           _PostComments = new ObservableCollection<Comment>(_CurrentPost.Comments);
           
        }
    }



}
