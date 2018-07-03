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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;
using DataModels;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Threading;
using System.Configuration;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing.Printing;
using MDBS_client;

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CoreFunc Core;
        public static Guid UserID;
        public static string UserDocNumber;

        public static BitmapImage[] Images = new BitmapImage[10];
        public static string[] ImageComments = new string[10];
        public static BitmapImage Image_0;
        public static BitmapImage Image_1;
        public static BitmapImage Image_2;
        public static BitmapImage Image_3;
        public static BitmapImage Image_4;
        public static BitmapImage Image_5;
        public static BitmapImage Image_6;
        public static BitmapImage Image_7;
        public static BitmapImage Image_8;
        public static BitmapImage Image_9;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            var user = Authorization();

            if (user.ID != Guid.Empty)
            {
                UserID = user.ID;
                UserDocNumber = user.DocNumber;
                LoginLabel.Header = "Вы вошли как: " + user.FullName + " (" + user.DocNumber + ")";
            }
            else
            {
                Exit(null, null);
            }

            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(MessageGrid, ItemsSourceChanged);
            }

            DialogGrid.MinRowHeight = 70;

            Core = new CoreFunc(UserID);

            SystemProcesses(null, null);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(SystemProcesses);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            IncomingImg.Source = new BitmapImage(new Uri("imgs/incoming.png", UriKind.Relative));
            OutgoingImg.Source = new BitmapImage(new Uri("imgs/outgoing.png", UriKind.Relative));
            SendingImg.Source = new BitmapImage(new Uri("imgs/sending.png", UriKind.Relative));
            ArchiveImg.Source = new BitmapImage(new Uri("imgs/archive.png", UriKind.Relative));

            UpdateFolderStructure();
        }

        public void UpdateFolderStructure()
        {
            if (!Directory.Exists(@".\Data\"))
            {
                Directory.CreateDirectory(@".\Data\");
            }

            var patients = Core.GetPatients(UserID);

            foreach (var patient in patients)
            {
                if (!Directory.Exists(@".\Data\" + patient.MedicalCardNumber))
                {
                    Directory.CreateDirectory(@".\Data\" + patient.MedicalCardNumber);
                }
            }
        }

        ///<summary>
        /// Обновление информации по категориям сообщений (входящие, исходящие...)
        ///</summary>
        public void SystemProcesses(object sender, EventArgs e)
        {
            try
            {
                var core = new CoreFunc(UserID);
                var systemData = core.GetSystemData(UserID);

                Incoming.Text = "Входящие ответы (" + systemData.IncomingInfo + ")";
                Outgoing.Text = "Исходящие запросы (" + systemData.OutgoingInfo + ")";

                core.SendMessages(UserID);
            }
            catch
            {
                return;
            }
        }

        ///<summary>
        /// Выход из приложения
        ///</summary>
        public void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        ///<summary>
        /// Вывод информации о программе
        ///</summary>
        public void Help(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();

            if (aboutWindow.ShowDialog() == true)
            {
            }
        }

        ///<summary>
        /// Печать диагноза
        ///</summary>
        private void DiagnosisPrint(object sender, RoutedEventArgs e)
        {
            if (DialogGrid.SelectedItems.Count == 1)
            {
                var dialogRow = DialogGrid.SelectedItems[0] as Dialog;

                if (!string.IsNullOrEmpty(dialogRow.TherapyPlan))
                {
                    FlowDocument doc = new FlowDocument();
                    Section sec = new Section();
                    Paragraph p0 = new Paragraph();
                    Paragraph p1 = new Paragraph();
                    //Paragraph p2 = new Paragraph();

                    //Bold bld = new Bold();
                    //bld.Inlines.Add(new Run("First Paragraph"));
                    //Italic italicBld = new Italic();
                    //italicBld.Inlines.Add(bld);
                    //Underline underlineItalicBld = new Underline();
                    //underlineItalicBld.Inlines.Add(italicBld);

                    p0.Inlines.Add("ООО МЦ \"Компания Александр\"" + "\n" +
                        "Специализированная дерматологическая клиника \"Псориаз-Центр\"" + "\n" +
                        "127005, Москва, Новолесная 1 / 49" + "\n" +
                        "Тел. 8(499)251 - 59 - 87");
                    p1.Inlines.Add("План лечения для пациента " + dialogRow.PatientName + " (" + dialogRow.MessageDate + "):" + "\n" +
                        dialogRow.TherapyPlan);

                    sec.Blocks.Add(p0);
                    sec.Blocks.Add(p1);
                    //sec.Blocks.Add(p2);
                    doc.Blocks.Add(sec);
                    doc.Name = "Doc";
                    doc.PageWidth = 800;

                    PrintDialog printDlg = new PrintDialog();

                    IDocumentPaginatorSource idpSource = doc;

                    if (printDlg.ShowDialog() == true)
                    {
                        printDlg.PrintDocument(idpSource.DocumentPaginator, "");
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала нужно выделить сообщение c диагнозом, которое вы хотите распечатать!");
            }
        }

        private void ItemsSourceChanged(object sender, EventArgs e)
        {
            int i = 0;

            foreach (var item in MessageGrid.ItemsSource)
            {
                var row = MessageGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;

                if (row != null)
                {
                    if (i == 0)
                    {
                        row.Background = Brushes.LightGreen;
                        i = 1;
                    }
                    else
                    {
                        row.Background = Brushes.White;
                        i = 0;
                    }
                }
            }
        }

        ///<summary>
        /// Событие смены категории сообщений (обновляет список сообщений исходя из выбранной категории)
        ///</summary>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshMessageGrid();
        }

        ///<summary>
        /// Обновление списка сообщений исходя из выбранной категории
        ///</summary>
        private void RefreshMessageGrid()
        {
            if ((DockPanel)this.CategoryListBox.SelectedItem == null)
                return;

            var core = new CoreFunc(UserID);
            var categoryName = ((DockPanel)this.CategoryListBox.SelectedItem).Name;
            var messages = new List<Message>();

            if (categoryName == "IncomingTab")
            {
                messages = core.GetIncomingMessages();
                MessageGridLabel.Content = "Входящие ответы:";
            }
            else if (categoryName == "OutgoingTab")
            {
                messages = core.GetOutgoingMessages();
                MessageGridLabel.Content = "Исходящие запросы:";
            }
            else if (categoryName == "SendingTab")
            {
                messages = core.GetSendingMessages();
                MessageGridLabel.Content = "Отправляются:";
            }
            else if (categoryName == "NeedAnswerTab")
            {
                messages = core.GetNeedAnswerMessages();
                MessageGridLabel.Content = "Нужен ответ:";
            }
            else if (categoryName == "ArchiveTab")
            {
                messages = core.GetArchiveMessages();
                MessageGridLabel.Content = "Архив сообщений:";
            }

            FillMessageGrid(messages);
            DialogGridLabel.Content = "";
            MessageGridLabel.Focusable = true;
            Keyboard.Focus(MessageGridLabel);
        }

        ///<summary>
        /// Заполнение таблицы сообщений
        /// Входной параметр messages - список сообщений текущей категории
        ///</summary>
        public void FillMessageGrid(List<Message> messages)
        {
            MessageGrid.ItemsSource = messages;
            DialogGrid.ItemsSource = null;

            ImageControl0.Source = null;
            ImageControl1.Source = null;
            ImageControl2.Source = null;
            ImageControl3.Source = null;
            ImageControl4.Source = null;
            ImageControl5.Source = null;
            ImageControl6.Source = null;
            ImageControl7.Source = null;
            ImageControl8.Source = null;
            ImageControl9.Source = null;

            PatientCard.Content = "";
            PatientSex.Content = "";
            PatientAge.Content = "";
            PatientWeight.Content = "";
            PatientVisitDate.Content = "";
            PatientUsedDrugs.Content = "";
            PatientRemissionPeriod.Content = "";
            PatientLastExacerbation.Content = "";
            PatientAppliedTherapy.Content = "";
            PatientSurveyResults.Content = "";
            PatientComplaints.Content = "";
            PatientInfo.Content = "";

            MessageGrid.Columns[0].Visibility = Visibility.Collapsed;
            MessageGrid.Columns[1].Visibility = Visibility.Collapsed;
            MessageGrid.Columns[3].Visibility = Visibility.Collapsed;
            MessageGrid.Columns[6].Visibility = Visibility.Collapsed;

            MessageGrid.Columns[2].Header = "№ карты пациента";
            MessageGrid.Columns[4].Header = "От кого";
            MessageGrid.Columns[5].Header = "Дата";

            MessageGrid.Columns[2].Width = 120;
            MessageGrid.Columns[4].Width = 180;
            MessageGrid.Columns[5].Width = 100;
        }

        ///<summary>
        /// Вызов формы со списком пациентов
        ///</summary>
        private void ShowPatients(object sender, RoutedEventArgs e)
        {
            PatientsWindow patientsWindow = new PatientsWindow(UserID, UserDocNumber);

            if (patientsWindow.ShowDialog() == true)
            {
            }
            else
            {
            }
        }

        ///<summary>
        /// Вызов формы со списком пользователей
        ///</summary>
        private void ShowUsers(object sender, RoutedEventArgs e)
        {
            MDBS_client.UsersWindow usersWindow = new MDBS_client.UsersWindow(UserID);

            if (usersWindow.ShowDialog() == true)
            {
            }
            else
            {
            }
        }

        ///<summary>
        /// Вызов формы создания нового запроса
        ///</summary>
        private void NewMessage(object sender, RoutedEventArgs e)
        {
            MessageWindow msgWindow = new MessageWindow(UserID, UserDocNumber);

            if (msgWindow.ShowDialog() == true)
            {
                RefreshMessageGrid();
            }
            else
            {
            }
        }

        ///<summary>
        /// Вызов формы создания нового пользователя
        ///</summary>
        private void CreateNewUser(object sender, RoutedEventArgs e)
        {
            MDBS_client.NewUserWindow msgWindow = new MDBS_client.NewUserWindow(UserID);

            if (msgWindow.ShowDialog() == true)
            {
            }
            else
            {
            }
        }

        ///<summary>
        /// Событие выбора сообщения
        /// В результате обновляется таблица с историей запросов по пациенту из выбранного сообщения,
        /// информация по этому пациенту и вложения к выбранному сообщению
        ///</summary>
        private void MessageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var core = new CoreFunc();
            var dialog = new List<Dialog>();

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var message = e.AddedItems[0] as Message;

                if (message == null)
                    return;

                var messageId = message.ID;

                dialog = core.GetDialog((Guid)messageId);

                DialogGrid.ItemsSource = dialog;

                DialogGrid.Columns[0].Visibility = Visibility.Collapsed;
                DialogGrid.Columns[2].Visibility = Visibility.Collapsed;
                DialogGrid.Columns[4].Visibility = Visibility.Collapsed;
                DialogGrid.Columns[5].Visibility = Visibility.Collapsed;
                DialogGrid.Columns[6].Visibility = Visibility.Collapsed;

                DialogGrid.Columns[1].Header = "Запрос";
                //DialogGrid.Columns[2].Header = "Диагноз";
                DialogGrid.Columns[3].Header = "План лечения";
                //DialogGrid.Columns[5].Header = "Пациент";
                DialogGrid.Columns[7].Header = "От кого";
                DialogGrid.Columns[8].Header = "Дата";

                DialogGrid.Columns[1].Width = 170;
                DialogGrid.Columns[2].Width = 170;
                DialogGrid.Columns[3].Width = 170;
                DialogGrid.Columns[4].Width = 90;
                DialogGrid.Columns[6].Width = 100;
                DialogGrid.Columns[7].Width = 180;

                DataGridTextColumn infoColumn = DialogGrid.Columns[1] as DataGridTextColumn;
                DataGridTextColumn diagnosisColumn = DialogGrid.Columns[2] as DataGridTextColumn;
                DataGridTextColumn therapyPlanColumn = DialogGrid.Columns[3] as DataGridTextColumn;

                Style style = DialogGrid.Resources["wordWrapStyle"] as Style;

                infoColumn.ElementStyle = style;
                infoColumn.EditingElementStyle = style;

                diagnosisColumn.ElementStyle = style;
                diagnosisColumn.EditingElementStyle = style;

                therapyPlanColumn.ElementStyle = style;
                therapyPlanColumn.EditingElementStyle = style;

                DialogGridLabel.Content = "История запросов и ответов по пациенту: " + message.PatientName;

                var patientId = message.PatientID;
                var patientInfo = core.GetPatientInfo((Guid)patientId);

                PatientCard.Content = patientInfo.MedicalCardNumber;
                PatientSex.Content = patientInfo.Sex;
                PatientAge.Content = patientInfo.Age;
                PatientWeight.Content = patientInfo.Weight;
                PatientVisitDate.Content = patientInfo.VisitDate;
                PatientUsedDrugs.Content = patientInfo.UsedDrugs;
                PatientRemissionPeriod.Content = patientInfo.RemissionPeriod;
                PatientLastExacerbation.Content = patientInfo.LastExacerbation;
                PatientAppliedTherapy.Content = patientInfo.AppliedTherapy;
                PatientSurveyResults.Content = patientInfo.SurveyResults;
                PatientComplaints.Content = patientInfo.Complaints;
                PatientInfo.Content = patientInfo.Info;

                if (message.From != UserID)
                    core.ReadMessage(messageId);

                if (MessageGrid.CurrentItem != null)
                {
                    var row = MessageGrid.ItemContainerGenerator.ContainerFromItem(MessageGrid.CurrentItem) as DataGridRow;
                    row.FontWeight = FontWeight.FromOpenTypeWeight(400);
                }

                AttachmentsUpdate(message.PatientName, (Guid)messageId);
            }
        }

        ///<summary>
        /// Событие обновления таблицы сообщений по текущей категории
        /// Для входящих проверяется стутус (жирным веделяются непрочитанные)
        /// Для сообщений, требующих ответа проверяется дата (в зависимости от даты сообщения окрашиваются по цветам)
        ///</summary>
        private void MessageGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var RowDataContaxt = e.Row.DataContext as Message;

            if (RowDataContaxt != null)
            {
                var categoryName = ((DockPanel)this.CategoryListBox.SelectedItem).Name;

                if (RowDataContaxt.Status == 0 && categoryName == "IncomingTab")
                {
                    e.Row.FontWeight = FontWeight.FromOpenTypeWeight(900);
                }

                if (categoryName == "NeedAnswerTab")
                {
                    var diff = (DateTime.Now - DateTime.Parse(RowDataContaxt.MessageDate)).TotalDays;

                    if (diff < 2)
                    {
                        e.Row.Background = Brushes.LightGreen;
                    }
                    else if (diff >= 2 && diff < 3)
                    {
                        e.Row.Background = Brushes.LightYellow;
                    }
                    else if (diff >= 3)
                    {
                        e.Row.Background = Brushes.Pink;
                    }
                }
            }
        }

        ///<summary>
        /// Событие обновления таблицы истории сообщений по пациенту
        /// Окрашивание входящих и исходящих сообщений в разные цыета для удобства
        ///</summary>
        private void DialogGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var RowDataContaxt = e.Row.DataContext as Dialog;

            if (RowDataContaxt != null)
            {
                if (RowDataContaxt.From == UserID)
                {
                    e.Row.Background = Brushes.LightYellow;
                }
                else
                {
                    e.Row.Background = Brushes.Linen;
                }
            }
        }

        ///<summary>
        /// Событие выбора сообщения из таблицы истории сообщений по пациенту
        /// Обновление блока с вложениями по выбранному сообщению
        ///</summary>
        private void DialogGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var dialog = e.AddedItems[0] as Dialog;

                if (dialog == null)
                    return;

                var messageId = dialog.ID;
                AttachmentsUpdate(dialog.PatientName, (Guid)messageId);
            }
        }

        ///<summary>
        /// Обновление блока с вложениями по выбранному сообщению
        /// Входной параметр messageId - Id выбранного сообщения
        ///</summary>
        public void AttachmentsUpdate(string patientNumber, Guid messageId)
        {
            Images = new BitmapImage[10];
            ImageComments = new string[10];
            ImageControl0.Source = null;
            ImageControl1.Source = null;
            ImageControl2.Source = null;
            ImageControl3.Source = null;
            ImageControl4.Source = null;
            ImageControl5.Source = null;
            ImageControl6.Source = null;
            ImageControl7.Source = null;
            ImageControl8.Source = null;
            ImageControl9.Source = null;

            if (Directory.Exists(@".\Data\" + patientNumber + @"\" + messageId.ToString()))
            {
                var imgs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"Data\" + patientNumber + @"\" + messageId.ToString());

                if (imgs.Length > 0)
                {
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(0)))
                    {
                        Images[0] = new BitmapImage(new Uri(imgs[0]));
                        ImageControl0.Source = Images[0];
                    }

                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(1)))
                    {
                        Images[1] = new BitmapImage(new Uri(imgs[1]));
                        ImageControl1.Source = Images[1];
                    }

                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(2)))
                    {
                        Images[2] = new BitmapImage(new Uri(imgs[2]));
                        ImageControl2.Source = Images[2];
                    }

                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(3)))
                    {
                        Images[3] = new BitmapImage(new Uri(imgs[3]));
                        ImageControl3.Source = Images[3];
                    }
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(4)))
                    {
                        Images[4] = new BitmapImage(new Uri(imgs[4]));
                        ImageControl4.Source = Images[4];
                    }
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(5)))
                    {
                        Images[5] = new BitmapImage(new Uri(imgs[5]));
                        ImageControl5.Source = Images[5];
                    }
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(6)))
                    {
                        Images[6] = new BitmapImage(new Uri(imgs[6]));
                        ImageControl6.Source = Images[6];
                    }
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(7)))
                    {
                        Images[7] = new BitmapImage(new Uri(imgs[7]));
                        ImageControl7.Source = Images[7];
                    }
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(8)))
                    {
                        Images[8] = new BitmapImage(new Uri(imgs[8]));
                        ImageControl8.Source = Images[8];
                    }
                    if (!string.IsNullOrEmpty(imgs.ElementAtOrDefault(9)))
                    {
                        Images[9] = new BitmapImage(new Uri(imgs[9]));
                        ImageControl9.Source = Images[9];
                    }
                }

                return;
            }

            var core = new CoreFunc();
            var images = core.GetAttachments(messageId);

            if (images.Count > 0)
            {
                if (images.ElementAtOrDefault(0) != null)
                {
                    Images[0] = ToImage(images[0].Data);
                    ImageControl0.Source = Images[0];
                    ImageComments[0] = images[0].Comment;
                }

                if (images.ElementAtOrDefault(1) != null)
                {
                    Images[1] = ToImage(images[1].Data);
                    ImageControl1.Source = Images[1];
                    ImageComments[1] = images[1].Comment;
                }

                if (images.ElementAtOrDefault(2) != null)
                {
                    Images[2] = ToImage(images[2].Data);
                    ImageControl2.Source = Images[2];
                    ImageComments[2] = images[2].Comment;
                }

                if (images.ElementAtOrDefault(3) != null)
                {
                    Images[3] = ToImage(images[3].Data);
                    ImageControl3.Source = Images[3];
                    ImageComments[3] = images[3].Comment;
                }
                if (images.ElementAtOrDefault(4) != null)
                {
                    Images[4] = ToImage(images[4].Data);
                    ImageControl4.Source = Images[4];
                    ImageComments[4] = images[4].Comment;
                }
                if (images.ElementAtOrDefault(5) != null)
                {
                    Images[5] = ToImage(images[5].Data);
                    ImageControl5.Source = Images[5];
                    ImageComments[5] = images[5].Comment;
                }
                if (images.ElementAtOrDefault(6) != null)
                {
                    Images[6] = ToImage(images[6].Data);
                    ImageControl6.Source = Images[6];
                    ImageComments[6] = images[6].Comment;
                }
                if (images.ElementAtOrDefault(7) != null)
                {
                    Images[7] = ToImage(images[7].Data);
                    ImageControl7.Source = Images[7];
                    ImageComments[7] = images[7].Comment;
                }
                if (images.ElementAtOrDefault(8) != null)
                {
                    Images[8] = ToImage(images[8].Data);
                    ImageControl8.Source = Images[8];
                    ImageComments[8] = images[8].Comment;
                }
                if (images.ElementAtOrDefault(9) != null)
                {
                    Images[9] = ToImage(images[9].Data);
                    ImageControl9.Source = Images[9];
                    ImageComments[9] = images[9].Comment;
                }
            }
        }

        // вынести в отдельный класс с тулзами
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        ///<summary>
        /// Открытие изображения в полном разрешении
        ///</summary>
        private void ImageControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                var source = e.Source as FrameworkElement;

                if (source != null)
                {
                    string elementName = source.Name;

                    ImageWindow imageWindow = new ImageWindow(Images, ImageComments, int.Parse(elementName.Substring(12)));

                    if (imageWindow.ShowDialog() == true)
                    {
                    }
                    else
                    {
                    }
                }
            }
        }

        ///<summary>
        /// Открытие формы авторизации, подгрузка логинов последних авторизованных пользователей
        ///</summary>
        private User Authorization()
        {
            ObservableCollection<string> logins = new ObservableCollection<string>();
            logins.Add(ConfigurationManager.AppSettings["login1"]);
            logins.Add(ConfigurationManager.AppSettings["login2"]);
            logins.Add(ConfigurationManager.AppSettings["login3"]);

            LoginWindow loginWindow = new LoginWindow(logins);

            if (loginWindow.ShowDialog() == true)
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["login3"].Value = ConfigurationManager.AppSettings["login2"];
                configFile.AppSettings.Settings["login2"].Value = ConfigurationManager.AppSettings["login1"];
                configFile.AppSettings.Settings["login1"].Value = LoginWindow.CurrentUser.DocNumber;
                configFile.Save();

                return LoginWindow.CurrentUser;
            }
            else
            {
                return new User();
            }
        }
    }
}