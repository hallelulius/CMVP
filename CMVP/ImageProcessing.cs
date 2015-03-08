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
       // private Bitmap filteredImg;
        private List<AForge.Point> Acenters;
        private List<AForge.DoublePoint> Adirections;
        private List<Panel> panelsToUpdate;
        private Timer imgProcesTimer;
        private int tempTime;
        private Graphics g;

        public Boolean drawCirkelsOnImg;
        public Boolean drawDirectionOnImg;
        public Boolean drawTriangleOnImg;
        public Boolean drawCenterOnImg;
        

        public ImageProcessing(VideoStream videoStream,List<Car> objects)
        {
            System.Console.WriteLine("CreatImageProcessingClass");
            this.imgProcesTimer = new Timer();
            this.imgProcesTimer.Interval=1;
            this.imgProcesTimer.Tick += new EventHandler(updatePanels);
            this.objects = objects;
            this.panelsToUpdate = new List<Panel>();
            this.videoStream = videoStream;
            this.tempTime=0;

            this.drawCirkelsOnImg = false;
            this.drawCenterOnImg = false;
            this.drawTriangleOnImg = false;
            this.drawDirectionOnImg = false;

        }
        void updatePanels(object sender, EventArgs e)
        {
            img = videoStream.getImage();
            processedImage = processImage();
            foreach (Panel panel in panelsToUpdate)
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
            List<AForge.Point> centers = new List<AForge.Point>();
            List<DoublePoint> direktions = new List<DoublePoint>();
            Console.WriteLine("Start: "+System.DateTime.Now.Millisecond);

            this.g = Graphics.FromImage(img);

            Console.WriteLine("Before circular Blobs: " + System.DateTime.Now.Millisecond);
            List<Blob> cirkels = getCircularBlobs(1, 13);
            Console.WriteLine("Before rectangular blobs: " + System.DateTime.Now.Millisecond);
            List<Blob> rectangles = getRectangularBlobs(5, 14, 5, 14);
            Console.WriteLine("after Blobs: " + System.DateTime.Now.Millisecond);
            List<System.Drawing.Point> points = getPoints(cirkels);
            List<System.Drawing.Point[]> triangles = getTriangels(points);


           Console.WriteLine("Before dubblets: " + System.DateTime.Now.Millisecond);

            triangles = filterDubblets(triangles);
            
            Console.WriteLine("Before wrong triangles: " + System.DateTime.Now.Millisecond);
            
            foreach(System.Drawing.Point[] triangle in triangles)
            {
                AForge.Point Acenter;
                AForge.DoublePoint Adirektion;
                AForge.Point[] Atriangle = new AForge.Point[3];

                Atriangle[0] = new AForge.Point(triangle[0].X,triangle[0].Y);
                Atriangle[1] = new AForge.Point(triangle[1].X, triangle[1].Y);
                Atriangle[2] = new AForge.Point(triangle[2].X, triangle[2].Y);

                if (getInformationFromTriangle(Atriangle, 44, 13, 7, 3, out Acenter, out Adirektion))
                {
                    centers.Add(Acenter);
                    direktions.Add(Adirektion);
                }
            }
            for (int k = 0; k < centers.Count; k++)
            {
                Console.WriteLine(k);
                int id = getId(centers[k],direktions[k], rectangles);
                Console.WriteLine("ID: " + id);

                //Draw Graphics
                if (drawCenterOnImg)
                {
                    g.DrawEllipse(bluePen, new Rectangle((int)centers[k].X - 2,(int)centers[k].Y - 2, 2, 2));
                }
                if (drawDirectionOnImg)
                {
                    g.DrawLine(yellowPen, new System.Drawing.Point((int)centers[k].X, (int) centers[k].Y), new System.Drawing.Point((int)(centers[k].X + direktions[k].X * 40), (int)(centers[k].Y + direktions[k].Y * 40)));
                }
            
            }

            if(drawCirkelsOnImg)
                drawCirkels(cirkels);
            return img;
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
            blobCounter.BackgroundThreshold = new RGB(200,200,200).Color;
            blobCounter.MinHeight = minRadius;
            blobCounter.MaxHeight = maxRadius;
            blobCounter.FilterBlobs = true;
            blobCounter.ProcessImage(img);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            SimpleShapeChecker s = new SimpleShapeChecker();
            s.MinAcceptableDistortion = 1;
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
        bool getInformationFromTriangle(AForge.Point[] points,double idealHeight, double idealBase, double heightError, double baseError, out AForge.Point center, out AForge.DoublePoint direction)
        {
            double d0 = points[1].DistanceTo(points[2]);
            double d1 = points[2].DistanceTo(points[0]);
            double d2 = points[1].DistanceTo(points[0]);


            AForge.DoublePoint top, base1, base2;

            if(d0 < d1 && d0 < d2)
            {
                //Console.WriteLine("d0 base");
                top=points[0];
                base1=points[1];
                base2=points[2];


            }
            else if (d1 < d0 && d1 < d2)
            {
                //Console.WriteLine("d1 base");
                top = points[1];
                base1 = points[0];
                base2 = points[2];
            }
            else
            {
                //Console.WriteLine("d2 base");
                top = points[2];
                base1 = points[1];
                base2 = points[0];
            }
            double baseLength = base1.DistanceTo(base2);
            double height = Math.Sqrt(top.DistanceTo(base1) * top.DistanceTo(base1) - (baseLength / 2) * (baseLength / 2));
            System.Console.WriteLine("TriangleBase: " + baseLength + " Ideal: " + idealBase);
            System.Console.WriteLine("TriangleHight: " + height + "Ideal: " + idealHeight);
            if (height > (idealHeight - heightError) && height < idealHeight + heightError)
            {
                if (baseLength > (idealBase - baseError) && baseLength < idealBase + baseError)
                {

                    center = new AForge.Point((int)(top.X / 2 + (base1.X + base2.X) / 4),(int)(top.Y / 2 + (base1.Y + base2.Y) / 4));
                    direction = new AForge.DoublePoint(top.X - center.X, top.Y - center.Y);
                    direction = new AForge.DoublePoint(direction.X / direction.EuclideanNorm(), direction.Y / direction.EuclideanNorm());
                    return true;
                }
            }
            center = new AForge.Point(0, 0);
            direction = new AForge.DoublePoint(0, 0);
            return false;
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
        private int getId(AForge.Point center, AForge.DoublePoint direction, List<Blob> rectangles)
        {
            List<Blob> idRectangles = filterOutIdRectangles(rectangles,center);
            return idRectangles.Count;
        }
        private List<Blob> getRectangularBlobs(int minWidth, int maxWidth, int minHight, int maxHight)
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.BackgroundThreshold = new RGB(200, 200, 200).Color;
            blobCounter.MaxHeight = maxHight;
            blobCounter.MinHeight = minHight;
            blobCounter.MaxWidth = maxWidth;
            blobCounter.MinWidth = minWidth;
            blobCounter.FilterBlobs = true;
            blobCounter.ProcessImage(img);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
            shapeChecker.MinAcceptableDistortion = 5f;
            List<Blob> rectangles = new List<Blob>();
            foreach(Blob b in blobs)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(b);

                if(edgePoints.Count>1 && shapeChecker.IsQuadrilateral(edgePoints))
                {
                    rectangles.Add(b);
                }
            }
            return rectangles;

        }
        private List<Blob> filterOutIdRectangles(List<Blob> rectangles, AForge.Point p)
        {
            List<Blob> idTag = new List<Blob>();
            foreach(Blob b in rectangles)
            {
                if(b.CenterOfGravity.DistanceTo(p)<14)
                {
                    idTag.Add(b);
                }
            }
            return idTag;
        }
    }
}
