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
using System.ComponentModel;




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
    public partial class Camera : VideoStream
    {
        private static int idCount=0;
        private int id;
        private Bitmap img;
        private Bitmap imgCatch;
        private VideoCaptureDevice videoSource = null;
        private System.Drawing.Point offset;
        private Boolean imgLock;
        private List<Panel> panelsToUpdate;
        public Camera()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if(videoDevices.Count==0)
            {
                throw new ApplicationException();
            }
            videoSource= new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.SetCameraProperty(CameraControlProperty.Focus, 0, CameraControlFlags.Manual); //sets focus to 0 and turns off autofocus
            init(videoSource,new System.Drawing.Point(0,0));
        }
        public Camera(VideoCaptureDevice videoSource,System.Drawing.Point offset)
        {
            init(videoSource, offset);
        }
        public Camera(VideoCaptureDevice videoSource)
        {
            //Initiat camera with an offset of [0,0]
            init(videoSource, new System.Drawing.Point(0, 0));
        }
        private void init(VideoCaptureDevice videoSource, System.Drawing.Point offset)
        {
            this.imgLock = true;
            this.offset = offset;
            id = idCount++;
            this.videoSource = videoSource;
            this.panelsToUpdate = new List<Panel>();
            if(videoSource== null)
            {
                Console.WriteLine("Fel");
            }
            else
                this.videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            img = new Bitmap(100, 100);
            imgLock = false;
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
                    img = (Bitmap)e.Frame.Clone() as Bitmap;
                    foreach(Panel panel in panelsToUpdate)
                    {
                        if (panel == null)
                        {
                            panelsToUpdate.Remove(panel);
                        }
                        else
                        {
                            panel.BackgroundImage = img;
                        }
                    }
                    done = true;
                }
                catch(Exception ex)
                {      
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Error in try statemen in camera, could not upload new img");
                }
            }
        }
        public Bitmap getImage()
        {
            return img;
        }
        public void start()
        {
            if (!videoSource.IsRunning)
                videoSource.Start();
        }
        public void stop()
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
        public VideoCaptureDevice getVideoSouce()
        {
            return videoSource;
        }
        public void pushDestination(Panel panel)
        {
            panelsToUpdate.Add(panel);
        }
        public void removeDestination(Panel panel)
        {
            panelsToUpdate.Remove(panel);
        }
        public Size getSize()
        {
            return videoSource.VideoCapabilities[0].FrameSize;
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
