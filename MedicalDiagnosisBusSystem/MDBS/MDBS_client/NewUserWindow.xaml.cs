using Core;
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

namespace MDBS_client
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        CoreFunc Сore = new CoreFunc();

        public string FullName
        {
            get { return UserNameBox.Text; }
        }

        public string Login
        {
            get { return LoginBox.Text; }
        }

        public string Password
        {
            get { return PasswordBox.Password; }
        }

        public string RepeatPassword
        {
            get { return RepeatPasswordBox.Password; }
        }

        public NewUserWindow(Guid UserID)
        {
            InitializeComponent();

            LoginBox.Text = "";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        ///<summary>
        /// Создание нового пользователя
        ///</summary>
        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FullName))
            {
                MessageBox.Show("Имя пользователя не заполнено!");
                return;
            }

            if (this.Password != this.RepeatPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            if (this.Password.Length < 5)
            {
                MessageBox.Show("Пароли недостаточно надежный!");
                return;
            }

            var passwordHash = Password.GetHashCode();

            Сore.CreateUser(
                this.FullName,
                this.Login,
                passwordHash.ToString());

            MessageBox.Show("Пользователь успешно создан!");

            this.DialogResult = true;
        }
    }
}