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
    /// Interaction logic for TadPole.xaml
    /// </summary>
    public partial class TadPole : Window
    {
        public Tadpole tadpole;
        public List<int> indices;

        public TadPole()
        {
            InitializeComponent();
            //tadpole = new Tadpole();
        }

        private void btnCreatePost_Click(object sender, RoutedEventArgs e)
        {
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"....\Posts\Posts.json"));
            (Owner as MainWindow).OpenPost(posts[],tadpole)
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"....\Posts\Posts.json"));
            foreach (var post in tadpole.posts)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                if (posts[post].images != "")
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(posts[post].images, UriKind.Absolute));
                    img.Width = 200;
                    stack.Children.Add(img);
                }
                TextBlock txt = new TextBlock();
                txt.Text = posts[post].title + " ... Uploaded by: " + posts[post].owner;
                stack.Children.Add(txt);
                listviewPosts.Items.Add(stack);
                indices.Add(posts[post].ID);
            }

        }
    }
}
