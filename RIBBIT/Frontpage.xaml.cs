using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for Frontpage.xaml
    /// </summary>
    public partial class Frontpage : Window
    {
        public Frontpage()
        {
            InitializeComponent();
        }

        List<Post> postList = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
        List<int> indices = new List<int>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillListView(postList);
        }

        private void fillListView(List<Post> allPosts)
        {
            foreach (var item in allPosts)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                if (item.images != "")
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(item.images, UriKind.Absolute));
                    img.Width=200;
                    stack.Children.Add(img);
                }
                TextBlock txt = new TextBlock();
                txt.Text = item.title + " ... Uploaded by: " + item.owner;
                stack.Children.Add(txt);
                lvPosts.Items.Add(stack);
                indices.Add(item.ID);
            }
        }
        private void lvPosts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<Post> postList = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
            if (lvPosts.SelectedIndex>=0)
            {
                (Owner as MainWindow).OpenPost(postList[indices[lvPosts.SelectedIndex]]);
                Close();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            indices = new List<int>();
            lvPosts.Items.Clear();

            foreach (var posts in postList)
            {
                if (posts.title.Contains(txtSearch.Text))
                {
                    StackPanel stack = new StackPanel();
                    stack.Orientation = Orientation.Horizontal;
                    if (posts.images != "")
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri(posts.images, UriKind.Absolute));
                        img.Width = 200;
                        stack.Children.Add(img);
                    }
                    TextBlock txt = new TextBlock();
                    txt.Text = posts.title + " ... Uploaded by: " + posts.owner;
                    stack.Children.Add(txt);
                    lvPosts.Items.Add(stack);
                    indices.Add(posts.ID);
                }
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            UserProfile userProfile = new UserProfile((Owner as MainWindow).currentUser);
            userProfile.Show();
            Close();
        }
    }
}
