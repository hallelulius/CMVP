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
        //Temporary variable until the physical filter is in use.
        private Bitmap filteredImg;
        private List<System.Drawing.Point> centers;
        private List<Vector3> directions;
        private List<Panel> panelsToUpdate;
        private Timer imgProcesTimer;

        



        public ImageProcessing(VideoStream videoStream)
        {
            System.Console.WriteLine("CreatImageProcessingClass");
            this.imgProcesTimer = new Timer();
            this.imgProcesTimer.Interval=20;
            this.imgProcesTimer.Tick += new EventHandler(updatePanels);
            this.objects = new List<Car>();
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
            filteredImg = filter.Apply(img);

            Graphics g = Graphics.FromImage(filteredImg);
            Pen redPen = new Pen(Color.Red, 2);
            List<Blob> cirkels = getCircularBlobs(20, 100);
            List<System.Drawing.Point> points = getPoints(cirkels);
            List<System.Drawing.Point[]> triangles = getTriangels(points);
            triangles = filterTriangels(triangles, 1.47, 0.05, 0.974, 0.4);
            if (triangles.Count > 0) 
                Console.WriteLine(triangles.Count);
            centers = getCenterOfTriangels(triangles);
            directions=getDirectionOfTriangels(triangles);

            for (int k = 0; k < triangles.Count; k++)
            {
                System.Drawing.Point[] triangelPoints = triangles.ElementAt(k);
                g.DrawLines(redPen, triangelPoints);
                g.DrawLine(redPen, triangelPoints.Last(), triangelPoints.First());
                g.DrawEllipse(redPen, new Rectangle(centers[k].X - 2, centers[k].Y - 2, 4, 4));
                g.DrawLine(redPen,centers[k],new System.Drawing.Point((int)(centers[k].X+directions[k].X),(int)(centers[k].Y+directions[k].Y)));
                //objects[k].setFound(true);
            }
            return filteredImg;
        }
        private List<Blob> getCircularBlobs(int MinRadius, int maxRadius)
        {

            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinHeight = 20;
            blobCounter.MaxHeight = 100;
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
            foreach(System.Drawing.Point[] triangel in triangels)
            {
                //Makes it easier to read. Extrakting X and Y Values of the points to create two vectors.
                int[] x= new int[3]{triangel[0].X,triangel[1].X,triangel[2].X};
                int[] y= new int[3]{triangel[0].Y,triangel[1].Y,triangel[2].Y};

                centers.Add(new System.Drawing.Point((int)x.Average(),(int)y.Average()));
            }
            return centers;
        }
        private List<Vector3> getDirectionOfTriangels(List<System.Drawing.Point[]>triangels)
        {
            List<Vector3> directions = new List<Vector3>();
            foreach(System.Drawing.Point[] triangle in triangels)
            {
                int[] x = new int[3] { triangle[0].X, triangle[1].X, triangle[2].X };
                int[] y = new int[3] { triangle[0].Y, triangle[1].Y, triangle[2].Y };

                Vector3 v1 = new Vector3(x[0] - x[1], y[0] - y[1],0);
                Vector3 v2 = new Vector3(x[0] - x[2], y[0] - y[2],0);
                Vector3 v3 = new Vector3(x[2] - x[1], y[2] - y[1],0);

                Vector3 triangleLeg, triangleBase;
                System.Drawing.Point baseCorner;


                if(v1.Square<v2.Square && v1.Square < v3.Square)
                {
                    triangleBase = v1;
                    triangleLeg = v2;
                    baseCorner= new System.Drawing.Point(x[0],y[0]);
                }
                else if(v2.Square<v1.Square && v2.Square < v3.Square)
                {
                    triangleBase = v2;
                    triangleLeg = v1;
                    baseCorner= new System.Drawing.Point(x[0],y[0]);
                }
                else
                {
                    triangleBase = v3.Inverse();
                    triangleLeg = v2;
                    baseCorner= new System.Drawing.Point(x[0],y[0]);
                }
                System.Drawing.Point center = new System.Drawing.Point((int)x.Average(),(int)y.Average());
                Vector3 triangelBase,triangelLeg;

                System.Drawing.Point triangleSharpPoint = new System.Drawing.Point((int)(center.X + triangleLeg.X), (int)(center.Y + triangleLeg.Y));
                Vector3 direction = new Vector3(center.X - triangleSharpPoint.X, center.Y- triangleSharpPoint.Y,0);
                directions.Add(direction);
                //directions.Add(Vector3.Divide(direction,direction.Norm));
            }
            return directions;
        }
        //Filter out triangels with a the right proportions.
        List<System.Drawing.Point[]> filterTriangels(List<System.Drawing.Point[]> triangels, double propotion, double pError, double angle,double aError)
        {
            List<System.Drawing.Point[]> passedTriangels = new List<System.Drawing.Point[]>();
            //Getting rid of dubblicates
            List<System.Drawing.Point> centers = getCenterOfTriangels(triangels);
            List<System.Drawing.Point> filteredDubblets; 
     
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
        
    }
}
