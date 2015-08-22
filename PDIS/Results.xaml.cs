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

namespace PDIS
{
    public partial class Results : PhoneApplicationPage
    {
        public Results()
        {
            InitializeComponent();

            Random slumpGenerator = new Random();
            int tal = slumpGenerator.Next(0, 10);

            //assigning percentage of the accurracy
            if (tal <= 6)
            {

                kim.Text = "plant leaf is infected at" + " " + (100 - (tal * 10)) + "%";

            }
            if (tal > 7)
            {
                kim.Text = "Plant leaf is normal at" + " " + (tal * 10) + "%";

            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/tabs.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            SmsComposeTask SMSCompose = new SmsComposeTask();
            SMSCompose.To = "";
            SMSCompose.Body = "Thank you for Scannig with PDIS.\n\n Your results are:";
            SMSCompose.Show();
        }
    }
}