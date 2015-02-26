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
        static private Pen redPen=new Pen(Color.Red, 2);
        static private Pen bluePen = new Pen(Color.LightSkyBlue,2);
        static private Pen greenPen = new Pen(Color.Green, 2);
        static private Pen yellowPen = new Pen(Color.Yellow, 2);


        List<Car> objects;
        private VideoStream videoStream;
        private Bitmap img;
        private Bitmap processedImage;
        //Temporary variable until the physical filter is in use.
        private Bitmap filteredImg;
        private List<System.Drawing.Point> centers;
        private List<System.Drawing.PointF> directions;
        private List<Panel> panelsToUpdate;
        private Timer imgProcesTimer;
        private int tempTime;
        private Graphics g;

        public Boolean drawCirkelsOnImg;
        

        public ImageProcessing(VideoStream videoStream)
        {
            System.Console.WriteLine("CreatImageProcessingClass");
            this.imgProcesTimer = new Timer();
            this.imgProcesTimer.Interval=1;
            this.imgProcesTimer.Tick += new EventHandler(updatePanels);
            this.objects = new List<Car>();
            this.panelsToUpdate = new List<Panel>();
            this.videoStream = videoStream;
            this.tempTime=0;

            this.drawCirkelsOnImg = true;

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
            Console.WriteLine("Start: "+System.DateTime.Now.Millisecond);

            YCbCrFiltering filter = new YCbCrFiltering();
            filter.Y = new Range(0.9f, 1);
            filteredImg = filter.Apply(img);

            this.g = Graphics.FromImage(filteredImg);
            Console.WriteLine("BW filter: " + System.DateTime.Now.Millisecond);

            Console.WriteLine("Before Blobs: " + System.DateTime.Now.Millisecond);
            List<Blob> cirkels = getCircularBlobs(5, 10);

            Console.WriteLine("Before after Blobs: " + System.DateTime.Now.Millisecond);
            List<System.Drawing.Point> points = getPoints(cirkels);
            List<System.Drawing.Point[]> triangles = getTriangels(points);


            Console.WriteLine("Before dubblets: " + System.DateTime.Now.Millisecond);

            triangles = filterDubblets(triangles);

            Console.WriteLine("Before wrong triangles: " + System.DateTime.Now.Millisecond);
            
            triangles = filterTriangels(triangles, 19, 15, 2, 2,true);

            Console.WriteLine("Before centers: " + System.DateTime.Now.Millisecond);

            centers = getCenterOfTriangels(triangles);

            Console.WriteLine("Before Direction: " + System.DateTime.Now.Millisecond);

            directions=getDirectionOfTriangels(triangles);

            for (int k = 0; k < triangles.Count; k++)
            {
                Console.WriteLine(k);
                System.Drawing.Point[] triangelPoints = triangles.ElementAt(k);
                g.DrawLines(greenPen, triangelPoints);
                g.DrawLine(greenPen, triangelPoints.Last(), triangelPoints.First());
                g.DrawEllipse(bluePen, new Rectangle(centers[k].X - 2, centers[k].Y - 2, 2, 2));
                g.DrawLine(yellowPen,centers[k],new System.Drawing.Point((int)(centers[k].X+directions[k].X*40),(int)(centers[k].Y+directions[k].Y*40)));
                //objects[k].setFound(true);
            }
            return filteredImg;
        }
        private void drawCirkels(List<Blob> cirkels)
        {
            foreach(Blob cirkel in cirkels)
            {
                g.DrawEllipse(redPen, cirkel.Rectangle);
            }
        }
        private List<Blob> getCircularBlobs(int minRadius, int maxRadius)
        {

            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinHeight = minRadius;
            blobCounter.MaxHeight = maxRadius;
            blobCounter.FilterBlobs = true;
            blobCounter.ProcessImage(filteredImg);
            Blob[] blobs = blobCounter.GetObjectsInformation();

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
                }
            }
            if (drawCirkelsOnImg)
                drawCirkels(cirkels);
            return cirkels;
        }
        private List<System.Drawing.Point> getPoints(List<Blob> blobs)
        {
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();

            foreach(Blob b in blobs)
            {
                points.Add(new System.Drawing.Point((int)b.CenterOfGravity.X,(int)b.CenterOfGravity.Y));
            }
            return points;
        }

        //function to get triangels in se
        private List<System.Drawing.Point[]> getTriangels(List<System.Drawing.Point> points)
        {
            List<System.Drawing.Point[]> pointList = new List<System.Drawing.Point[]>();
            foreach(System.Drawing.Point a in points)
            {
                foreach(System.Drawing.Point b in points)
                {
                    foreach(System.Drawing.Point c in points)
                    {
                        if (a != b && a != c && b != c)
                        {
                            //We need to check a that there doesn't excist any duplicates
                            System.Drawing.Point[] locations = new System.Drawing.Point[3];

                            locations[0] = a;
                            locations[1] = b;
                            locations[2] = c;

                            pointList.Add(locations);
                        }
                    }
                }
            }
            return pointList;
        }
        private List<System.Drawing.Point> getCenterOfTriangels(List<System.Drawing.Point[]> triangels)
        {
            List<System.Drawing.Point> centers = new List<System.Drawing.Point>();
            foreach (System.Drawing.Point[] triangle in triangels)
            {
                int[] x = new int[3] { triangle[0].X, triangle[1].X, triangle[2].X };
                int[] y = new int[3] { triangle[0].Y, triangle[1].Y, triangle[2].Y };

                double d0 = Math.Sqrt((x[0] - x[1]) * (x[0] - x[1]) + (y[0] - y[1]) * (y[0] - y[1]));
                double d1 = Math.Sqrt((x[0] - x[2]) * (x[0] - x[2]) + (y[0] - y[2]) * (y[0] - y[2]));
                double d2 = Math.Sqrt((x[1] - x[2]) * (x[1] - x[2]) + (y[1] - y[2]) * (y[1] - y[2]));

                System.Drawing.Point top, base1, base2;

                if (d0 < d1 && d0 < d2)
                {
                    top = triangle[2];
                    base1 = triangle[0];
                    base2 = triangle[1];
                }
                if (d1 < d0 && d1 < d2)
                {
                    top = triangle[1];
                    base1 = triangle[0];
                    base2 = triangle[2];
                }
                else
                {
                    top = triangle[2];
                    base1 = triangle[1];
                    base2 = triangle[0];
                }
                System.Drawing.Point center = new System.Drawing.Point(top.X / 2 + (base1.X + base2.X) / 4, top.Y / 2 + (base1.Y + base2.Y) / 4);
                centers.Add(center);
            }
            return centers;
        }
        private List<System.Drawing.PointF> getDirectionOfTriangels(List<System.Drawing.Point[]>triangels)
        {
            List<System.Drawing.PointF> directions = new List<System.Drawing.PointF>();
            foreach(System.Drawing.Point[] triangle in triangels)
            {
                int[] x = new int[3] { triangle[0].X, triangle[1].X, triangle[2].X };
                int[] y = new int[3] { triangle[0].Y, triangle[1].Y, triangle[2].Y };

                double d0 = Math.Sqrt((x[0] - x[1]) * (x[0] - x[1]) + (y[0] - y[1]) * (y[0] - y[1]));
                double d1 = Math.Sqrt((x[0] - x[2]) * (x[0] - x[2]) + (y[0] - y[2]) * (y[0] - y[2]));
                double d2 = Math.Sqrt((x[1] - x[2]) * (x[1] - x[2]) + (y[1] - y[2]) * (y[1] - y[2]));

                System.Drawing.Point top, base1, base2;

                if(d0 < d1 && d0 < d2)
                {
                    Console.WriteLine("d0 base");
                    top=triangle[2];
                    base1=triangle[0];
                    base2=triangle[1];
                }
                else if(d1 < d0 && d1 < d2)
                {
                    Console.WriteLine("d1 base");
                    top = triangle[1];
                    base1 = triangle[0];
                    base2 = triangle[2];
                }
                else
                {
                    Console.WriteLine("d2 base");
                    top = triangle[0];
                    base1 = triangle[1];
                    base2 = triangle[2];
                }
                System.Drawing.Point center = new System.Drawing.Point(top.X / 2 + (base1.X + base2.X) / 4, top.Y / 2 + (base1.Y + base2.Y) / 4);
                System.Drawing.PointF direction = new System.Drawing.PointF(top.X-center.X,top.Y-center.Y);
                float directionDistance = (float) Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
                System.Drawing.PointF NormedDirection = new System.Drawing.PointF(direction.X/directionDistance,direction.Y/directionDistance);
                directions.Add(NormedDirection);
                //directions.Add(Vector3.Divide(direction,direction.Norm));
            }
            return directions;
        }
        //Filter out triangels with a the right proportions.
        List<System.Drawing.Point[]> filterTriangels(List<System.Drawing.Point[]> triangels, double propotion, double pError, double angle,double aError)
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
                

                //System.Console.Out.WriteLine(triangelPropotion);
                if (triangelPropotion > (propotion - pError) && triangelPropotion < propotion + pError)
                {
                    double triangleAngle = System.Math.Atan(triangelHight / triangelBase/2);
                    if (triangleAngle > (angle - aError) && triangleAngle < angle + aError)
                    passedTriangels.Add(triangels.ElementAt(k));
                }
            }
            return passedTriangels;
        }
        List<System.Drawing.Point[]> filterTriangels(List<System.Drawing.Point[]> triangles, double idealHight, double idealBase, double errorHight, double errorBase, Boolean b)
        {
            List<System.Drawing.Point[]> passedTriangels = new List<System.Drawing.Point[]>();

            for (int k = 0; k < triangles.Count; k++)
            {

                double xDist = triangles.ElementAt(k)[0].X - triangles.ElementAt(k)[1].X;
                double yDist = triangles.ElementAt(k)[0].Y - triangles.ElementAt(k)[1].Y;
                double d1 = Math.Sqrt(xDist * xDist + yDist * yDist);
                xDist = triangles.ElementAt(k)[1].X - triangles.ElementAt(k)[2].X;
                yDist = triangles.ElementAt(k)[1].Y - triangles.ElementAt(k)[2].Y;
                double d2 = Math.Sqrt(xDist * xDist + yDist * yDist);
                xDist = triangles.ElementAt(k)[0].X - triangles.ElementAt(k)[2].X;
                yDist = triangles.ElementAt(k)[0].Y - triangles.ElementAt(k)[2].Y;
                double d3 = Math.Sqrt(xDist * xDist + yDist * yDist);


                double triangleBase;
                double triangleHight;
                if (d1 < d2 && d1 < d3)
                {
                    triangleBase = d1;
                    triangleHight = Math.Sqrt(d2 * d2 - (d1 / 2) * (d1 / 2));
                }
                else if (d2 < d1 && d2 < d3)
                {
                    triangleBase = d2;
                    triangleHight = Math.Sqrt(d3 * d3 - (d2 / 2) * (d2 / 2));
                }
                else
                {
                    triangleBase = d3;
                    triangleHight = Math.Sqrt(d1 * d1 - (d3 / 2) * (d3 / 2));
                }
                System.Console.WriteLine("TriangleBase: " + triangleBase + " Ideal: " + idealBase);
                System.Console.WriteLine("TriangleHight: " + triangleHight + "Ideal: " + idealHight);
                if (triangleHight > (idealHight - errorHight) && triangleHight < idealHight + errorHight)
                {
                    if (triangleBase > (idealBase - errorBase) && triangleBase < idealBase + errorBase)
                        passedTriangels.Add(triangles.ElementAt(k));
                }

            }
            return passedTriangels;
        }
        private List<System.Drawing.Point[]> filterDubblets(List<System.Drawing.Point[]> triangles)
        {
            List<System.Drawing.Point[]> filteredTriangles = new List<System.Drawing.Point[]>();

            foreach(System.Drawing.Point[] triangle in triangles)
            {
                Array.Sort(triangle,
                    delegate(System.Drawing.Point p1, System.Drawing.Point p2)
                    {
                        if(p1.Y==p2.Y)
                            return p1.X - p2.X;
                        else
                            return p1.Y-p2.Y;
                    }
                );
            }
            if(triangles.Count>0)
                filteredTriangles.Add(triangles[0]);
            foreach(System.Drawing.Point[] t1 in triangles)
            {
                Boolean add = true;
                foreach(System.Drawing.Point[] t2 in filteredTriangles)
                {
                    if (t1[0].Equals(t2[0]) && t1[1].Equals(t2[1]) && t1[2].Equals(t2[2]))
                        add = false;
                        
                }
                if(add)
                {
                    filteredTriangles.Add(t1);
                }
            }

            return filteredTriangles;
        }
    }
}
