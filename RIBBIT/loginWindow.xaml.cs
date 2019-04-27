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
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;

namespace RIBBIT
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        //JsonSerializer serializer = new JsonSerializer();
        List<User> users = new List<User>();

        public loginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            StringBuilder sb = encrypt();   //passwordhash

            //users = JsonConvert.DeserializeObject("/JSONs/users.json", typeof(List<User>));
            //File.WriteAllText("/JSONs/users.json", JsonConvert.SerializeObject())


            /*foreach string name in json.usersc
             * if name==username, error message: name already in use
             * else add new user with username
             */
        }

        private StringBuilder encrypt()
        {
            string password = txtPassword.Password;
            SHA512 sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            lblOutput.Content = sb.ToString();
            sha.Dispose();
            return sb;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string username = txtUsername.Text;
                StringBuilder sb = encrypt();
            }
        }
    }
}
