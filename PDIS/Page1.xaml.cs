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
using ExifLib;
using System.Windows.Media.Imaging;

namespace PDIS
{
    public partial class Page1 : PhoneApplicationPage
    {
        bool photoLoaded = false;
        public Page1()
        {
            this.Loaded += new RoutedEventHandler(Page1_Loaded);
            InitializeComponent();
            
        }

        void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            if (!photoLoaded)
            {
                PhotoChooserTask task = new PhotoChooserTask();
                task.Completed += new EventHandler<PhotoResult>(task_Completed);
                task.Show();
            }
        }

        void task_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                photoLoaded = true;
                JpegInfo info = ExifLib.ExifReader.ReadJpeg(e.ChosenPhoto, e.OriginalFileName);
                var img = new BitmapImage();
                img.SetSource(e.ChosenPhoto);
                loadedImg.Source = img;
                ReadExif(info);
            }
        }

        private void ReadExif(JpegInfo info)
        {
            artist.Text = "Artist: " + info.Artist;
            copyright.Text = "Copyright: " + info.Copyright;
            datetime.Text = "Datetime: " + info.DateTime;
            description.Text = "Description: " + info.Description;
            exposure.Text = "Exposure: " + info.ExposureTime;
            filename.Text = "File name: " + info.FileName;
            filesize.Text = "File size: " + info.FileSize;
            flash.Text = "Flash: " + info.Flash;
            fnumber.Text = "F number: " + info.FNumber;
            gpslatitude.Text = "GPS Latitude: " + info.GpsLatitude;
            gpslatituderef.Text = "GPS Latitude ref: " + info.GpsLatitudeRef;
            gpslongitude.Text = "GPS Longitude: " + info.GpsLongitude;
            gpslongituderef.Text = "GPS Longitude ref: " + info.GpsLongitudeRef;
            height.Text = "Height: " + info.Height;
            iscolor.Text = "Is color: " + info.IsColor;
            isvalid.Text = "Is valid: " + info.IsValid;
            loadtime.Text = "Load time: " + info.LoadTime;
            make.Text = "Make: " + info.Make;
            orientation.Text = "Orientation: " + info.Orientation;
            resolutionunit.Text = "Resolution unit: " + info.ResolutionUnit;
            software.Text = "Software: " + info.Software;
            thumbdata.Text = "Thumbnail data: " + info.ThumbnailData;
            thumboffset.Text = "Thumbnail offset: " + info.ThumbnailOffset;
            thumbsize.Text = "Thumbnail size: " + info.ThumbnailSize;
            usercomment.Text = "User comment: " + info.UserComment;
            width.Text = "Width: " + info.Width;
            xresolution.Text = "X resolution: " + info.XResolution;
            yresolution.Text = "Y resolution: " + info.YResolution;
            photoLoaded = true;

           if( info.Width == 2000)
           {
               wid.Text = "de";
           }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            
        }

    }
}