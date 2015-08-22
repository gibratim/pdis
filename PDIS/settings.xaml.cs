using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
namespace PDIS
{
    public partial class settings : PhoneApplicationPage
    {
        Accelerometer accelerometer;
        public settings()
        {
            InitializeComponent();
            if (!Accelerometer.IsSupported)
            {
                //if the device doesnot support accelerometer then bujolo of the buttons are disabled
                statusTextBlock.Text = "device does not support accelerometer";
                startButton.IsEnabled = false;
                stopButton.IsEnabled = false;
            }


        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {

            if (accelerometer == null)
            {

                //instantiate the accelerometer
                accelerometer = new Accelerometer();
                accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            }

            try
            {
                statusTextBlock.Text = "starting accelerometer";
                accelerometer.Start();
            }
            catch (InvalidOperationException ex)
            {
                statusTextBlock.Text = "unable to start accelerometer";
            }
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            //call update on the UI thread and pass the accelerometer reading
            Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
        }

        private void UpdateUI(AccelerometerReading accelerometerReading)
        {
            statusTextBlock.Text = "getting data";
            Vector3 acceleration = accelerometerReading.Acceleration;
            //show numeric value
            xTextBlock.Text = "X: " + acceleration.X.ToString("0.00");
            yTextBlock.Text = "Y: " + acceleration.Y.ToString("0.00");
            zTextBlock.Text = "Z: " + acceleration.Z.ToString("0.00");

            //show value graphically
            xLine.X2 = xLine.X1 + acceleration.X * 200;
            yLine.Y2 = yLine.Y1 + acceleration.Y * 200;
            zLine.X2 = zLine.X1 + acceleration.Z * 100;
            zLine.X2 = zLine.Y1 + acceleration.Z * 100;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {

            if (accelerometer != null)
            {
                accelerometer.Stop();
                statusTextBlock.Text = "accelerometer stopped";

            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/proces.xaml", UriKind.Relative));
        }
    }
}