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
using Microsoft.Xna.Framework;

namespace PDIS
{
    public partial class trls : PhoneApplicationPage
    {
        //for timer
        DispatcherTimer countDownTimer;
        public trls()
        {
            InitializeComponent();

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(50);
            dt.Tick += delegate { try { FrameworkDispatcher.Update(); } catch { } };
            dt.Start();
            //seki.IsEnabled = false;
        }

        private void seki_Click(object sender, RoutedEventArgs e)
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
            if (count == 1)
            {

                var indicator1 = new ProgressIndicator
             {

                 IsVisible = true,

                 IsIndeterminate = true,

                 Text = "Finished"

             };

                SystemTray.SetProgressIndicator(this, indicator1);
                //MessageBox.Show("the leaf is infected with tomato blight");
                MessageBoxResult result =
            MessageBox.Show("the leaf is infected with tomato blight\n",
            "Thank you",
            MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/Pan.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("/trls.xaml", UriKind.Relative));

                }

            }
        }
    }
}