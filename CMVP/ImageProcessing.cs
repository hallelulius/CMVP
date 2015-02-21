using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
    class ImageProcessing : VideoStream
    {
        List<Car> objects;
        private VideoStream videoStream;
        private Bitmap img;
        private Bitmap processedImage;
        private List<Panel> panelsToUpdate;
        private Timer imgProcesTimer;



        public ImageProcessing(VideoStream videoStream)
        {
            System.Console.WriteLine("CreatImageProcessingClass");
            this.imgProcesTimer = new Timer();
            this.imgProcesTimer.Interval=20;
            this.imgProcesTimer.Tick += new EventHandler(updatePanels);
            objects = new List<Car>();
            this.panelsToUpdate = new List<Panel>();
            this.videoStream = videoStream;
        }
        void updatePanels(object sender, EventArgs e)
        {
            img = videoStream.getImage();
            processedImage = processImage();
            foreach(Panel panel in panelsToUpdate)
            {
                panel.BackgroundImage = processedImage;
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
            return videoStream.getSize();
        }
        public Bitmap getImage()
        {
            return img;
        }
        
        private Bitmap processImage()
        {
            YCbCrFiltering filter = new YCbCrFiltering();
            filter.Y = new Range(0.9f, 1);
            Bitmap filteredImg = filter.Apply(img);

            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinHeight = 20;
            blobCounter.MaxHeight = 100;
            blobCounter.FilterBlobs = true;
            blobCounter.ProcessImage(filteredImg);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            Graphics g = Graphics.FromImage(filteredImg);
            Pen redPen = new Pen(Color.Red, 2);
            SimpleShapeChecker s = new SimpleShapeChecker();
            List<Blob> cirkels = new List<Blob>();
            BlobCountingObjectsProcessing bcop = new BlobCountingObjectsProcessing();

            for (int n = 0; n < blobs.Length; n++)
            {
                Blob b = blobs[n];
                List<IntPoint> edgePoint = blobCounter.GetBlobsEdgePoints(b);
                float radius = b.Rectangle.Width / 2;
                if (s.IsCircle(edgePoint))
                {
                    cirkels.Add(b);
                    g.DrawEllipse(redPen,
                    (int)(b.Rectangle.Location.X),
                    (int)(b.Rectangle.Location.Y),
                    (int)(radius * 2),
                    (int)(radius * 2));

                }
            }
            System.Drawing.Point[] points = new System.Drawing.Point[cirkels.Count];
            for (int k = 0; k < points.Length; k++)
            {
                Blob b = cirkels.ElementAt(k);
                points[k] = new System.Drawing.Point((int)b.CenterOfGravity.X, (int)b.CenterOfGravity.Y);
            }
            List<System.Drawing.Point[]> triangels = getTriangels(points);
            triangels = filterTriangels(triangels, 1.47, 0.05);
            for (int k = 0; k < triangels.Count; k++)
            {
                System.Drawing.Point[] triangelPoints = triangels.ElementAt(k);
                g.DrawLines(redPen, triangelPoints);
                g.DrawLine(redPen, triangelPoints.Last(), triangelPoints.First());
            }
            return filteredImg;
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
        //Filter out triangels with a the right proportions.
        List<System.Drawing.Point[]> filterTriangels(List<System.Drawing.Point[]> triangels, double propotion, double error)
        {
            List<System.Drawing.Point[]> passedTriangels = new List<System.Drawing.Point[]>();
            for (int k = 0; k < triangels.Count; k++)
            {
                double xDist = triangels.ElementAt(k)[0].X - triangels.ElementAt(k)[1].X;
                double yDist = triangels.ElementAt(k)[0].Y - triangels.ElementAt(k)[1].Y;
                double d1 = Math.Sqrt(xDist * xDist + yDist * yDist);
                xDist = triangels.ElementAt(k)[1].X - triangels.ElementAt(k)[2].X;
                yDist = triangels.ElementAt(k)[1].Y - triangels.ElementAt(k)[2].Y;
                double d2 = Math.Sqrt(xDist * xDist + yDist * yDist);
                xDist = triangels.ElementAt(k)[0].X - triangels.ElementAt(k)[2].X;
                yDist = triangels.ElementAt(k)[0].Y - triangels.ElementAt(k)[2].Y;
                double d3 = Math.Sqrt(xDist * xDist + yDist * yDist);


                double triangelBase;
                double triangelHight;
                if (d1 < d2 && d1 < d3)
                {
                    triangelBase = d1;
                    triangelHight = Math.Sqrt(d2 * d2 - (d1 / 2) * (d1 / 2));
                }
                else if (d2 < d1 && d2 < d3)
                {
                    triangelBase = d2;
                    triangelHight = Math.Sqrt(d3 * d3 - (d2 / 2) * (d2 / 2));
                }
                else
                {
                    triangelBase = d3;
                    triangelHight = Math.Sqrt(-d1 * d1 - (d3 / 2) * (d3 / 2));
                }

                double triangelPropotion = triangelHight / triangelBase;
                System.Console.Out.WriteLine(triangelPropotion);
                if (triangelPropotion > (propotion - error) && triangelPropotion < propotion + error)
                {
                    passedTriangels.Add(triangels.ElementAt(k));
                }
            }

            return passedTriangels;
        }
        
    }
}
