﻿using Core;
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

        byte[] Img0;
        byte[] Img1;
        byte[] Img2;
        byte[] Img3;
        byte[] Img4;
        byte[] Img5;
        byte[] Img6;
        byte[] Img7;
        byte[] Img8;
        byte[] Img9;

        string[] ImgsPath = new string[10];
        string[] ImgsDataPath = new string[10];

        PatientsWindow PatientsWindow;

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

                    if (Img0 == null)
                    {
                        Image0.Source = img;
                        ImgsPath[0] = filename;
                        Img0 = File.ReadAllBytes(filename);
                    }
                    else if (Img1 == null)
                    {
                        Image1.Source = img;
                        ImgsPath[1] = filename;
                        Img1 = File.ReadAllBytes(filename);
                    }
                    else if (Img2 == null)
                    {
                        Image2.Source = img;
                        ImgsPath[2] = filename;
                        Img2 = File.ReadAllBytes(filename);
                    }
                    else if (Img3 == null)
                    {
                        Image3.Source = img;
                        ImgsPath[3] = filename;
                        Img3 = File.ReadAllBytes(filename);
                    }
                    else if (Img4 == null)
                    {
                        Image4.Source = img;
                        ImgsPath[4] = filename;
                        Img4 = File.ReadAllBytes(filename);
                    }
                    else if (Img5 == null)
                    {
                        Image5.Source = img;
                        ImgsPath[5] = filename;
                        Img5 = File.ReadAllBytes(filename);
                    }
                    else if (Img6 == null)
                    {
                        Image6.Source = img;
                        ImgsPath[6] = filename;
                        Img6 = File.ReadAllBytes(filename);
                    }
                    else if (Img7 == null)
                    {
                        Image7.Source = img;
                        ImgsPath[7] = filename;
                        Img7 = File.ReadAllBytes(filename);
                    }
                    else if (Img8 == null)
                    {
                        Image8.Source = img;
                        ImgsPath[8] = filename;
                        Img8 = File.ReadAllBytes(filename);
                    }
                    else if (Img9 == null)
                    {
                        Image9.Source = img;
                        ImgsPath[9] = filename;
                        Img9 = File.ReadAllBytes(filename);
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
            PatientsWindow = new PatientsWindow(UserID);

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
                if (ImgsPath[i] != null)
                {
                    File.Copy(ImgsPath[i], @".\Data\" +
                        this.Patient +
                        @"\" + msgFolder +
                        @"\" +
                        i +
                        ImgsPath[i].Substring(ImgsPath[i].IndexOf(".")));

                    ImgsDataPath[i] = @".\Data\" + this.Patient + @"\" + msgFolder +
                        @"\" + i + ImgsPath[i].Substring(ImgsPath[i].IndexOf("."));
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
                ImgsDataPath);
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