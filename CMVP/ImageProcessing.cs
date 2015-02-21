using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CMVP
{
    class ImageProcessing : VideoStream
    {
        List<int> objects;
        private VideoStream videoStream;
        private Bitmap img;
        private List<Panel> panelsToUpdate;
        private Timer imgProcesTimer;


        public ImageProcessing(VideoStream videoStream)
        {
            System.Console.WriteLine("CreatImageProcessingClass");
            this.imgProcesTimer = new Timer();
            this.imgProcesTimer.Interval=20;
            this.imgProcesTimer.Tick += new EventHandler(updatePanels);
            objects = new List<int>();
            this.panelsToUpdate = new List<Panel>();
            this.videoStream = videoStream;
        }
        void updatePanels(object sender, EventArgs e)
        {
            img = videoStream.getImage();
            foreach(Panel panel in panelsToUpdate)
            {
                panel.BackgroundImage = img;
            }
        }
        public void start()
        {
            imgProcesTimer.Start();
        }
        public void stop()
        {
            imgProcesTimer.Stop();
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
            return new Size(0, 0);
        }

        public Bitmap getImage()
        {
            return img;
        }

        //function to get triangels in sets of 3 points from a list of points
        private List<System.Drawing.Point[]> getTriangels(System.Drawing.Point[] points)
        {
            List<System.Drawing.Point[]> pointList = new List<System.Drawing.Point[]>();
            for (int k = 0; k < points.Length; k++)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    for (int h = 0; h < points.Length; h++)
                    {
                        if (k != i && k != h && i != h)
                        {
                            System.Drawing.Point[] locations = new System.Drawing.Point[3];
                            locations[0] = points.ElementAt(k);
                            locations[1] = points.ElementAt(i);
                            locations[2] = points.ElementAt(h);
                            pointList.Add(locations);
                        }
                    }
                }
            }
            return pointList;
        }
        
    }
}
