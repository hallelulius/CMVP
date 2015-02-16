using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Vision.Motion;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;

namespace CMVP
{
    public partial class Camera : UserControl
    {
        private static List<Camera> cams;
        private int id;
        private Bitmap img;
        private VideoCaptureDevice videoSource = null;

        public Camera(VideoCaptureDevice videoSource)
        {
            this.videoSource = videoSource;
            this.videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            this.id = cams.Count;
            cams.Add(this);
            img = new Bitmap(4000, 4000);
        }
        //Eventhandler to updateImage
        //Not ready yet
        private void video_NewFrame(object sender, NewFrameEventArgs e)
        {
            Boolean done = false;
            while (!done)
            {
                try
                {
                    Bitmap imgCatch = img;
                    img = (Bitmap)e.Frame.Clone();
                    done = true;
                    //Här ska kod läggas till för att lägga bilden på lämpligt ställe.
                }
                catch
                {
                    Console.WriteLine("Error in try statemen in camera, could not upload new img");
                }
            }
        }
        public void startCamera()
        {
            if (!videoSource.IsRunning)
                videoSource.Start();
        }
        public void stopCamera()
        {
            if (videoSource.IsRunning)
                videoSource.Stop();
        }
        public bool isRunning()
        {
            return videoSource.IsRunning;
        }
        public void closeVideoSource()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }
        //Prevent sudden close while device is runnng
        private void Display_FormaClosed(object sender, FormClosedEventArgs e)
        {
            closeVideoSource();
        }
        //preview of the camera on the setting tab

        public void updatePreview()
        {
            Bitmap copy = cameraController.grabOneFrame(this);
            cameraImagePanel.BackgroundImage = new Bitmap(copy, new Size(cameraImagePanel.Width, cameraImagePanel.Height));
        }
        //checkbox to include camera in simulation
        private void includeCameraBox_CheckedChanged(object sender, EventArgs e)
        {
            isIncluded = includedCameraBox.Checked;

            if ((isIncluded))
            {
                cameraStatusLabel.ForeColor = Color.Green;
                cameraStatusLabl.Text = "Camera Included";
            }
            else
            {
                CameraStatusLabel.ForeColor.Red;
                cameraStatusLabel.Text = "Camera Excluded";
            }

        }
    }
}  
