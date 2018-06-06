using Core;
using DataModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        string UserDocNumber;

        //byte[][] ImgsData = new byte[10][];
        //string[] ImgsPath = new string[10];
        //string[] ImgsDataPath = new string[10];

        MessageContainer[] MC = new MessageContainer[10];

        PatientsWindow PatientsWindow;

        public MessageWindow(Guid userId, string userDocNumber)
        {
            InitializeComponent();
            UserID = userId;
            UserDocNumber = userDocNumber;
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

        /*
        public string Diagnosis
        {
            get { return DiagnosisBox.Text; }
        }
        */

        ///<summary>
        /// Прикрепление вложений (изображений)
        ///</summary>
        private void AttachImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            //openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.FileNames.Count() > 10)
                {
                    MessageBox.Show("К сообщению можно прикрепить только 10 изображений! Будут прикреплены только первые 10 изображений!");
                }

                int i = 0;

                foreach (string filename in openFileDialog.FileNames)
                {
                    BitmapImage img = new BitmapImage();

                    try
                    {
                        img = new BitmapImage(new Uri(filename));
                    }
                    catch
                    {
                        return;
                    }

                    /*
                    while (MC[i] == null)
                    {
                        var mc = new MessageContainer();

                        var pathText = (TextBox)this.FindName("PathBox" + i.ToString());
                        pathText.Text = filename;

                        var attachedImage = (Image)this.FindName("Image" + i.ToString());
                        attachedImage.Source = img;

                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[i] = mc;

                        i += 1;
                    }
                    */

                    if (MC[0] == null)
                    {
                        var mc = new MessageContainer();

                        Image0.Source = img;
                        PathBox0.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[0] = mc;
                    }
                    else if (MC[1] == null)
                    {
                        var mc = new MessageContainer();

                        Image1.Source = img;
                        PathBox1.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[1] = mc;
                    }
                    else if (MC[2] == null)
                    {
                        var mc = new MessageContainer();

                        Image2.Source = img;
                        PathBox2.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[2] = mc;
                    }
                    else if (MC[3] == null)
                    {
                        var mc = new MessageContainer();

                        Image3.Source = img;
                        PathBox3.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[3] = mc;
                    }
                    else if (MC[4] == null)
                    {
                        var mc = new MessageContainer();

                        Image4.Source = img;
                        PathBox4.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[4] = mc;
                    }
                    else if (MC[5] == null)
                    {
                        var mc = new MessageContainer();

                        Image5.Source = img;
                        PathBox5.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[5] = mc;
                    }
                    else if (MC[6] == null)
                    {
                        var mc = new MessageContainer();

                        Image6.Source = img;
                        PathBox6.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[6] = mc;
                    }
                    else if (MC[7] == null)
                    {
                        var mc = new MessageContainer();

                        Image7.Source = img;
                        PathBox7.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[7] = mc;
                    }
                    else if (MC[8] == null)
                    {
                        var mc = new MessageContainer();

                        Image8.Source = img;
                        PathBox8.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[8] = mc;
                    }
                    else if (MC[9] == null)
                    {
                        var mc = new MessageContainer();

                        Image9.Source = img;
                        PathBox9.Text = filename;
                        mc.ImgsPath = filename;
                        mc.ImgsData = File.ReadAllBytes(filename);
                        mc.ImgsDataPath = "";

                        MC[9] = mc;
                    }
                }
            }
        }

        ///<summary>
        /// Конвертер изобрадения в массив байтов
        ///</summary>
        public byte[] BitmapImageToByte(BitmapImage bitmapImage)
        {
            Stream stream = bitmapImage.StreamSource;
            byte[] buffer = null;

            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }

        ///<summary>
        /// Открытие окна с пациентами при выборе пациента, по которому делается запрос
        ///</summary>
        private void ChoosePatient_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow = new PatientsWindow(UserID, UserDocNumber);

            PatientsWindow.PatientGrid.MouseDoubleClick += PatientChoose;

            if (PatientsWindow.ShowDialog() == true)
            {
            }
            else
            {
            }
        }

        ///<summary>
        /// Обновление поля с картой пациента по выбранному пациенту
        ///</summary>
        private void PatientChoose(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var row = sender as DataGrid;
                
                if (row != null && row.SelectedItems.Count == 1)
                {
                    var patient = row.SelectedItems[0] as Patient;
                    PatientBox.Text = patient.MedicalCardNumber;
                }

                PatientsWindow.Close();
            }
        }

        private void RemoveImage_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var num = btn.Name.Substring(9);

            var pathText = (TextBox)this.FindName("PathBox" + num.ToString());
            pathText.Text = "";

            var attachedImage = (Image)this.FindName("Image" + num.ToString());
            attachedImage.Source = null;

            MC[int.Parse(num)] = null;
        }

        ///<summary>
        /// Отправка сообщения/запроса в центр
        ///</summary>
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
            /*
            else if (string.IsNullOrEmpty(this.Diagnosis))
            {
                MessageBox.Show("Диагноз пациента не заполнен!");
                return;
            }
            */

            Guid messageId = Guid.NewGuid();
            string msgFolder = messageId.ToString();

            Directory.CreateDirectory(@".\Data\" + this.Patient + @"\" + msgFolder);

            for (int i=0; i<10; i++)
            {
                if (MC[i] != null)
                {
                    File.Copy(MC[i].ImgsPath, @".\Data\" +
                        this.Patient +
                        @"\" + msgFolder +
                        @"\" +
                        i +
                        MC[i].ImgsPath.Substring(MC[i].ImgsPath.IndexOf(".")));

                    MC[i].ImgsDataPath = @".\Data\" + this.Patient + @"\" + msgFolder +
                        @"\" + i + MC[i].ImgsPath.Substring(MC[i].ImgsPath.IndexOf("."));
                }
            }

            var core = new CoreFunc();

            core.SendMessage(
                messageId,
                this.Info,
                "", //this.Diagnosis,
                this.Patient,
                UserID,
                new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"), // Id главного пользователя (центра) todo
                MC);
                //Img0,
                //Img1,
                //Img2,
                //Img3,
                //Img4,
                //Img5,
                //Img6,
                //Img7,
                //Img8,
                //Img9);

            MessageBox.Show("Сообщение отправляется!");

            this.DialogResult = true;
        }
    }
}