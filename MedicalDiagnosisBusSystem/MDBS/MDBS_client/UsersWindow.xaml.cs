using Core;
using DataModels;
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
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        List<User> Users = new List<User>();
        Guid UserID;

        public UsersWindow(Guid userId)
        {
            InitializeComponent();

            var core = new CoreFunc();

            UserID = userId;
            Users = core.GetUsers(UserID);
            UserGrid.ItemsSource = Users;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        ///<summary>
        /// Формирование таблицы пользователей
        ///</summary>
        public void UserGridColumnsGenerated(object sender, EventArgs e)
        {
            UserGrid.Columns[0].Visibility = Visibility.Collapsed;

            UserGrid.Columns[1].Header = "Имя пользователя";
            UserGrid.Columns[2].Header = "Номер доктора (логин)";

            UserGrid.Columns[1].Width = 200;
            UserGrid.Columns[2].Width = 200;
        }

        ///<summary>
        /// Создание нового пользователя
        ///</summary>
        public void NewUser(object sender, EventArgs e)
        {
            NewUserWindow newUsersWindow = new NewUserWindow(UserID);

            if (newUsersWindow.ShowDialog() == true)
            {
                var core = new CoreFunc();

                Users = core.GetUsers(UserID);
                UserGrid.ItemsSource = Users;
            }
            else
            {
            }
        }
    }
}