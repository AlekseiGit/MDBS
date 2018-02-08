﻿using System;
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
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        BitmapImage[] Images = new BitmapImage[10];
        int CurrentImage;

        public ImageWindow(BitmapImage[] images, int currentImage)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            if (images[currentImage] != null)
            {
                FullImage.Source = images[currentImage];
                Images = images;
                CurrentImage = currentImage;
            }
        }

        private void PrevImage_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentImage - 1 >=0 && CurrentImage - 1 <= 9 && Images[CurrentImage - 1] != null)
            {
                FullImage.Source = Images[CurrentImage - 1];
                CurrentImage = CurrentImage - 1;
            }
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentImage + 1 >= 0 && CurrentImage + 1 <= 9 && Images[CurrentImage + 1] != null)
            {
                FullImage.Source = Images[CurrentImage + 1];
                CurrentImage = CurrentImage + 1;
            }
        }
    }
}