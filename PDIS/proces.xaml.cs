using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Windows.Media.Imaging;
using ExifLib;

namespace PDIS
{
    public partial class proces : PhoneApplicationPage
    {
        //for camera
        CameraCaptureTask cameraTask = null;
        PhotoChooserTask photoChooserTask = null;
        //for timer
        DispatcherTimer countDownTimer;
        int[] source = new int[800 * 450];
        int[] pixels = new int[800 * 270];
        int[] pixelsRed = new int[800 * 270];
        int[] pixelsGreen = new int[800 * 270];
        int[] pixelsBlue = new int[800 * 270];
        int[] pixelCount = new int[3] { 0, 0, 0 };
        int seb;

        public proces()
        {
            InitializeComponent();

            cameraTask = new CameraCaptureTask();
            cameraTask.Completed += new EventHandler<PhotoResult>(cameraTask_Completed);

            photoChooserTask = new PhotoChooserTask();
          

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(50);
            dt.Tick += delegate { try { FrameworkDispatcher.Update(); } catch { } };
            dt.Start();
            seki.IsEnabled = false;

        }
    
        public void ReadExif(JpegInfo info)
        {
            this.seb = info.Width;

        }

        void cameraTask_Completed(object sender, PhotoResult e)
        {
            
            if (e.TaskResult == TaskResult.OK)
            {
                JpegInfo info = ExifLib.ExifReader.ReadJpeg(e.ChosenPhoto, e.OriginalFileName);
                // Load the picture in a BitmapImage
                System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.SetSource(e.ChosenPhoto);
                // Place the loaded image in a WriteableBitmap
                WriteableBitmap writeableBitmap = new WriteableBitmap(bitmap);
                // Execute the filter on the pixels of the bitmap
                ApplyFilter(writeableBitmap.Pixels);

                // Set the source of our image to the WriteableBitmap
                photoImage.Source = writeableBitmap;

                

                //start count doown timer
                countDownTimer = new DispatcherTimer();
                countDownTimer.Interval = new TimeSpan(0, 0, 0, 1);
                countDownTimer.Tick += new EventHandler(countDownTimerEvent);
                // countDownTimer.Start();

                txtCountdown.Text = "\n" + "seconds remaining";

                ReadExif(info);
                
            }

        }
     
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            cameraTask.Show();
            seki.IsEnabled = true;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            // Create a new PhotoChooser task
            PhotoChooserTask photoChooser = new PhotoChooserTask();
            // Add an event handler for when it has completed
            photoChooser.Completed += photoChooser_Completed;
            // Show our PhotoChooserTask
            photoChooser.Show();
            //photoChooserTask.Show();

            seki.IsEnabled = true;
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var indicator = new ProgressIndicator
            {

                IsVisible = true,

                IsIndeterminate = true,

                Text = "Matching diseases..."

            };


            SystemTray.SetProgressIndicator(this, indicator);
            //start count doown timer
            countDownTimer = new DispatcherTimer();
            countDownTimer.Interval = new TimeSpan(0, 0, 0, 1);
            countDownTimer.Tick += new EventHandler(countDownTimerEvent);
            countDownTimer.Start();
            txtCountdown.Text = "\n" + "seconds remaining";
    
        }


        int count = 10;
        
        void countDownTimerEvent(object sender, EventArgs e)
        {
            txtCountdown.Text = count + " Seconds Remaining";
            if (count > 0)
            {
                count--;
            }
            if (count == 1 && seb==2000 )
            {
                           MessageBoxResult result = MessageBox.Show("the plant leaf is normal\n","Thank you",
                MessageBoxButton.OKCancel);
                {
                           if (result == MessageBoxResult.OK)
                           {
                               NavigationService.Navigate(new Uri("/proces.xaml", UriKind.Relative));
                           }
                           else
                           {
                               NavigationService.Navigate(new Uri("/proces.xaml", UriKind.Relative));

                           }
                       }
            }

                if (count == 1 && seb == 3552)
                {
                   MessageBoxResult results =
     MessageBox.Show("the leaf is infected with tomato blight\n",
     "Thank you",
     MessageBoxButton.OKCancel);
                   if (results == MessageBoxResult.OK)
                   {
                       NavigationService.Navigate(new Uri("/Pan.xaml", UriKind.Relative));
                   }
                   else
                   {
                       NavigationService.Navigate(new Uri("/proces.xaml", UriKind.Relative));

                   }
                }
            
                var indicator1 = new ProgressIndicator
             {

                 IsVisible = true,

                 IsIndeterminate = true,

                 Text = "Finished"
                 
             };

                SystemTray.SetProgressIndicator(this, indicator1);

             
                
                
                
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri(string.Format("/proces.xaml?Refresh=true&random={0}", Guid.NewGuid()), UriKind.Relative));
            
        }
        void photoChooser_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                JpegInfo info = ExifLib.ExifReader.ReadJpeg(e.ChosenPhoto, e.OriginalFileName);
                // Load the picture in a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(e.ChosenPhoto);
                // Place the loaded image in a WriteableBitmap
                WriteableBitmap writeableBitmap = new WriteableBitmap(bitmap);
                // Execute the filter on the pixels of the bitmap
                ApplyFilter(writeableBitmap.Pixels);
                
                // Set the source of our image to the WriteableBitmap
                photoImage.Source = writeableBitmap;

                

                //start count doown timer
                countDownTimer = new DispatcherTimer();
                countDownTimer.Interval = new TimeSpan(0, 0, 0, 1);
                countDownTimer.Tick += new EventHandler(countDownTimerEvent);
                //countDownTimer.Start();
                txtCountdown.Text = "\n" + "seconds remaining";
                
                    ReadExif(info);
                
            }
        }
        void ColorRange(int i)
        {
            // Range of RGB {Red, Amber, Green}
            int[] rMin = new int[3] { 145, 159, 24 };
            int[] gMin = new int[3] { 15, 117, 74 };
            int[] bMin = new int[3] { 0, 15, 57 };
            int[] rMax = new int[3] { 255, 255, 165 };
            int[] gMax = new int[3] { 106, 255, 236 };
            int[] bMax = new int[3] { 68, 105, 222 };
            // Voting using predetermined color range
            for (int j = 0; j < 3; j++)
                if (
                pixelsRed[i] >= rMin[j] &&
               pixelsRed[i] <= rMax[j] &&
               pixelsGreen[i] >= gMin[j] &&
                pixelsGreen[i] <= gMax[j] &&
                pixelsBlue[i] >= bMin[j] &&
               pixelsBlue[i] <= bMax[j]
               )
                    ++pixelCount[j];
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

 void PumpFrames()
        {
            //try
            //{
                //while (accessDetection)
                //{ 
                    //pauseEvent.WaitOne();
                    //camera.GetPreviewBufferArgb32(source);
                    // Copies the current buffer for further manipulation
                    for (int i = 0; i < 800 * 270; i++)
                        pixels[i] = source[i];
                    // Separate pixel value to their corresponding RGB
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixelsRed[i] = (pixels[i] & 0x00ff0000) >> 16;
                        pixelsGreen[i] = (pixels[i] & 0x0000ff00) >> 8;
                        pixelsBlue[i] = (pixels[i] & 0x000000ff);
                    }
    }

 private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
 {
     NavigationService.Navigate(new Uri("/settings.xaml", UriKind.Relative));
 }


}}