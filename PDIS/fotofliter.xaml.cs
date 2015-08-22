using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace PDIS
{
    public partial class fotofliter : PhoneApplicationPage
    {
        public fotofliter()
        {
            InitializeComponent();
            
        }
        void photoChooser_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                // Load the picture in a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(e.ChosenPhoto);
                // Place the loaded image in a WriteableBitmap
                WriteableBitmap writeableBitmap = new WriteableBitmap(bitmap);
                // Execute the filter on the pixels of the bitmap
                ApplyFilter(writeableBitmap.Pixels);
                // Set the source of our image to the WriteableBitmap
                myImage.Source = writeableBitmap;
            }
        }

        public static void ApplyFilter(int[] target)
        {
            // for each pixel in the array
            for (int i = 0; i < target.Length; i++)
            {
                // First two characters are for Alpha
                byte a = (byte)(target[i] >> 24);
                // Second two characters are for Red
                byte r = (byte)((target[i] & 0x00ff0000) >> 16);
                // Third two characters are for Green
                byte g = (byte)((target[i] & 0x0000ff00) >> 8);
                // Last two characters are for Blue
                byte b = (byte)(target[i] & 0x000000ff);

                // Subtract the colors from white(255 or 0xFF)
                r = (byte)(0xFF - r);
                g = (byte)(0xFF - g);
                b = (byte)(0xFF - b);

                // Place back A, R, G and B
                target[i] = (int)(a << 24 | r << 16 | g << 8 | b);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new PhotoChooser task
            PhotoChooserTask photoChooser = new PhotoChooserTask();
            // Add an event handler for when it has completed
            photoChooser.Completed += photoChooser_Completed;
            // Show our PhotoChooserTask
            photoChooser.Show();
        }
    }
}