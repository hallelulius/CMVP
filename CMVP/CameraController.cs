using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using AForge.Video.DirectShow;
using AForge.Video;
using AForge;

namespace CMVP
{
    public partial class CameraController : VideoStream
    {
        private List<Camera> allCameras = new List<Camera>();//All that is connected to the computer
        private List<Camera> includedCameras = new List<Camera>(); //Cameras that is included is in simulation
        private List<IntPoint> resolutionList = new List<IntPoint>();
        private int seqetialCameraDelay = 300;//Used while starting ameras to prevent overload of computer
       
        private IntPoint resolution;

        public CameraController()
        {
            resolution = new IntPoint(1920, 1080);
            useCamerasForSetting();
        }
        public void useCamerasForSetting()
        {
            if(!Program.isSimulating())
            {
                loadCamList();
                startAllCameras();
            }
        }
        private void loadCamList()
        {
            try
            {
                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice); //Sets focus to 0 and turns off autofocus
                stopAllCameras();
                allCameras.Clear();
                if (videoDevices.Count == 0)
                {
                    throw new ApplicationException();
                }
                int i = 0;
                foreach (FilterInfo device in videoDevices)
                {
                    allCameras.Add(new Camera(new VideoCaptureDevice(device.MonikerString)));
                    allCameras[i].getVideoSouce().SetCameraProperty(CameraControlProperty.Focus, 0, CameraControlFlags.Manual); //sets focus to 0 and turns off autofocus
                    i++;
                }
            }
            catch (ApplicationException)
            {
                //Errorkode "No capture devide on your system!
                Console.WriteLine("Error in try-statement in cameracontroller: No capture device found on your system");
            }
        }
        public void startAllCameras()
        {
            if(allCameras.Count > 0)
            {
                foreach(Camera cam in allCameras)
                {
                    cam.start();
                    //Delay used becaus lab.computer can't start multiple cameras simultaneously
                    System.Threading.Thread.Sleep(this.seqetialCameraDelay);
                }
            }
        }  
        public void startAllIncludedCameras()
        {
            stopAllCameras();
            foreach(Camera cam in includedCameras)
            {
                cam.setResolution(resolution);
            }
        }
        public void stopAllCameras()
        {
            foreach(Camera cam in allCameras)
            {
                if(cam !=null)
                {
                    cam.stop();
                }
            }
        }
        public Bitmap grabOneFrame(Camera cam)
        {
            Bitmap img = null;
            Boolean done = false;
            while(!done)
            {
                try
                {
                    if(cam.getImg()!= null)
                    {
                        img = cam.getImg();
                        done = true;
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Error in try-statement in cameracontroller: Couldn't get img from cam:" + cam.getId());
                }

            }
            if (img == null)
            {
                Console.WriteLine("Error in cameraControll: Img = null in GrabeOneFrame");
            }
            return img;
        }
        public List<Bitmap> grabAllaFrames()
        {
            List<Bitmap> bitmapList = new List<Bitmap>();
            foreach(Camera cam in includedCameras)
            {
                bitmapList.Add(grabOneFrame(cam));
            }
            return bitmapList;
        }
        public List<Camera> getIncludedCameras()
        {
            return includedCameras;
        }
        //Probably need to be adjust due to scaling and rotatonproblems.
        public Bitmap grabMergedFrame(int X,int Y)
        {
            Bitmap bigImg = new Bitmap(X, Y);
            Graphics g = Graphics.FromImage(bigImg);
            foreach(Camera cam in includedCameras)
            {
                Bitmap img = grabOneFrame(cam);
                g.DrawImage(img, cam.getOffset());
            }
            return bigImg;
        }
        /*
         * Used to update resolution list when cameras are updated so that the list only 
         *contains resolutin that all include cameras support
         */
        public void updateResolutionList()
        {
            resolutionList.Clear();
            List<VideoCapabilities> tempCap = new List<VideoCapabilities>();
            if(includedCameras.Count > 1)
            {
                tempCap = includedCameras[0].getVideoCapabilities().ToList();
                foreach(Camera cam in includedCameras)
                {
                    List<VideoCapabilities> tempCap2 = cam.getVideoCapabilities().ToList();
                    foreach(VideoCapabilities cap in tempCap)
                    {
                        if(!tempCap2.Contains(cap))
                        {
                            tempCap.Remove(cap);
                        }
                    }
                }
            }
            foreach(VideoCapabilities cap in tempCap)
            {
                resolutionList.Add(new IntPoint(cap.FrameSize.Width, cap.FrameSize.Height));
            }
        }
        public List<IntPoint> getResolutionList()
        {
            return resolutionList;
        }
        public int getNumberOfCameras()
        {
            return allCameras.Count;
        }
        public Bitmap getImage()
        {
            Bitmap temp=this.allCameras[0].getImage();
            return temp;
        }
        public void start()
        {
            
        }
        public void stop()
        {

        }
        public void pushDestination(Panel panel)
        {

        }
        public void removeDestination(Panel panel)
        {

        }
        public Size getSize()
        {
            return new Size(0, 0);
        }
        public double getTime()
        {
            return 0;
        }
    }

}
