using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace PDIS
{
    public partial class detect : PhoneApplicationPage
    {
        WriteableBitmap mainStream;
        private Thread videoThread;
        private static ManualResetEvent pauseEvent = new ManualResetEvent(true);
        PhotoCamera camera = new PhotoCamera();
        public SoundEffect soundPlay { get; set; }
        public string soundPath { get; set; }
        public bool accessDetection = false;
        public bool clickDetection = true;
        public bool stopNotify = false;
        public int counterNotify = 0;
        public int counterThread = 0;
        public int statusNew = 0;
        public int statusOld = 0;
        int[] source = new int[800 * 450];
        int[] pixels = new int[800 * 270];
        int[] pixelsRed = new int[800 * 270];
        int[] pixelsGreen = new int[800 * 270];
        int[] pixelsBlue = new int[800 * 270];
        int[] pixelCount = new int[3] { 0, 0, 0 };
        public detect()
        {
            InitializeComponent();
            
            // Delay 2s (2000ms) for splash screen
           // Thread.Sleep(2000);
        }
        // Code for initialization and setting the source for the viewfinder
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            camera = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
            // Event is fired when PhotoCamera object has beeninitialized.
            camera.Initialized += new EventHandler<Microsoft.Devices.CameraOperationCompletedEventArgs>(cam_Initialized);
            camera.AutoFocusCompleted += new EventHandler<CameraOperationCompletedEventArgs>(cam_AutoFocusCompleted);
            //Set the VideoBrush source to the video
            mainCameraBrush.SetSource(camera);
        }
        // Close camera initialization before go to other page
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (camera != null)
            {
                // Dispose camera to minimize power consumption and to expedite shutdown
                camera.Dispose();
                // Release memory, ensure garbage collection
                camera.Initialized -= cam_Initialized;
                camera.AutoFocusCompleted -= cam_AutoFocusCompleted;
            }
        }
        // Ensure that the viewfinder is upright in LandscapeRight.
        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if (camera != null)
            {
                // LandscapeRight rotation when camera is on back of phone
                int landscapeRightRotation = 180;
                // Change LandscapeRight rotation for front-facing camera
                if (camera.CameraType == CameraType.FrontFacing)
                    landscapeRightRotation = -180;
                // Rotate video brush from camera
                if (e.Orientation == PageOrientation.PortraitUp)
                {
                    // Rotate for LandscapeRight orientation
                    mainCameraBrush.RelativeTransform = new CompositeTransform()
                    {
                        CenterX = 0.5,
                        CenterY = 0.5,
                        Rotation = landscapeRightRotation
                    };
                }
                else
                {
                    // Rotate for standard landscape orientation
                    mainCameraBrush.RelativeTransform = new CompositeTransform()
                    {
                        CenterX = 0.5,
                        CenterY = 0.5,
                        Rotation = 0
                    };
                }
            }
            base.OnOrientationChanged(e);
        }
        // Update the UI if initialization succeeds
        void cam_Initialized(object sender, Microsoft.Devices.CameraOperationCompletedEventArgs e)
        {
            if (e.Succeeded)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    ApplicationBarMenuItem menuStart = (ApplicationBarMenuItem)ApplicationBar.MenuItems[1];
                    menuStart.IsEnabled = true;
                    phoneStatus.Text = "CAMERA READY";
                });
            }
        }
        // Start autofocus when screen is tap
        void cam_AutoFocus(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (camera.IsFocusSupported == true)
            {
                // Focus when a capture is not in progress
                try
                {
                    camera.Focus();
                }
                // Cannot focus when a capture is in progress
                catch (Exception focusError)
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        phoneStatus.Text = focusError.Message;
                    });
                }
            }
            else
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    phoneStatus.Text = "CAMERA NOT SUPPORT PROGRAMMABLE AUTOFOCUS";
                });
            }
        }
        // Update status of autofocus
        void cam_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(delegate()
            {
                phoneStatus.Text = "AUTOFOCUS COMPLETED";
            });
        }

        // Determine pixel color range
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

        // Set notification for current status
        void NotifyUser()
        {
            // Count comparison between predetermined color range
            if (pixelCount[0] > pixelCount[1] && pixelCount[0] > pixelCount[2])
                statusNew = 0;
            if (pixelCount[1] > pixelCount[0] && pixelCount[1] > pixelCount[2])
                statusNew = 1;
            if (pixelCount[2] > pixelCount[0] && pixelCount[2] > pixelCount[1])
                statusNew = 2;
            // Show the total count for each predetermined color range
            this.Dispatcher.BeginInvoke(delegate()
            {
                currentCount.Text = "RED -> " + pixelCount[0] +
                "\nAMBER -> " + pixelCount[1] +
                "\nGREEN -> " + pixelCount[2];
                phoneStatus.Text = "THREAD #" + counterThread;
            });
            // Repeat notification after 3x same signal
            if (counterNotify > 2)
                stopNotify = false;
            // Green to Amber
            if (statusOld == 2 && statusNew == 1)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    notifyStatusBackground.Width = 195;
                    notifyStatus.Text = "SLOW";
                    notifyStatus.Foreground =
                   new SolidColorBrush(Colors.Orange);
                    currentStatus.Text = "STATUS -> GREEN-AMBER";
                });
                soundPath = "Audio/slow.wav";
            }
            // Amber to Red
            if (statusOld == 1 && statusNew == 0)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    notifyStatusBackground.Width = 180;
                    notifyStatus.Text = "STOP";
                    notifyStatus.Foreground =
                    new SolidColorBrush(Colors.Red);
                    currentStatus.Text = "STATUS -> AMBER-RED";
                });
                soundPath = "Audio/stop.wav";
                MessageBox.Show("a tomato detected");
            }
            // Red to Green
            if (statusOld == 0 && statusNew == 2)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    notifyStatusBackground.Width = 100;
                    notifyStatus.Text = "GO";
                    notifyStatus.Foreground =
                    new SolidColorBrush(Colors.Green);
                    currentStatus.Text = "STATUS -> RED-GREEN";
                });
                soundPath = "Audio/go.wav";
                MessageBox.Show("that is green leaf");
            }
            // Green to Green
            if (statusOld == 2 && statusNew == 2)
            {
                if (stopNotify == false)
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        notifyStatusBackground.Width = 100;
                        notifyStatus.Text = "GO";
                        notifyStatus.Foreground =
                        new SolidColorBrush(Colors.Green);
                        currentStatus.Text = "STATUS -> GREEN-GREEN";
                    });
                    soundPath = "Audio/go.wav";
                    MessageBox.Show("that is green leaf");
                    counterNotify = 0;
                    stopNotify = true;
                }
                else
                {
                    soundPath = "Audio/silent.wav";
                    ++counterNotify;
                }
            }
            // Red to Red
            if (statusOld == 0 && statusNew == 0)
            {
                if (stopNotify == false)
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        notifyStatusBackground.Width = 180;
                        notifyStatus.Text = "WAIT";
                        notifyStatus.Foreground =
                        new SolidColorBrush(Colors.Red);
                        currentStatus.Text = "STATUS -> RED-RED";
                    });
                    soundPath = "Audio/wait.wav";
                    MessageBox.Show("still a tomato");
                    counterNotify = 0;
                    stopNotify = true;
                }
                else
                {
                    soundPath = "Audio/silent.wav";
                    ++counterNotify;
                }
            }
            // Amber to Amber
            if (statusOld == 1 && statusNew == 1)
            {
                if (stopNotify == false)
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        notifyStatusBackground.Width = 195;
                        notifyStatus.Text = "SLOW";
                        notifyStatus.Foreground =
                        new SolidColorBrush(Colors.Orange);
                        currentStatus.Text = "STATUS -> AMBER-AMBER";
                    });
                    soundPath = "Audio/slow.wav";
                    counterNotify = 0;
                    stopNotify = true;
                }
                else
                {
                    soundPath = "Audio/silent.wav";
                    ++counterNotify;
                }
            }
            // Green to Red
            if (statusOld == 2 && statusNew == 0)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    notifyStatusBackground.Width = 0;
                    notifyStatus.Text = "";
                    notifyStatus.Foreground = null;
                    currentStatus.Text = "STATUS -> GREEN-RED";
                });
                soundPath = "Audio/silent.wav";
            }

            // Amber to Green
            if (statusOld == 1 && statusNew == 2)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    notifyStatusBackground.Width = 0;
                    notifyStatus.Text = "";
                    notifyStatus.Foreground = null;
                    currentStatus.Text = "STATUS -> AMBER-GREEN";
                });
                soundPath = "Audio/silent.wav";
            }

            // Red to Amber
            if (statusOld == 0 && statusNew == 1)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    notifyStatusBackground.Width = 0;
                    notifyStatus.Text = "";
                    notifyStatus.Foreground = null;
                    currentStatus.Text = "STATUS -> RED-AMBER";
                });
                soundPath = "Audio/silent.wav";
            }
            // Load the sound file
            StreamResourceInfo info = Application.GetResourceStream(new Uri(soundPath, UriKind.Relative));
            soundPlay = SoundEffect.FromStream(info.Stream);
            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
            soundPlay.Play();
            // Send new status to old status & counterThread increment
            statusOld = statusNew;
            ++counterThread;
            // Delay next thread for 3sec (3000ms)
            Thread.Sleep(3000);
        }
        // Navigation to About Page event
        private void About_Clicked(object sender, EventArgs e)
        {
            // Navigate to About Page
            NavigationService.Navigate(new Uri("/AboutPage.xaml",UriKind.Relative));
            // Check whether detection process is working
            if (!clickDetection)
                Detection_Clicked(sender, e);
        }
        // Start or stop detection process event
        private void Detection_Clicked(object sender, EventArgs e)
        {
            ApplicationBarMenuItem menuStart = (ApplicationBarMenuItem)ApplicationBar.MenuItems[1];
            this.Dispatcher.BeginInvoke(delegate()
            {
                videoThread = new System.Threading.Thread(PumpFrames);
                mainStream = new WriteableBitmap((int)camera.PreviewResolution.Width,
               (int)camera.PreviewResolution.Height);
                overlayCamera.Source = mainStream;
                notifyStatusBackground.Width = 0;
                notifyStatus.Text = "";
                currentStatus.Text = "";
                currentCount.Text = "";
            });
            if (clickDetection)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    videoThread.Start();
                    menuStart.IsEnabled = true;
                    menuStart.Text = "stop";
                    phoneStatus.Text = "DETECTING...";
                });
                clickDetection = false;
                accessDetection = true;
            }
            else
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    videoThread.Abort();
                    menuStart.IsEnabled = false;
                    menuStart.Text = "start";
                    phoneStatus.Text = "STOPPING DETECTION...";
                });
                counterThread = 0;
                stopNotify = false;
                clickDetection = true;
                accessDetection = false;
            }
        }
        // Detection process
        void PumpFrames()
        {
            try
            {
                while (accessDetection)
                {
                    pauseEvent.WaitOne();
                    camera.GetPreviewBufferArgb32(source);
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
                    // Segmentation left box (j:height size, k:width size)
                    for (int j = 0; j < 270; j++)
                        for (int k = 0; k < 200; k++)
                            ColorRange((800 * j) + 0 + k);
                    // Segmentation right box (j:height size, k:width size)
                    for (int j = 0; j < 270; j++)
                        for (int k = 0; k < 200; k++)
                            ColorRange((800 * j) + 600 + k);
                    // Activate notify user event
                    NotifyUser();
                    InitializeComponent();
                    // Reset pixel count value after display
                    for (int j = 0; j < 3; j++)
                        pixelCount[j] = 0;
                    // Clean all array before next detection
                    Array.Clear(pixels, 0, pixels.Length);
                }
            }
            catch (Exception e)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    phoneStatus.Text = e.Message;
                });
            }
            finally
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    ApplicationBarMenuItem menuStart = (ApplicationBarMenuItem)ApplicationBar.MenuItems[1];
                    phoneStatus.Text = "PRESS START TO DETECT";
                    menuStart.IsEnabled = true;
                });
            }
        }
    }
}