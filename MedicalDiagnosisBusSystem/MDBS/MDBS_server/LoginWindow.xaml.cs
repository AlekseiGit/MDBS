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
using Core;
using DataModels;
using System.Collections.ObjectModel;
using System.Net;

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

            WebClient Client = new WebClient();
            string Response;

            try
            {
                Response = Client.DownloadString("http://www.microsoft.com");
            }
            catch
            {
                ErrorMessage.Text = "Отсутствует подключение к интернету!";
            }
        }

        public static User CurrentUser { get; set; }

        ///<summary>
        /// Проверка/авторизация текущего пользователя
        ///</summary>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Password;
            var passwordHash = password.GetHashCode();
            var core = new CoreFunc();

            var user = core.CheckUser(login, passwordHash.ToString(), 1);

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

        ///<summary>
        /// Выход из приложения
        ///</summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}