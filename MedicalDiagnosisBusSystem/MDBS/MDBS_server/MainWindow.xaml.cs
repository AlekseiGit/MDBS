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

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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
            var core = new CoreFunc();
            var categoryName = ((ListBoxItem)this.CategoryListBox.SelectedItem).Content.ToString();
            var messages = new List<Message>();

            switch (categoryName)
            {
                case "Входящие":
                    {
                        messages = core.GetIncomingMessages();
                        break;
                    }
                case "Исходящие":
                    {
                        messages = core.GetOutgoingMessages();
                        break;
                    }
                case "Требуется ответ":
                    {
                        messages = core.GetNeedAnswerMessages();
                        break;
                    }
                case "Архив":
                    {
                        messages = core.GetArchiveMessages();
                        break;
                    }
            }

            FillMessageGrid(messages);
        }

        public void FillMessageGrid(List<Message> messages)
        {
            MessageGrid.ItemsSource = messages;

            MessageGrid.Columns[0].Visibility = Visibility.Collapsed;
            MessageGrid.Columns[4].Visibility = Visibility.Collapsed;

            MessageGrid.Columns[1].Header = "Пациент";
            MessageGrid.Columns[2].Header = "От кого";
            MessageGrid.Columns[3].Header = "Дата";

            MessageGrid.Columns[1].Width = 120;
            MessageGrid.Columns[2].Width = 120;
            MessageGrid.Columns[3].Width = 120;
        }

        private void NewMessage(object sender, RoutedEventArgs e)
        {
            MessageWindow msgWindow = new MessageWindow();

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

        private void MessageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var core = new CoreFunc();
            var dialog = new List<Dialog>();

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var messageId = (e.AddedItems[0] as Message).ID;

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
            }
        }

        private void MessageGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var RowDataContaxt = e.Row.DataContext as Message;

            if (RowDataContaxt != null)
            {
                if (RowDataContaxt.Status == 0)
                {
                    //e.Row.Background = Brushes.LightGreen;
                    e.Row.FontWeight = FontWeight.FromOpenTypeWeight(900);
                }
                //else if (RowDataContaxt.Status == 1)
                //{
                //    e.Row.Background = Brushes.White;
                //}
            }
        }

        private void DialogGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var RowDataContaxt = e.Row.DataContext as Dialog;

            if (RowDataContaxt != null)
            {
                if (RowDataContaxt.From == new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"))
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

            ImageControl1.Source = null;
            ImageControl2.Source = null;
            ImageControl3.Source = null;
            ImageControl4.Source = null;
            ImageControl5.Source = null;

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var messageId = (e.AddedItems[0] as Dialog).ID;
                var patientId = (e.AddedItems[0] as Dialog).PatientID;

                var patientInfo = core.GetPatientInfo((Guid)patientId);
                var images = core.GetAttachments((Guid)messageId);

                PatientCard.Content = patientInfo.MedicalCardNumber;
                if (patientInfo.Sex == 1)
                    PatientSex.Content = "М";
                else
                    PatientSex.Content = "Ж";
                PatientAge.Content = patientInfo.BirthDate;
                PatientInfo.Content = patientInfo.Info;

                if (images.Count > 0)
                {
                    if (images.ElementAtOrDefault(0) != null)
                    {
                        BitmapImage img = ToImage(images[0].Data);
                        ImageControl1.Source = img;
                    }

                    if (images.ElementAtOrDefault(1) != null)
                    {
                        BitmapImage img = ToImage(images[1].Data);
                        ImageControl2.Source = img;
                    }

                    if (images.ElementAtOrDefault(2) != null)
                    {
                        BitmapImage img = ToImage(images[2].Data);
                        ImageControl3.Source = img;
                    }

                    if (images.ElementAtOrDefault(3) != null)
                    {
                        BitmapImage img = ToImage(images[3].Data);
                        ImageControl4.Source = img;
                    }
                    if (images.ElementAtOrDefault(4) != null)
                    {
                        BitmapImage img = ToImage(images[4].Data);
                        ImageControl5.Source = img;
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
    }
}