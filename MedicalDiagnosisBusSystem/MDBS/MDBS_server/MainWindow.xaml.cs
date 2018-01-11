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

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Guid UserID;
        CoreFunc Core;

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

            var user = Authorization();

            if (user != Guid.Empty)
            {
                UserID = user;
            }
            else
            {
                Exit(null, null);
            }

            //var core = new CoreFunc();
            //var messages = core.GetIncomingMessages();
            //FillMessageGrid(messages);

            //CategoryListBox.SelectedItem = CategoryListBox.Items.GetItemAt(0);

            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(MessageGrid, ItemsSourceChanged);
            }

            DialogGrid.RowHeight = 70;

            Core = new CoreFunc(UserID);

            RefreshInformation(null, null);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(RefreshInformation);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        public void RefreshInformation(object sender, EventArgs e)
        {
            var systemData = Core.GetSystemData(UserID);

            Incoming.Content = "Входящие (" + systemData.IncomingInfo + ")";
            Outgoing.Content = "Исходящие (" + systemData.OutgoingInfo + ")";
            NeedAnswer.Content = "Нужен ответ (" + systemData.NeedAnswerInfo + ")";

            if (systemData.NeedAnswerStatus == 1)
            {
                NeedAnswer.Background = Brushes.LightGreen;
            }
            else if (systemData.NeedAnswerStatus == 2)
            {
                NeedAnswer.Background = Brushes.LightYellow;
            }
            if (systemData.NeedAnswerStatus == 3)
            {
                NeedAnswer.Background = Brushes.Pink;
            }
        }

        public void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var core = new CoreFunc(UserID);
            var categoryName = ((ListBoxItem)this.CategoryListBox.SelectedItem).Content.ToString();
            var messages = new List<Message>();

            if (categoryName.Contains("Входящие"))
            {
                messages = core.GetIncomingMessages();
            }
            else if (categoryName.Contains("Исходящие"))
            {
                messages = core.GetOutgoingMessages();
            }
            else if (categoryName.Contains("Нужен ответ"))
            {
                messages = core.GetNeedAnswerMessages();
            }
            else if (categoryName.Contains("Архив"))
            {
                messages = core.GetArchiveMessages();
            }

            FillMessageGrid(messages);
        }

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
            PatientWeight.Content = "";
            PatientAge.Content = "";
            PatientCurrentTherapy.Content = "";
            PatientInfo.Content = "";

            MessageGrid.Columns[0].Visibility = Visibility.Collapsed;
            MessageGrid.Columns[4].Visibility = Visibility.Collapsed;

            MessageGrid.Columns[1].Header = "Пациент";
            MessageGrid.Columns[2].Header = "От кого";
            MessageGrid.Columns[3].Header = "Дата";

            MessageGrid.Columns[1].Width = 120;
            MessageGrid.Columns[2].Width = 120;
            MessageGrid.Columns[3].Width = 120;
        }

        private void ShowPatients(object sender, RoutedEventArgs e)
        {
            PatientsWindow patientsWindow = new PatientsWindow();

            if (patientsWindow.ShowDialog() == true)
            {
            }
            else
            {
            }
        }

        private void NewPatient(object sender, RoutedEventArgs e)
        {
            NewPatientWindow newPatientWindow = new NewPatientWindow();

            if (newPatientWindow.ShowDialog() == true)
            {
                //MessageBox.Show("Авторизация пройдена");
            }
            else
            {
            }
        }

        private void NewMessage(object sender, RoutedEventArgs e)
        {
            MessageWindow msgWindow = new MessageWindow(UserID);

            if (msgWindow.ShowDialog() == true)
            {
                //MessageBox.Show("Авторизация пройдена");
            }
            else
            {
            }
        }

        private void AnswerMessage(object sender, RoutedEventArgs e)
        {
            if (DialogGrid.SelectedItems.Count == 1)
            {
                AnswerWindow answerWindow = new AnswerWindow();
                var dialogRow = DialogGrid.SelectedItems[0] as Dialog;

                if (dialogRow.From == new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"))
                    return;

                answerWindow.ParentMessageId = dialogRow.ID;
                answerWindow.PatientId = dialogRow.PatientID;
                answerWindow.PatientName = dialogRow.PatientName;
                answerWindow.FromId = new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D");
                answerWindow.FromName = dialogRow.FromName;
                answerWindow.ToId = dialogRow.From;

                if (answerWindow.ShowDialog() == true)
                {
                }
                else
                {
                }
            }
        }

        private void ShowFullSizeImage(object sender, RoutedEventArgs e)
        {
            if (DialogGrid.SelectedItems.Count == 1)
            {
                AnswerWindow answerWindow = new AnswerWindow();
                var dialogRow = DialogGrid.SelectedItems[0] as Dialog;

                if (dialogRow.From == new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"))
                    return;

                answerWindow.ParentMessageId = dialogRow.ID;
                answerWindow.PatientId = dialogRow.PatientID;
                answerWindow.PatientName = dialogRow.PatientName;
                answerWindow.FromId = new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D");
                answerWindow.FromName = dialogRow.FromName;
                answerWindow.ToId = dialogRow.From;

                if (answerWindow.ShowDialog() == true)
                {
                }
                else
                {
                }
            }
        }

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
                DialogGrid.Columns[4].Visibility = Visibility.Collapsed;
                DialogGrid.Columns[6].Visibility = Visibility.Collapsed;

                DialogGrid.Columns[1].Header = "Сообщение";
                DialogGrid.Columns[2].Header = "Диагноз";
                DialogGrid.Columns[3].Header = "План Лечения";
                DialogGrid.Columns[5].Header = "Пациент";
                DialogGrid.Columns[7].Header = "От кого";
                DialogGrid.Columns[8].Header = "Дата";

                DialogGrid.Columns[1].Width = 90;
                DialogGrid.Columns[2].Width = 170;
                DialogGrid.Columns[3].Width = 170;
                DialogGrid.Columns[4].Width = 90;
                DialogGrid.Columns[6].Width = 100;
                DialogGrid.Columns[7].Width = 110;

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

                core.ReadMessage(messageId);
                if (MessageGrid.CurrentItem != null)
                {
                    var row = MessageGrid.ItemContainerGenerator.ContainerFromItem(MessageGrid.CurrentItem) as DataGridRow;
                    row.FontWeight = FontWeight.FromOpenTypeWeight(400);
                }
            }
        }

        private void MessageGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var RowDataContaxt = e.Row.DataContext as Message;

            if (RowDataContaxt != null)
            {
                if (RowDataContaxt.Status == 0)
                {
                    e.Row.FontWeight = FontWeight.FromOpenTypeWeight(900);
                }

                if (CategoryListBox.SelectedItem.ToString().Contains("Нужен ответ"))
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

        private void DialogGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var core = new CoreFunc();

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

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var dialog = e.AddedItems[0] as Dialog;

                if (dialog == null)
                    return;

                var messageId = dialog.ID;
                var patientId = dialog.PatientID;

                var patientInfo = core.GetPatientInfo((Guid)patientId);
                var images = core.GetAttachments((Guid)messageId);

                PatientCard.Content = patientInfo.MedicalCardNumber;
                PatientSex.Content = patientInfo.Sex;
                PatientWeight.Content = patientInfo.Weight;
                PatientAge.Content = patientInfo.BirthDate;
                PatientCurrentTherapy.Content = patientInfo.CurrentTherapy;
                PatientInfo.Content = patientInfo.Info;

                if (images.Count > 0)
                {
                    if (images.ElementAtOrDefault(0) != null)
                    {
                        Image_0 = ToImage(images[0].Data);
                        ImageControl0.Source = Image_0;
                    }

                    if (images.ElementAtOrDefault(1) != null)
                    {
                        Image_1 = ToImage(images[1].Data);
                        ImageControl1.Source = Image_1;
                    }

                    if (images.ElementAtOrDefault(2) != null)
                    {
                        Image_2 = ToImage(images[2].Data);
                        ImageControl2.Source = Image_2;
                    }

                    if (images.ElementAtOrDefault(3) != null)
                    {
                        Image_3 = ToImage(images[3].Data);
                        ImageControl3.Source = Image_3;
                    }
                    if (images.ElementAtOrDefault(4) != null)
                    {
                        Image_4 = ToImage(images[4].Data);
                        ImageControl4.Source = Image_4;
                    }
                    if (images.ElementAtOrDefault(5) != null)
                    {
                        Image_5 = ToImage(images[5].Data);
                        ImageControl5.Source = Image_5;
                    }
                    if (images.ElementAtOrDefault(6) != null)
                    {
                        Image_6 = ToImage(images[6].Data);
                        ImageControl6.Source = Image_6;
                    }
                    if (images.ElementAtOrDefault(7) != null)
                    {
                        Image_7 = ToImage(images[7].Data);
                        ImageControl7.Source = Image_7;
                    }
                    if (images.ElementAtOrDefault(8) != null)
                    {
                        Image_8 = ToImage(images[8].Data);
                        ImageControl8.Source = Image_8;
                    }
                    if (images.ElementAtOrDefault(9) != null)
                    {
                        Image_9 = ToImage(images[9].Data);
                        ImageControl9.Source = Image_9;
                    }
                }
            }
        }

        // вынести в отдельный класс с тулзами
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void ImageControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                var source = e.Source as FrameworkElement;

                if (source != null)
                {
                    string elementName = source.Name;

                    ImageWindow imageWindow = new ImageWindow();

                    FieldInfo field = typeof(MainWindow).GetField("Image_" + elementName.Substring(12));
                    var image = (BitmapImage)field.GetValue(null);
                    imageWindow.FullImage.Source = image;
                    imageWindow.WindowState = WindowState.Maximized;

                    if (imageWindow.ShowDialog() == true)
                    {
                    }
                    else
                    {
                    }
                }
            }
        }

        private Guid Authorization()
        {
            LoginWindow loginWindow = new LoginWindow();

            if (loginWindow.ShowDialog() == true)
            {
                return LoginWindow.UserID;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}