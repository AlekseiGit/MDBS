﻿using Core;
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
        public MessageWindow()
        {
            InitializeComponent();
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

        public string Img_one
        {
            get { return Image1.Source.ToString(); }
        }

        public string Img_two
        {
            get { return Image2.Source.ToString(); }
        }

        public string Img_three
        {
            get { return Image3.Source.ToString(); }
        }

        public string Img_four
        {
            get { return Image4.Source.ToString(); }
        }

        public string Img_five
        {
            get { return Image5.Source.ToString(); }
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
                    if (ImagePath1.Content.ToString() == "...")
                        ImagePath1.Content = filename;
                    else if (ImagePath2.Content.ToString() == "...")
                        ImagePath2.Content = filename;
                    else if (ImagePath3.Content.ToString() == "...")
                        ImagePath3.Content = filename;
                    else if (ImagePath4.Content.ToString() == "...")
                        ImagePath4.Content = filename;
                    else if (ImagePath5.Content.ToString() == "...")
                        ImagePath5.Content = filename;

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
            var core = new CoreFunc();
            core.SendMessage(
                this.Info,
                this.Diagnosis,
                this.Patient,
                new Guid("1A239C9B-E404-4AF3-A7BD-8D1C4925781D"), //user_id
                new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"), //to_id
                this.ImagePath1.Content.ToString(),
                this.ImagePath2.Content.ToString(),
                this.ImagePath3.Content.ToString(),
                this.ImagePath4.Content.ToString(),
                this.ImagePath5.Content.ToString());

            this.DialogResult = true;
        }
    }
}