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
    public partial class Camera
    {
        private static int idCount=0;
        private int id;
        private Bitmap img;
        private VideoCaptureDevice videoSource = null;
        private System.Drawing.Point offset;

        public Camera(VideoCaptureDevice videoSource,System.Drawing.Point offset)
        {
            init(videoSource, offset);


        }
        public Camera(VideoCaptureDevice videoSource)
        {
            init(videoSource, new System.Drawing.Point(0, 0));
        }
        private void init(VideoCaptureDevice videoSource, System.Drawing.Point offset)
        {
            this.offset = offset;
            id = idCount++;
            this.videoSource = videoSource;
            if(videoSource== null)
            {
                Console.WriteLine("Fel");
            }
            else
                this.videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            img = new Bitmap(4000, 4000);
        }
        public System.Drawing.Point getOffset()
        {
            return offset;
        }
        public Bitmap getImg()
        {
            return img;
        }
        public int getId()
        {
            return id;
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
        public VideoCaptureDevice getVideoSouce()
        {
            return videoSource;
        }
        public VideoCapabilities[] getVideoCapabilities()
        {
            return videoSource.VideoCapabilities;
        }
        public void setResolution(IntPoint resolution)
        {
            foreach(VideoCapabilities cap in videoSource.VideoCapabilities)
            {
                if(cap.FrameSize.Width == resolution.X && cap.FrameSize.Height == resolution.Y)
                {
                    videoSource.VideoResolution = cap;
                    if(videoSource.IsRunning)
                    {
                        videoSource.Stop();
                        videoSource.Start();
                    }
                    return;
                }
            }
            throw new FormatException("ERROR: Resolution " + resolution.X + " x " + resolution.Y + "is not supported by all included cameras.");

        }
    }
}  
