using Social.Data;
using Social.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Social.Control
{
    public sealed partial class SinglePostControl : UserControl
    {
        string content;
        string createdTime;

        public static readonly DependencyProperty CreatedTimeProp = DependencyProperty.Register("CreatedString", typeof(string), typeof(SinglePostControl), new PropertyMetadata(null));
        public string CreatedString
        {
            get { return (string)GetValue(CreatedTimeProp); }
            set { SetValue(CreatedTimeProp, value); }
        }


        public static readonly DependencyProperty ContentProp = DependencyProperty.Register("ContentString", typeof(string), typeof(SinglePostControl), new PropertyMetadata(null));
        public string ContentString
        {
            get { return (string)GetValue(ContentProp); }
            set => SetValue(ContentProp, value);
        }

        public SinglePostControl()
        {
            this.InitializeComponent();
        }
        private void StackPanel_Loaded_1(object sender, RoutedEventArgs e)
        {
            content = ContentString;
            createdTime = CreatedString;
            ContentTB.Text = content;
            CommentTimeTB.Text = createdTime;
        }
    }
}
