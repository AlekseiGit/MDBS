using Core;
using Microsoft.Win32;
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

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        Guid UserID;

        public MessageWindow(Guid userId)
        {
            InitializeComponent();
            UserID = userId;
        }

        public Guid To
        {
            get { return (Guid)ToBox.SelectedValue; }
        }

        public string Patient
        {
            get { return PatientBox.Text; }
        }

        public string Info
        {
            get { return InfoBox.Text; }
        }

        public string Diagnosis
        {
            get { return DiagnosisBox.Text; }
        }

        public string Img_0
        {
            get { return Image0.Source.ToString(); }
        }

        public string Img_1
        {
            get { return Image1.Source.ToString(); }
        }

        public string Img_2
        {
            get { return Image2.Source.ToString(); }
        }

        public string Img_3
        {
            get { return Image3.Source.ToString(); }
        }

        public string Img_4
        {
            get { return Image4.Source.ToString(); }
        }

        public string Img_5
        {
            get { return Image5.Source.ToString(); }
        }

        public string Img_6
        {
            get { return Image6.Source.ToString(); }
        }

        public string Img_7
        {
            get { return Image7.Source.ToString(); }
        }

        public string Img_8
        {
            get { return Image8.Source.ToString(); }
        }

        public string Img_9
        {
            get { return Image9.Source.ToString(); }
        }

        private void AttachImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            //openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    if (ImagePath0.Content.ToString() == "...")
                        ImagePath0.Content = filename;
                    else if (ImagePath1.Content.ToString() == "...")
                        ImagePath1.Content = filename;
                    else if (ImagePath2.Content.ToString() == "...")
                        ImagePath2.Content = filename;
                    else if (ImagePath3.Content.ToString() == "...")
                        ImagePath3.Content = filename;
                    else if (ImagePath4.Content.ToString() == "...")
                        ImagePath4.Content = filename;
                    else if (ImagePath5.Content.ToString() == "...")
                        ImagePath5.Content = filename;
                    else if (ImagePath6.Content.ToString() == "...")
                        ImagePath6.Content = filename;
                    else if (ImagePath7.Content.ToString() == "...")
                        ImagePath7.Content = filename;
                    else if (ImagePath8.Content.ToString() == "...")
                        ImagePath8.Content = filename;
                    else if (ImagePath9.Content.ToString() == "...")
                        ImagePath9.Content = filename;

                    /*
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"C:\DBI\j.jpg", UriKind.Relative);
                    bi.EndInit();

                    if (Image1.Source == null)
                        Image1.Source = bi;
                    else if (Image2.Source == null)
                        Image2.Source = bi;
                    else if (Image3.Source == null)
                        Image3.Source = bi;
                    else if (Image4.Source == null)
                        Image4.Source = bi;
                    else if (Image5.Source == null)
                        Image5.Source = bi;
                    */
                }
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Patient))
            {
                MessageBox.Show("Номер карты пациента не заполнен!");
                return;
            }
            else if (string.IsNullOrEmpty(this.Info))
            {
                MessageBox.Show("Сообщение не заполнено!");
                return;
            }
            else if (string.IsNullOrEmpty(this.Diagnosis))
            {
                MessageBox.Show("Диагноз пациента не заполнен!");
                return;
            }

            var core = new CoreFunc();

            core.SendMessage(
                this.Info,
                this.Diagnosis,
                this.Patient,
                UserID,
                new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"), //to_id
                this.ImagePath0.Content.ToString(),
                this.ImagePath1.Content.ToString(),
                this.ImagePath2.Content.ToString(),
                this.ImagePath3.Content.ToString(),
                this.ImagePath4.Content.ToString(),
                this.ImagePath5.Content.ToString(),
                this.ImagePath6.Content.ToString(),
                this.ImagePath7.Content.ToString(),
                this.ImagePath8.Content.ToString(),
                this.ImagePath9.Content.ToString());

            MessageBox.Show("Сообщение отправляется!");

            this.DialogResult = true;
        }
    }
}