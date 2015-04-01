using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using FlyCapture2Managed;
using FlyCapture2Managed.Gui;


namespace CMVP
{

    public class PTGreyCamera : VideoStream
    {
        
        private FlyCapture2Managed.Gui.CameraControlDialog m_camCtlDlg;
        private ManagedCameraBase m_camera = null;
        private ManagedImage m_rawImage;
        private ManagedImage m_processedImage;
        private bool m_grabImages;
        private AutoResetEvent m_grabThreadExited;
        private BackgroundWorker m_grabThread;
        private Bitmap image;
        private List<Panel> panelsToUpdate;
        private double time;

        public PTGreyCamera()
        {
            m_rawImage = new ManagedImage();
            m_processedImage = new ManagedImage();
            m_camCtlDlg = new CameraControlDialog();
            m_grabThreadExited = new AutoResetEvent(false);
            panelsToUpdate = new List<Panel>();
            setup();
        }
        ~PTGreyCamera()
        {
            if (m_camera != null)
            {
                m_camera.Dispose();
            }
            m_rawImage.Dispose();
            m_processedImage.Dispose();
            m_camCtlDlg.Disconnect();
            m_grabThreadExited.Dispose();
            disconnect();
            Console.WriteLine("Closing Camera");      
        }

        public Bitmap getImage()
        {
            return image;
        }

        private void StartGrabLoop()
        {
            m_grabThread = new BackgroundWorker();
            m_grabThread.ProgressChanged += new ProgressChangedEventHandler(UpdateUI);
            m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            m_grabThread.WorkerReportsProgress = true;
            m_grabThread.RunWorkerAsync();
        }

        private void UpdateUI(object sender, ProgressChangedEventArgs e)
        {
 	        image = m_processedImage.bitmap;
   
            foreach (Panel p in panelsToUpdate)
            {
                p.BackgroundImage = image;
            }
        }

        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("GrabLoop started");
            BackgroundWorker worker = sender as BackgroundWorker;
            while (m_grabImages)
            {
                try
                {
                    m_camera.RetrieveBuffer(m_rawImage);

                }
                catch (FC2Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    continue;
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine("Error_ " + ex.Message);
                    Console.WriteLine("No camera found: GrabLoop stopped");
                    stop();
                    continue;
                }

                lock (this)
                {
                    m_rawImage.Convert(PixelFormat.PixelFormatBgr, m_processedImage);
                    time = (double) System.DateTime.Now.Ticks*Math.Pow(10,-7);
                }
  
                try
                {
                    worker.ReportProgress(0);
                }
                catch (InvalidOperationException ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Worker report failed!");
                    continue;
                }
                

            }
        }

            private void setup()
            {
                CameraSelectionDialog camSlnDlg = new CameraSelectionDialog();
                bool retVal = camSlnDlg.ShowModal();
                if (retVal)
                {

                    try
                    {
                        ManagedPGRGuid[] selectedGuids = camSlnDlg.GetSelectedCameraGuids();
                        ManagedPGRGuid guidToUse = selectedGuids[0];

                        ManagedBusManager busMgr = new ManagedBusManager();
                        InterfaceType ifType = busMgr.GetInterfaceTypeFromGuid(guidToUse);

                        if (ifType == InterfaceType.GigE)
                        {
                            m_camera = new ManagedGigECamera();
                        }
                        else
                        {
                            m_camera = new ManagedCamera();
                        }


                        // Connect to the first selected GUID
                        m_camera.Connect(guidToUse);
                        m_camCtlDlg.Connect(m_camera);

                        CameraInfo camInfo = m_camera.GetCameraInfo();

                        // Set embedded timestamp to on
                        EmbeddedImageInfo embeddedInfo = m_camera.GetEmbeddedImageInfo();
                        embeddedInfo.timestamp.onOff = true;
                        m_camera.SetEmbeddedImageInfo(embeddedInfo);

                        m_camera.StartCapture();

                        m_grabImages = true;
                        System.Console.WriteLine("Camera OK");

                    }
                    catch (FC2Exception ex)
                    {
                        Debug.WriteLine("Failed to load form successfully: " + ex.Message);
                        Environment.ExitCode = -1;
                        Application.Exit();
                        return;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine("No camera found: " + ex.Message);
                        Environment.ExitCode = -1;
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    Environment.ExitCode = -1;
                    Application.Exit();
                    return;
                }
            }


        public void disconnect()
        {
            try
            {

                m_camera.Disconnect();
            }
            catch (FC2Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    
        public void start()
        {
            m_grabImages = true;
            StartGrabLoop();
        }
        public void showCameraSettings()
        {
            m_camCtlDlg.Show();
        }
         public void stop()
        {
            m_grabImages = false;
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
            return image.Size;
        }
        public double getTime()
        {
            if (m_grabImages)
            {
                return  time;
            }
            else
            {
                return -1;
            }

        }
    }
}
