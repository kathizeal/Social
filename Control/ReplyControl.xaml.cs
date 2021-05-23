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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Social.Control
{
    public sealed partial class ReplyControl : UserControl
    {
        PostManager _PostManager = PostManager.GetInstance();
        UserManager _UserManager = UserManager.GetInstance();
        User _CurrentUser;
        Post _CurrentPost;
        Comment _CurrentComment;

        public static readonly DependencyProperty ParentIdProp = DependencyProperty.Register("ParentId", typeof(long), typeof(ReplyControl), new PropertyMetadata(null));
        public long ParentId
        {
            get { return (long)GetValue(ParentIdProp); }
            set { SetValue(ParentIdProp, value); }
        }
        private ObservableCollection<Comment> _PostComment;
        public ObservableCollection<Comment> PostComment { get { return this._PostComment; } }
        public ReplyControl()
        {
            this.InitializeComponent();
        }
        private void ReplyStack_Loaded(object sender, RoutedEventArgs e)
        {

            _CurrentComment = _PostManager.GetComment(ParentId);
            _PostComment = _PostManager.GetReply(_CurrentComment.CommentId);
            _PostComment = _PostManager.DateChangeComment(_PostComment);
            _CurrentUser = _UserManager.Current();
            _CurrentUser = _UserManager.Find(_CurrentUser.UserId);
            _CurrentPost = _PostManager.ViewPost(_CurrentComment.PostId);
            _CurrentUser.ProfilePic = _UserManager.ProfilePic(_CurrentUser);
            ReplyStack.Visibility = Visibility.Visible;
                ReplyList.ItemsSource = _PostComment;
           
            
        }
        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                Comment newReply = new Comment();
                newReply.ProfilePic = _CurrentUser.ProfilePic;
                newReply.PostId = _CurrentComment.PostId;
                newReply.CommentContent = CommentTextBox.Text;
                newReply.CommenterName = _CurrentUser.UserName;
                newReply.CommenterId = _CurrentUser.UserId;
                newReply.ParentCommentId = _CurrentComment.CommentId;
                _CurrentComment.CurrentReply = newReply;
                _PostComment.Add(newReply);
                _PostManager.AddReply(_CurrentPost, _CurrentComment, newReply);
                _PostComment = _PostManager.DateChangeComment(_PostComment);
                CommentTextBox.Text = "";
               
            }
        }
    }
}
