using Microsoft.Win32;
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
    /// Interaction logic for userSettings.xaml
    /// </summary>
    public partial class UserSettings : Window
    {
        User user;
        List<User> users;
        string replace;

        public UserSettings(User user)
        {
            InitializeComponent();
            this.user = user;
            users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"....\Debug\Users\users.json")); //deserealize
            txtDisplayName.Text = user.displayName;
            txtAbout.Text = user.about;
            //imgProfile.Source = new BitmapImage(new Uri(show, UriKind.RelativeOrAbsolute));
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (User search in users)
            {
                if (search.username==user.username)
                {
                    search.displayName = txtDisplayName.Text;
                    search.about = txtAbout.Text;
                    search.profilePicture = user.profilePicture;
                    user = search;
                    break;
                }
            }
            File.WriteAllText(@"....\Debug\Users\users.json", JsonConvert.SerializeObject(users)); //serialize
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UserProfile profile = new UserProfile(user);
            profile.Owner = Owner; //maybe no se necesita
            profile.Show();
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            string pathNew = string.Empty;
            //string pathback = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All supported graphics|*.jpg;*jpeg;*.png|"+
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg;|"+
                "Portable Network Graphic (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
            }
            pathNew = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Users\pics\"+user.username+".jpg");
            //imgProfile.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            replacePic(path,pathNew);
        }
        public void replacePic(string path, string pathNew)
        {
            File.Copy(path, pathNew, true);
        }
    }
}
