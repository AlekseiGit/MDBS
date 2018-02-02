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

        string ImagePath0 = "";
        string ImagePath1 = "";
        string ImagePath2 = "";
        string ImagePath3 = "";
        string ImagePath4 = "";
        string ImagePath5 = "";
        string ImagePath6 = "";
        string ImagePath7 = "";
        string ImagePath8 = "";
        string ImagePath9 = "";

        public MessageWindow(Guid userId)
        {
            InitializeComponent();
            UserID = userId;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
                    BitmapImage img = new BitmapImage(new Uri(filename));

                    if (string.IsNullOrEmpty(ImagePath0))
                    {
                        Image0.Source = img;
                        ImagePath0 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath1))
                    {
                        Image1.Source = img;
                        ImagePath1 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath2))
                    {
                        Image2.Source = img;
                        ImagePath2 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath3))
                    {
                        Image3.Source = img;
                        ImagePath3 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath4))
                    {
                        Image4.Source = img;
                        ImagePath4 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath5))
                    {
                        Image5.Source = img;
                        ImagePath5 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath6))
                    {
                        Image6.Source = img;
                        ImagePath6 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath7))
                    {
                        Image7.Source = img;
                        ImagePath7 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath8))
                    {
                        Image8.Source = img;
                        ImagePath8 = filename;
                    }
                    else if (string.IsNullOrEmpty(ImagePath9))
                    {
                        Image9.Source = img;
                        ImagePath9 = filename;
                    }

                    img.StreamSource.Dispose();
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
                ImagePath0,
                ImagePath1,
                ImagePath2,
                ImagePath3,
                ImagePath4,
                ImagePath5,
                ImagePath6,
                ImagePath7,
                ImagePath8,
                ImagePath9);

            MessageBox.Show("Сообщение отправляется!");

            this.DialogResult = true;
        }
    }
}