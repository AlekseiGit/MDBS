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
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        BitmapImage[] Images = new BitmapImage[10];
        string[] ImageComments = new string[10];
        int CurrentImage;

        public ImageWindow(BitmapImage[] images, string[] imageComments, int currentImage)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            if (images[currentImage] != null)
            {
                FullImage.Source = images[currentImage];
                Images = images;
                ImageComments = imageComments;
                CurrentImage = currentImage;
                CountBox.Text = (CurrentImage + 1).ToString() + " из " + Images.Where(i => i != null).Count().ToString();
                CommentBox.Content = imageComments[currentImage];
            }
        }

        ///<summary>
        /// Открытие предыдущего (от текущего) изображения в большом разрешении
        ///</summary>
        private void PrevImage_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentImage - 1 >=0 && CurrentImage - 1 <= 9 && Images[CurrentImage - 1] != null)
            {
                FullImage.Source = Images[CurrentImage - 1];
                CommentBox.Content = ImageComments[CurrentImage - 1];
                CurrentImage = CurrentImage - 1;
                CountBox.Text = (CurrentImage + 1).ToString() + " из " + Images.Where(i => i != null).Count().ToString();
            }
        }

        ///<summary>
        /// Открытие следующего (от текущего) изображения в большом разрешении
        ///</summary>
        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentImage + 1 >= 0 && CurrentImage + 1 <= 9 && Images[CurrentImage + 1] != null)
            {
                FullImage.Source = Images[CurrentImage + 1];
                CommentBox.Content = ImageComments[CurrentImage + 1];
                CurrentImage = CurrentImage + 1;
                CountBox.Text = (CurrentImage + 1).ToString() + " из " + Images.Where(i => i != null).Count().ToString();
            }
        }
    }
}