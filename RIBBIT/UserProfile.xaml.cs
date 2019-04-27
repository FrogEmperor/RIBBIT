using System;
using System.Collections.Generic;
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
using System.IO;
using Newtonsoft.Json;

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        User user;
        List<int> indices = new List<int>();
        List<Post> postList = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));

        public UserProfile(User user)
        {
            InitializeComponent();
            this.user = user;
            lblUsername.Content = user.username;
            lblKarma.Content = user.postKarma + user.commentKarma;
            lblBirthday.Content = user.accountBirthday;
            listviewTrophies.ItemsSource = user.trophies;
            imgPic.Source = new BitmapImage(new Uri(user.profilePicture, UriKind.RelativeOrAbsolute));
            lblDisplayname.Content = user.displayName;
            txtblockAbout.Text = user.about;
            
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            UserSettings settings = new UserSettings(user);
            settings.Owner = Owner;
            settings.Show();
            Close();
        }

        private void btnCreatePost_Click(object sender, RoutedEventArgs e)
        {
            Create_Post createPost = new Create_Post();
            createPost.Owner = Owner;
            createPost.Show();
            Close();
        }

        private void btnFrontPage_Click(object sender, RoutedEventArgs e)
        {
            Frontpage frontpage = new Frontpage();
            frontpage.Owner = Owner;
            frontpage.Show();
            Close();
        }

        private void fillListView(List<int> add)
        {
            indices = new List<int>();
            listviewProfile.Items.Clear();
            for (int i =0; i<add.Count(); i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                if (postList[add[i]].images != "")
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(postList[add[i]].images, UriKind.Absolute));
                    img.Width = 200;
                    stack.Children.Add(img);
                }
                TextBlock txt = new TextBlock();
                txt.Text = postList[add[i]].title + " ... Uploaded by: " + postList[add[i]].owner;
                stack.Children.Add(txt);
                listviewProfile.Items.Add(stack);
                indices.Add(postList[add[i]].ID);
            }
            listviewProfile.Items.Refresh();  
        }

        private void ListviewProfile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listviewProfile.SelectedIndex >= 0)
            {
                (Owner as MainWindow).OpenPost(postList[indices[listviewProfile.SelectedIndex]]);
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillListView(user.posts);
        }

        private void BtnOverview_Click(object sender, RoutedEventArgs e)
        {
            List<int> over = user.posts.Concat(user.comments).ToList();
            fillListView(over);
        }

        private void BtnPosts_Click(object sender, RoutedEventArgs e)
        {
            fillListView(user.posts);
        }

        private void BtnComments_Click(object sender, RoutedEventArgs e)
        {
            fillListView(user.comments);
        }

        private void BtnSaved_Click(object sender, RoutedEventArgs e)
        {
            //fillListView(user.saved);
        }

        private void BtnHidden_Click(object sender, RoutedEventArgs e)
        {
            //fillListView(user.hidden);
        }

        private void BtnUpvoted_Click(object sender, RoutedEventArgs e)
        {
            //fillListView(user.upvoted);
        }

        private void BtnDownvoted_Click(object sender, RoutedEventArgs e)
        {
            //fillListView(user.downvoted);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            (Owner as MainWindow).currentUser = null;
            loginWindow login = new loginWindow();
            login.Show();
            Close();
        }
    }
}
