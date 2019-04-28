using Newtonsoft.Json;
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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        User user;
        List<int> indices = new List<int>();
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

        private void btnLvSaved_Click(object sender, RoutedEventArgs e)
        {
            indices.Clear();
            listviewProfile.Items.Clear();
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
            foreach (var item in (Owner as MainWindow).currentUser.saves)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                if (posts[item].images != "")
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(posts[item].images, UriKind.Absolute));
                    img.Width = 200;
                    stack.Children.Add(img);
                }
                TextBlock txt = new TextBlock();
                txt.Text = posts[item].title + " ... Uploaded by: " + posts[item].owner;
                stack.Children.Add(txt);
                listviewProfile.Items.Add(stack);
                indices.Add(posts[item].ID);
            }
            listviewProfile.UnselectAll();
        }

        private void listviewProfile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
            (Owner as MainWindow).OpenPost(posts[indices[listviewProfile.SelectedIndex]]);
            this.Close();
        }
        

        private void fillListView(List<int> add)
        {
            List<Post> postList = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
            indices = new List<int>();
            listviewProfile.Items.Clear();
            for (int i = 0; i < add.Count(); i++)
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
        private void btnPosts_Click(object sender, RoutedEventArgs e)
        {
            fillListView((Owner as MainWindow).currentUser.posts);
        }
    }
}
