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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User currentUser;
        public MainWindow()
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            
            Create_Post cp = new Create_Post();
            cp.Owner = this;
            cp.Show();
            this.Hide();
            
        }

        public void OpenPost(Post postActual)
        {
            PostWindow postWindow = new PostWindow();
            postWindow.postActual = postActual;
            postWindow.Owner = this;
            postWindow.Show();
        }
    }
}
