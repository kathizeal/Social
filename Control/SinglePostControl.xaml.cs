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

        public static readonly DependencyProperty TitleProp = DependencyProperty.Register("Title", typeof(string), typeof(SinglePostControl), new PropertyMetadata(null));
        public string Title
        {
            get { return (string)GetValue(TitleProp); }
            set { SetValue(TitleProp, value); }
        }


        public static readonly DependencyProperty ContentProp = DependencyProperty.Register("ContentString", typeof(string), typeof(SinglePostControl), new PropertyMetadata(null));
        public string ContentString
        {
            get { return (string)GetValue(ContentProp); }
            set => SetValue(ContentProp, value);
        }
        public static readonly DependencyProperty TimeProp = DependencyProperty.Register("CreatedTime", typeof(string), typeof(SinglePostControl), new PropertyMetadata(null));
        public string CreatedTime
        {
            get { return (string)GetValue(TimeProp); }
            set { SetValue(TimeProp, value); }
        }

        public SinglePostControl()
        {
            this.InitializeComponent();
        }

        private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            UserManager userManager = UserManager.GetInstance();
            User CurrentUser = userManager.Current();
            CurrentUser = userManager.FindUser(CurrentUser.UserId);
            var bitmap = new BitmapImage();
            var storageFile = await StorageFile.GetFileFromPathAsync(CurrentUser.ProfilePic);
            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
                await bitmap.SetSourceAsync(stream);
            }


            Br.ImageSource = bitmap;
            TitleCard.Text = Title;
            ContentCard.Text = ContentString;
            TimeCard.Text = CreatedTime;
        }
    }
}
