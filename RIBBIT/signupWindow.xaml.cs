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
    /// Interaction logic for signupWindow.xaml
    /// </summary>
    public partial class signupWindow : Window
    {
        List<User> users = new List<User>();

        public signupWindow(List<User> users)
        {
            this.users = users;
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Signup();
            }
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            Signup();
        }

        private void Signup()
        {
            bool canAdd = true;
            string email = txtEmail.Text;
            string username = txtUser.Text;
            string password = loginWindow.encrypt(txtPassword.Password).ToString();
            foreach (User user in users)
            {
                if (username == user.username || email == user.email)
                {
                    canAdd = false;
                    break;
                }
            }
            if (canAdd && email != string.Empty && username != string.Empty && txtPassword.Password != string.Empty)
            {
                users.Add(new User(username, password, email));
                File.WriteAllText(@"....\Debug\Users\users.json", JsonConvert.SerializeObject(users)); //serialize
            }
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            loginWindow login = new loginWindow();
            login.Show();
            login.Owner = Owner;
        }
    }
}
