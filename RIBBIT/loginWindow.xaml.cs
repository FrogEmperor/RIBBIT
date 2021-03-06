﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
            List<User> users;

            public loginWindow()
            {
                InitializeComponent();
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Users\users.json")); //deserealize
            }

            private void BtnLogin_Click(object sender, RoutedEventArgs e)
            {
                Login();
            }

            private void Login()
            {
                string username = txtUsername.Text;
                StringBuilder sb = encrypt(txtPassword.Password);   //passwordhash

                foreach (User user in users)
                {
                    if (username == user.username && sb.ToString() == user.password)
                    {
                        (Owner as MainWindow).currentUser = user;
                        (Owner as MainWindow).OpenProfile(user);
                        this.Close();
                        //lblOutput.Content = "hola";
                        //frontpage fp = new frontpage(user);
                        return;
                    }
                }
                MessageBox.Show("Wrong combination");
            }

            public static StringBuilder encrypt(string password)
            {
                SHA512 sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                sha.Dispose();
                return sb;
            }

            private void Window_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    Login();
                }
            }

            private void BtnSignup_Click(object sender, RoutedEventArgs e)
            {
                signupWindow signup = new signupWindow(users);
                signup.Owner = this.Owner;
                signup.Show();
                Close();
            }
    }
}
