﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        
        public Post postActual { get; set; }
        private List<Comment> comments = new List<Comment>();
        private Comment commentShown;
        public PostWindow()
        {
            InitializeComponent();
        }
        private void HideImage()
        {
            canvasMedia.Visibility = Visibility.Hidden;
            canvasShadow.Visibility = Visibility.Hidden;
            btnCloseimage.Visibility = Visibility.Hidden;
            btnCloseimage.IsHitTestVisible = false;
            canvasShadow.IsHitTestVisible = false;
            canvasMedia.IsHitTestVisible = false;
        }
        private void ShowImage()
        {
            canvasShadow.Visibility = Visibility.Visible;
            canvasMedia.Visibility = Visibility.Visible;
            btnCloseimage.Visibility = Visibility.Visible;
            btnCloseimage.IsHitTestVisible = true;
            canvasShadow.IsHitTestVisible = true;
            canvasMedia.IsHitTestVisible = true;
            try
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(postActual.images, UriKind.Absolute));

                canvasMedia.Children.Add(img);
                Canvas.SetLeft(img, (canvasMedia.ActualWidth / 2 - img.Width / 2));
                Canvas.SetTop(img, (canvasMedia.ActualHeight / 2 - img.Height / 2));
            }
            catch
            {

            }
        }
        
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideReply();
            Comments(listbxComments, postActual.comments);
            txtblkUser.Text = postActual.owner;
            txtblkTitle.Text = postActual.title;
            txtblkContent.Text = postActual.content;
            HideImage();
            if(postActual.images == String.Empty)
            {
                btnShowImage.Visibility = Visibility.Visible;
            }
        }
        private void btnShowImage_Click(object sender, RoutedEventArgs e)
        {
            ShowImage();
        }
        private void btnCloseimage_Click(object sender, RoutedEventArgs e)
        {
            HideImage();
        }
        private void txtblkUser_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //abrir el perfil de usuario
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            List<Post> posts = JsonConvert.DeserializeObject    <List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText(@"Posts\Comments.json"));
            Comment NewComment = new Comment((Owner as MainWindow).currentUser.username, comments.Count, txtbxNewComment.Text);
            postActual.comments.Add(comments.Count);
            posts[postActual.ID].comments.Add(comments.Count);
            comments.Add(NewComment);
            this.comments.Add(NewComment);
            string arregloObjects = JsonConvert.SerializeObject(comments, Formatting.Indented);
            File.WriteAllText(@"Posts\Comments.json", arregloObjects);
            listbxComments.Items.Clear();
            Comments(listbxComments, postActual.comments);
            string arregloObjects2 = JsonConvert.SerializeObject(posts, Formatting.Indented);
            File.WriteAllText(@"Posts\Posts.json", arregloObjects2);
        }

        private void Comments(ListBox listBox, List<int> comments)
        {
            List<Comment> commentsTemp = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText(@"Posts\Comments.json"));
            foreach (var item in comments)
            {
                listBox.Items.Add(commentsTemp[item]);
                if (commentsTemp[item].comments.Count>0)
                {
                    ListBox lb = new ListBox();
                    Comments(lb,commentsTemp[item].comments);
                    listBox.Items.Add(lb);
                }
            }
            listBox.SelectionMode = SelectionMode.Single;
            listBox.MouseDoubleClick += new MouseButtonEventHandler(ShowReply);
        }

        private void HideReply()
        {
            canvasShadow.Visibility = Visibility.Hidden;
            canvasShadow.IsHitTestVisible = false;
            canvasComment.Visibility = Visibility.Hidden;
            txtReply.Visibility = Visibility.Hidden;
            btnReport.Visibility = Visibility.Hidden;
            txtReply.Visibility = Visibility.Hidden;
            btnReport.Visibility = Visibility.Hidden;
            btnSendResponse.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            btnAward.Visibility = Visibility.Hidden;
            btnUpvote.Visibility = Visibility.Hidden;
            btnDownVote.Visibility = Visibility.Hidden;
            btnCloseReply.Visibility = Visibility.Hidden;
            lblReply.Visibility = Visibility.Hidden;
            txtReply.Text = String.Empty;
        }

        private void btnCloseReply_Click(object sender, RoutedEventArgs e)
        {
            HideReply();
        }

        private void ShowReply(object sender, MouseButtonEventArgs e)
        {
            canvasShadow.Visibility = Visibility.Visible;
            canvasShadow.IsHitTestVisible = true;
            txtReply.IsHitTestVisible = true;
            canvasComment.Visibility = Visibility.Visible;
            txtReply.Visibility = Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnSendResponse.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
            btnAward.Visibility = Visibility.Visible;
            btnUpvote.Visibility = Visibility.Visible;
            btnDownVote.Visibility = Visibility.Visible;
            btnCloseReply.Visibility = Visibility.Visible;
            lblReply.Visibility = Visibility.Visible;
            if((sender as ListBox).SelectedItem is Comment)
            {
                commentShown = ((sender as ListBox).SelectedItem as Comment);
            }
            (sender as ListBox).UnselectAll();
        }

        private void btnSendResponse_Click(object sender, RoutedEventArgs e)
        {
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText(@"Posts\Comments.json"));
            Comment c = new Comment((Owner as MainWindow).currentUser.username, comments.Count, txtReply.Text);
            comments.Add(c);
            comments[commentShown.ID].comments.Add(c.ID); 
            string arregloObjects = JsonConvert.SerializeObject(comments, Formatting.Indented);
            File.WriteAllText(@"Posts\Comments.json", arregloObjects);
            HideReply();
            listbxComments.Items.Clear();
            Comments(listbxComments,postActual.comments);
        }

        private void btnUpvote_Click(object sender, RoutedEventArgs e)
        {
            commentShown.upvote += 1;
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText(@"Posts\Comments.json"));
            comments[commentShown.ID].upvote += 1;
            string arregloObjects = JsonConvert.SerializeObject(comments, Formatting.Indented);
            File.WriteAllText(@"Posts\Comments.json", arregloObjects);
            HideReply();
            listbxComments.Items.Clear();
            Comments(listbxComments, postActual.comments);
        }

        private void btnDownVote_Click(object sender, RoutedEventArgs e)
        {
            commentShown.downvote += 1;
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText(@"Posts\Comments.json"));
            comments[commentShown.ID].downvote += 1;
            string arregloObjects = JsonConvert.SerializeObject(comments, Formatting.Indented);
            File.WriteAllText(@"Posts\Comments.json", arregloObjects);
            HideReply();
            listbxComments.Items.Clear();
            Comments(listbxComments, postActual.comments);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //(Owner as MainWindow).currentUser.saves.Add(postActual.ID);
        }

        private void btnSavePost_Click_1(object sender, RoutedEventArgs e)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Users\users.json"));
            (Owner as MainWindow).currentUser.saves.Add(postActual.ID);
            for(int i = 0; i<users.Count;i++)
            {
                if(users[i].username == (Owner as MainWindow).currentUser.username)
                {
                    users[i].saves.Add(postActual.ID); 
                }
            }
            string arregloObjects = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(@"Users\users.json", arregloObjects);
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            (Owner as MainWindow).OpenProfile((Owner as MainWindow).currentUser);
            this.Close();
        }

        private void btnUpvote_Copy_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < (Owner as MainWindow).currentUser.vote.Count; i++)
            {
                if ((Owner as MainWindow).currentUser.vote[i][0] == postActual.ID)
                {
                    if ((Owner as MainWindow).currentUser.vote[i][1]==0)
                    {
                        postActual.upvote += 2;
                        (Owner as MainWindow).currentUser.vote[i][1] = 1;
                        List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Post.json"));
                        posts[postActual.ID].upvote += 1;
                        string arregloObjects2 = JsonConvert.SerializeObject(posts, Formatting.Indented);
                        File.WriteAllText(@"Posts\Post.json", arregloObjects2);
                        List<User> usuarios2 = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Users\users.json"));
                        for (int j = 0; j < usuarios2.Count; j++)
                        {
                            if(usuarios2[j].username== (Owner as MainWindow).currentUser.username)
                            {
                                usuarios2[j].vote[i][1] = 1;
                            }
                        }
                        arregloObjects2 = JsonConvert.SerializeObject(usuarios2, Formatting.Indented);
                        File.WriteAllText(@"Posts\Post.json", arregloObjects2);
                        HideReply();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            int[] vote = { postActual.ID, 1 };
            postActual.upvote += 1;
            (Owner as MainWindow).currentUser.vote.Add(vote);
            List<User> usuarios = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Users\users.json"));
            for (int j = 0; j < usuarios.Count; j++)
            {
                if (usuarios[j].username == (Owner as MainWindow).currentUser.username)
                {
                    usuarios[j].vote.Add(vote);
                }
            }
            string arregloObjects = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(@"Posts\Post.json", arregloObjects);
            HideReply();
        }

        private void btnDownVote_Copy_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < (Owner as MainWindow).currentUser.vote.Count; i++)
            {
                if ((Owner as MainWindow).currentUser.vote[i][0] == postActual.ID)
                {
                    if ((Owner as MainWindow).currentUser.vote[i][1] == 1)
                    {
                        postActual.upvote -= 2;
                        (Owner as MainWindow).currentUser.vote[i][1] = 0;
                        List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Post.json"));
                        posts[postActual.ID].upvote += 1;
                        string arregloObjects2 = JsonConvert.SerializeObject(posts, Formatting.Indented);
                        File.WriteAllText(@"Posts\Post.json", arregloObjects2);
                        List<User> usuarios2 = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Users\users.json"));
                        for (int j = 0; j < usuarios2.Count; j++)
                        {
                            if (usuarios2[j].username == (Owner as MainWindow).currentUser.username)
                            {
                                usuarios2[j].vote[i][1] = 1;
                            }
                        }
                        arregloObjects2 = JsonConvert.SerializeObject(usuarios2, Formatting.Indented);
                        File.WriteAllText(@"Posts\Post.json", arregloObjects2);
                        HideReply();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            int[] vote = { postActual.ID, 0 };
            postActual.upvote -= 1;
            (Owner as MainWindow).currentUser.vote.Add(vote);
            List<User> usuarios = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Users\users.json"));
            for (int j = 0; j < usuarios.Count; j++)
            {
                if (usuarios[j].username == (Owner as MainWindow).currentUser.username)
                {
                    usuarios[j].vote.Add(vote);
                }
            }
            string arregloObjects = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(@"Posts\Post.json", arregloObjects);
            HideReply();
        }
    }
}
