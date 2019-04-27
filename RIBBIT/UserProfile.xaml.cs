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

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        User user;

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
    }
}
