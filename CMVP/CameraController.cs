using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video.DirectShow;
using AForge.Video;
using AForge;

namespace CMVP
{
    public partial class CameraController
    {
        private List<Camera> allCameras = new List<Camera>();//All that is connected to the computer
        private List<Camera> includedCameras = new List<Camera>(); //Cameras that is included is in simulation
        private List<IntPoint> resolutionList = new List<IntPoint>();
        private int seqetialCameraDelay = 300;//Used while starting ameras to prevent overload of computer
        private Timer updatePreview; //timer to controll camera preview in setting tab
        private IntPoint preferedCameraResolution; //Used in calibration

        public CameraController()
        {

        }
    }
}
