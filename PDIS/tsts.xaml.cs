using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;

namespace PDIS
{
    public partial class tsts : PhoneApplicationPage
    { //for camera
        CameraCaptureTask cameraTask = null;
        PhotoChooserTask photoChooserTask = null;
        //for timer
        DispatcherTimer countDownTimer;


        public tsts()
        {
            InitializeComponent();

            cameraTask = new CameraCaptureTask();
            cameraTask.Completed += new EventHandler<PhotoResult>(cameraTask_Completed);

            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(50);
            dt.Tick += delegate { try { FrameworkDispatcher.Update(); } catch { } };
            dt.Start();
            seki.IsEnabled = false;
        }

        void cameraTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK) return;

            System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
            bmp.SetSource(e.ChosenPhoto);
            photoImage.Source = bmp;
        }
        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK) return;

            System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
            bmp.SetSource(e.ChosenPhoto);
            photoImage.Source = bmp;

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            cameraTask.Show();
            seki.IsEnabled = true;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            photoChooserTask.Show();
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
            if (count == 1 )
            {
                
                var indicator1 = new ProgressIndicator
             {

                 IsVisible = true,

                 IsIndeterminate = true,

                 Text = "Finished"

             };

                SystemTray.SetProgressIndicator(this, indicator1);
                MessageBoxResult result =
       MessageBox.Show("the leaf is normal\n",
       "Thank you",
       MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/tabs.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("/proces.xaml", UriKind.Relative));

                }
                //NavigationService.Navigate(new Uri("/Results.xaml", UriKind.Relative));
               /*
                Random slumpGenerator = new Random();
                // Or whatever limits you want... Next() returns a double
                int tal = slumpGenerator.Next(0, 10);

                textbox.Text = tal.ToString();
                //assigning percentage of the accurracy
                if (tal <= 4)
                {

                    txtCountdown.Text = "Infected with tomato blight";
                    //MessageBox.Show("The leaf is infected at");
                }
                if (tal > 5)
                {
                    txtCountdown.Text = "Normal";
                    //MessageBox.Show("The leaf is Normal at");
                } */
               }
                
                
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri(string.Format("/proces.xaml?Refresh=true&random={0}", Guid.NewGuid()), UriKind.Relative));
            
        }

    }


}