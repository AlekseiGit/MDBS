﻿using System;
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
using Core;
using DataModels;
using System.Collections.ObjectModel;

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(ObservableCollection<string> logins)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoginBox.ItemsSource = logins;
        }

        public static User CurrentUser { get; set; }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Password;
            var passwordHash = password.GetHashCode();
            var core = new CoreFunc();

            var user = core.CheckUser(login, passwordHash.ToString(), 0);

            if (user.ID != Guid.Empty)
            {
                CurrentUser = user;
                this.DialogResult = true;
            }
            else
            {
                ErrorMessage.Text = "Неправильный логин или пароль!";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}