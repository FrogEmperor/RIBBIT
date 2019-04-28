using Microsoft.Win32;
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
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for Create_Post.xaml
    /// </summary>
    public partial class Create_Post : Window
    {
        public Create_Post()
        {
            InitializeComponent();
        }



        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Portable Network Graphic (*.png)|*.png";
                if (dialog.ShowDialog() == true)
                {
                    lblImageSource.Content = dialog.FileName;
                    string imageSource = dialog.FileName;
                }
            }
            catch
            {
                
            }
        }
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            List<Post> p = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText(@"Posts\Posts.json"));
            string path;
            try
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Posts\Images\" + p.Count + ".jpg");
                File.Copy(lblImageSource.Content.ToString(), path);
            }
            catch
            {
                path = String.Empty;
            }
            Post newPost = new Post((Owner as MainWindow).currentUser.username, p.Count,txtbxTitle.Text, path, txtbxTitle.Text, txtbxContent.Text);
            p.Add(newPost);
            string arregloObjects = JsonConvert.SerializeObject(p, Formatting.Indented);
            File.WriteAllText(@"Posts\Posts.json", arregloObjects);
            txtbxContent.Text = String.Empty;
            txtbxTitle.Text = String.Empty;
            lblImageSource.Content = String.Empty;
            (this.Owner as MainWindow).OpenPost(newPost); 
            this.Close();
            
        }
    }
}
