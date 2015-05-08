using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Vision.Motion;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;
using System.Threading;



namespace CMVP
{
    class ImageProcessing
    {

        private PTGreyCamera videoStream;
        private Bitmap img;
        private Bitmap croppedImg;
        private object _locker = new object();
        private object _locker2 = new object();

        private byte threshold;

        List<Blob> cirkels;
        List<Car> objects;
        List<Quadrilateral> squares = new List<Quadrilateral>();
        Dictionary<Car, Triangle> prevTriangles = new Dictionary<Car, Triangle>();

        //variables used for calculating time difference between updates
        private double deltaTime;
        private double prevTime;

        //to ensure proper workingflow
        private bool working = true;
        private AutoResetEvent whCamera;
        private AutoResetEvent whTime;
        private AutoResetEvent whBrain; public AutoResetEvent BRAIN_EVENT_HANDLE { get { return whBrain; } }
        private ManualResetEventSlim whDataUsed; public ManualResetEventSlim DATA_USED { get { return whDataUsed; } }


        //sets ideal triangle base and height
        static private double idealHeight = 35; // 44 on table 35 on floor
        static private double idealBase = 12;  //  18 on table 12 on the floor.

        //static private double heightError = 4;
        //static private double baseError = 4;
        static private int blobMin = 2;
        static private int blobMax = 6;
        static private Triangle idealTriangle = new Triangle(idealHeight, idealBase);
        static private double worstAccepted = 0;


        public ImageProcessing(PTGreyCamera videoStream, List<Car> objects)
        {

            this.objects = objects;
            this.videoStream = videoStream;
            this.threshold = 180;
            this.cirkels = new List<Blob>();
            System.Console.WriteLine("Image processing OK");

        }

        public void start()
        {
            working = true;
            whCamera = videoStream.NEW_IMG_AVAILABLE;
            whTime = videoStream.TIME_MEASURED;
            whBrain = new AutoResetEvent(false);
            whDataUsed = new ManualResetEventSlim(true);
            prevTime = videoStream.getTime();
            Thread thread = new Thread(run);
            thread.Name = "Image Processing";
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start();

        }
        public void initiate()
        {

            img = videoStream.getImage();
            List<Blob> cirkels = getBlobs(blobMin, blobMax, img);
            List<AForge.IntPoint> points = getPoints(cirkels);

            initiateCars(points);
            initiateBlocks(points);

            List<int> intList = new List<int>();
            foreach (Car car in objects)
            {
                intList.Add(car.ID);
            }
            MessageBox.Show("The following cars where found: " + String.Join(",", intList.ToArray()) + " \n " + Program.obstacle.Count + " obstacles was found");
        }
        private void initiateCars(List<AForge.IntPoint> points)
        {
            objects.Clear();
            List<Triangle> triangles = getTriangles(points);
            triangles = filterTriangleDubblets(triangles);
            List<Triangle> carTriangles = new List<Triangle>();
            foreach (Triangle triangle in triangles)
            {
                if (triangle.compare(idealTriangle))
                {
                    carTriangles.Add(triangle);
                }
            }
            foreach (Triangle triangle in carTriangles)
            {
                List<AForge.IntPoint> idPoints = getIdPoints(triangle, points);

                int triangleId = idPoints.Count;
                if (triangleId != 0)
                {
                    //Remove used points
                    foreach (AForge.IntPoint p in triangle.getPoints())
                        points.Remove(p);
                    foreach (AForge.IntPoint p in idPoints)
                        points.Remove(p);
                    Console.WriteLine("ID: " + triangleId);
                    //Size need to be calculated implement later.
                    Car car = new Car(triangleId, triangle.CENTER, triangle.DIRECTION, 50);
                    objects.Add(car);
                    prevTriangles.Add(car, triangle);
                }
            }
        }
        private void initiateBlocks(List<AForge.IntPoint> points)
        {
            List<Quadrilateral> tempSquares = getQuadrilaterals(points);
            tempSquares = filterQuadrilateralDubblets(tempSquares);
            squares = new List<Quadrilateral>();
            foreach (Quadrilateral q in tempSquares)
            {
                if (q.square(0.07))
                {
                    squares.Add(q);
                    Program.obstacle.Add(new Item(q.CENTER, (int)Math.Round(q.SIZE)));
                }
            }
        }

        private void run()
        {
            while (true)
            {
                if (working)
                {
                    whCamera.WaitOne();
                    processImage();
                    whBrain.Set();
                }
                

            }
        }
        private void processImage()
        {
            double tempTime = videoStream.getTime();
            deltaTime = tempTime - prevTime;
            prevTime = tempTime;
            img = videoStream.getImage();
            whTime.Set();
            foreach (Car car in objects)
            {
                AForge.IntPoint pos = car.getPosition();
                //bör ta hänsyn till riktningen för minimera fönstret
                Size windowSize = new Size(200, 200);

                int cropX, cropY;
                if (car.found)
                {
                    cropX = pos.X - windowSize.Width / 2;
                    cropY = pos.Y - windowSize.Height / 2;
                    if (cropX < 0)
                        cropX = 0;
                    else if (cropX > img.Width - windowSize.Width)
                        cropX = img.Width - windowSize.Width;
                    if (cropY < 0)
                        cropY = 0;
                    else if (cropY > img.Height - windowSize.Height)
                        cropY = img.Height - windowSize.Height;
                    croppedImg = img.Clone(new Rectangle(cropX, cropY, windowSize.Width, windowSize.Height), img.PixelFormat);
                }
                else
                {
                    cropX = 0;
                    cropY = 0;
                    croppedImg = img;
                }

                List<Blob> cirkels = getBlobs(blobMin, blobMax, croppedImg);
                List<AForge.IntPoint> points = getPoints(cirkels);

                points = filterPointsThatBelongsToOtherCars(points, car);

                List<Triangle> triangles = getTriangles(points);
                triangles = filterTriangleDubblets(triangles);

                Triangle prevTriangle = null;
                prevTriangles.TryGetValue(car, out prevTriangle);

                triangles.Sort(delegate(Triangle t1, Triangle t2)
                {

                    return (t1.compareTo(prevTriangle).CompareTo(t2.compareTo(prevTriangle)));
                });
                List<double> d = new List<double>();
                List<int> i = new List<int>();
                foreach (Triangle t in triangles)
                {
                    d.Add(t.compareTo(prevTriangle) + t.compareTo(idealTriangle));
                    i.Add(getIdPoints(t, points).Count);
                }
                bool carFoundThisTime = false;
                whDataUsed.Wait(8); //ensure data is passed to the cars
                foreach (Triangle triangle in triangles)
                {
                    //Unknown if comparing to idealTriangle is nescesarry.
                    if (triangle.compareTo(prevTriangle) + triangle.compareTo(idealTriangle) < 200000)
                    {
                        AForge.IntPoint translation = new AForge.IntPoint(cropX, cropY);
                        List<AForge.IntPoint> idPoints = getIdPoints(triangle, points);
                        int triangleId = idPoints.Count;
                        if (car.ID == triangleId)
                        {
                            if (worstAccepted < d[1])
                                worstAccepted = d[1];
                            //Remove used points
                            foreach (AForge.IntPoint p in triangle.getPoints())
                                points.Remove(p);
                            foreach (AForge.IntPoint p in idPoints)
                                points.Remove(p);
                            car.setPositionAndOrientation(triangle.CENTER + translation, triangle.DIRECTION, deltaTime);
                            car.found = true;
                            carFoundThisTime = true;
                            prevTriangles.Remove(car);
                            triangle.offset(translation);
                            prevTriangles.Add(car, triangle);
                            break;
                        }
                    }
                }
                if (!carFoundThisTime)
                    car.found = false;
            }
        }

        private Boolean pointInTriangle(AForge.IntPoint p, Triangle t, double errorMargin)
        {
            Quadrilateral boundarySquare = t.getRectangle();
            AForge.IntPoint[] boundary = boundarySquare.CORNERS;
            double bArea = boundarySquare.getArea();

            Triangle t1 = new Triangle(new AForge.IntPoint[] { p, boundary[0], boundary[1] });
            Triangle t2 = new Triangle(new AForge.IntPoint[] { p, boundary[1], boundary[2] });
            Triangle t3 = new Triangle(new AForge.IntPoint[] { p, boundary[2], boundary[3] });
            Triangle t4 = new Triangle(new AForge.IntPoint[] { p, boundary[3], boundary[0] });

            double totalArea = t1.getArea() + t2.getArea() + t3.getArea() + t4.getArea();

            double tempDiff = totalArea - bArea;
            double diff = Math.Abs(tempDiff);

            double error = diff / bArea;
            return error < errorMargin;
        }
        private List<AForge.IntPoint> filterPointsThatBelongsToOtherCars(List<AForge.IntPoint> points, Car car)
        {
            List<AForge.IntPoint> filteredPoints = new List<AForge.IntPoint>(points);
            foreach (Car c in objects)
            {
                if (c != car)
                {
                    Triangle carTriangle;
                    if (prevTriangles.TryGetValue(c, out carTriangle))
                    {
                        foreach (AForge.IntPoint p in points)
                        {
                            if (pointInTriangle(p, carTriangle, 0.01))
                                filteredPoints.Remove(p);
                        }
                    }
                }

            }
            return filteredPoints;
        }
        private List<AForge.IntPoint> getPoints(List<Blob> blobs)
        {
            List<AForge.IntPoint> points = new List<AForge.IntPoint>();

            foreach (Blob b in blobs)
            {
                points.Add(b.CenterOfGravity.Round());
            }
            return points;
        }
        private List<Triangle> getTriangles(List<AForge.IntPoint> points)
        {
            List<Triangle> triangles = new List<Triangle>();
            foreach (AForge.IntPoint a in points)
            {
                foreach (AForge.IntPoint b in points)
                {
                    foreach (AForge.IntPoint c in points)
                    {
                        if (a != b && a != c && b != c)
                        {
                            AForge.IntPoint[] locations = new AForge.IntPoint[3];

                            locations[0] = a;
                            locations[1] = b;
                            locations[2] = c;

                            triangles.Add(new Triangle(locations));
                        }
                    }
                }
            }
            return triangles;
        }
        private List<Quadrilateral> getQuadrilaterals(List<AForge.IntPoint> points)
        {
            List<Quadrilateral> quadrilaterals = new List<Quadrilateral>();
            foreach (AForge.IntPoint a in points)
            {
                foreach (AForge.IntPoint b in points)
                {
                    foreach (AForge.IntPoint c in points)
                    {
                        foreach (AForge.IntPoint d in points)
                        {
                            if (a != b && a != c && a != d && b != c && b != d && c != d)
                            {
                                AForge.IntPoint[] locations = new AForge.IntPoint[4];

                                locations[0] = a;
                                locations[1] = b;
                                locations[2] = c;
                                locations[3] = d;

                                quadrilaterals.Add(new Quadrilateral(locations));
                            }
                        }
                    }
                }
            }
            return quadrilaterals;
        }
        private List<Triangle> filterTriangleDubblets(List<Triangle> triangles)
        {
            List<Triangle> filteredTriangles = new List<Triangle>();
            if (triangles.Count > 0)
            {
                filteredTriangles.Add(triangles.First());
                foreach (Triangle t in triangles)
                {
                    Boolean add = true;
                    foreach (Triangle ft in filteredTriangles)
                    {
                        if (t.Equals(ft))
                            add = false;
                    }
                    if (add)
                    {
                        filteredTriangles.Add(t);
                    }
                }
            }
            return filteredTriangles;
        }
        private List<Quadrilateral> filterQuadrilateralDubblets(List<Quadrilateral> quadrilaterals)
        {
            List<Quadrilateral> filteredQuadrilaterals = new List<Quadrilateral>();
            if (quadrilaterals.Count > 0)
            {
                filteredQuadrilaterals.Add(quadrilaterals.First());
                foreach (Quadrilateral t in quadrilaterals)
                {
                    Boolean add = true;
                    foreach (Quadrilateral ft in filteredQuadrilaterals)
                    {
                        if (t.Equals(ft))
                            add = false;
                    }
                    if (add)
                    {
                        filteredQuadrilaterals.Add(t);
                    }
                }
            }
            return filteredQuadrilaterals;
        }
        private List<AForge.IntPoint> getIdPoints(Triangle triangle, List<AForge.IntPoint> points)
        {
            List<AForge.IntPoint> idPoints = new List<AForge.IntPoint>();
            Quadrilateral boundarySquare = triangle.getRectangle();
            if (squares.Count == 0)
                squares.Add(boundarySquare);
            AForge.IntPoint[] boundary = boundarySquare.CORNERS;
            double bArea = boundarySquare.getArea();
            foreach (AForge.IntPoint p in points)
            {

                Triangle t1 = new Triangle(new AForge.IntPoint[] { p, boundary[0], boundary[1] });
                Triangle t2 = new Triangle(new AForge.IntPoint[] { p, boundary[1], boundary[2] });
                Triangle t3 = new Triangle(new AForge.IntPoint[] { p, boundary[2], boundary[3] });
                Triangle t4 = new Triangle(new AForge.IntPoint[] { p, boundary[3], boundary[0] });

                double totalArea = t1.getArea() + t2.getArea() + t3.getArea() + t4.getArea();

                double tempDiff = totalArea - bArea;
                double diff = Math.Abs(tempDiff);

                double error = diff / bArea;
                if (error < 0.4)
                {
                    Boolean add = true;
                    foreach (AForge.IntPoint c in triangle.getPoints())
                    {
                        if (c.Equals(p))
                        {
                            add = false;
                            break;
                        }

                    }
                    if (add)
                        idPoints.Add(p);
                }
            }
            return idPoints;
        }
        public  List<Blob> getBlobs(int minHeight, int maxHeight, Bitmap img)
        {
            lock (_locker)
            {
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.BackgroundThreshold = new RGB(threshold, threshold, threshold).Color;
                blobCounter.MinHeight = minHeight;
                blobCounter.MaxHeight = maxHeight;
                blobCounter.FilterBlobs = true;
                blobCounter.ProcessImage(img);
                Blob[] blobs = blobCounter.GetObjectsInformation();
                return blobs.ToList<Blob>();
            }

        }
        public List<Blob> getBlobsSlow(int minHeight, int maxHeight, Bitmap img)
        {
            lock (_locker)
            {
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.BackgroundThreshold = new RGB(threshold, threshold, threshold).Color;
                blobCounter.MinHeight = minHeight;
                blobCounter.MaxHeight = maxHeight;
                blobCounter.FilterBlobs = true;
                blobCounter.ProcessImage(img);
                Blob[] blobs = blobCounter.GetObjectsInformation();
                return blobs.ToList<Blob>();
            }

        }
        public void stop()
        {
            working = false;
        }


        public Bitmap getImage()
        {
            lock (_locker2)
            {
                return videoStream.getImage();
            }
        }
        public double getTime()
        {
            return videoStream.getTime();
        }
        public byte getThreshold()
        {
            return threshold;
        }
        public void setThreshold(byte val)
        {
            threshold = val;
        }
        /// <summary>
        /// Use to calculate how many pixels there are per meter. Put the measure stick on the track to calculate
        /// Use when  the camera is moved then update PIXEL_SIZE in the car class
        /// </summary>
        private void pixelsPerMeter()
        {
            Bitmap bp = videoStream.getImage();
            List<Blob> blobs = getBlobs(2, 10, bp);
            List<IntPoint> points = getPoints(blobs);
            if (points.Count != 2)
            {
                MessageBox.Show("Make sure that you only have the measuring stick in the picture\n Points found: " + points.Count);

            }
            else
            {
                MessageBox.Show("Pixels in one meter:" + points[0].DistanceTo(points[1]).ToString());
            }


        }
        /* Methods that could be used in the future for insperation.
         * 
         * private List<Blob> filterOutIdRectangles(List<Blob> rectangles, AForge.Point p)
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
         * 
         * 
         * 
         *  private int getId(Triangle triangle, List<AForge.IntPoint> points)
        {
            int count = 0;
            Quadrilateral boundarySquare = triangle.getRectangle();
            if (squares.Count == 0) 
                squares.Add(boundarySquare);
            AForge.IntPoint[] boundary = boundarySquare.CORNERS;
            double bArea = boundarySquare.getArea();
            foreach(AForge.IntPoint p in points)
            {

                Triangle t1 = new Triangle(new AForge.IntPoint[] { p, boundary[0], boundary[1] });
                Triangle t2 = new Triangle(new AForge.IntPoint[] { p, boundary[1], boundary[2] });
                Triangle t3 = new Triangle(new AForge.IntPoint[] { p, boundary[2], boundary[3] });
                Triangle t4 = new Triangle(new AForge.IntPoint[] { p, boundary[3], boundary[0] });

                double totalArea = t1.getArea() + t2.getArea() + t3.getArea() + t4.getArea();

                double tempDiff = totalArea - bArea;
                double diff = Math.Abs(tempDiff);
                
                double error = diff/bArea;
                if (error < 0.03)
                    count++;
            }
            return count;
        }
         * 
         * 
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
         * 
         * 
         * 
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
                //System.Console.WriteLine("TriangleBase: " + triangleBase + " Ideal: " + idealBase);
                //System.Console.WriteLine("TriangleHight: " + triangleHight + "Ideal: " + idealHight);
                if (triangleHight > (idealHight - errorHight) && triangleHight < idealHight + errorHight)
                {
                    if (triangleBase > (idealBase - errorBase) && triangleBase < idealBase + errorBase)
                        passedTriangels.Add(triangles.ElementAt(k));
                }

            }
            return passedTriangels;
        }
         * 
         * 
         * 
         * 
         * 
        private bool getInformationFromTriangle(AForge.IntPoint[] points,double idealHeight, double idealBase, double heightError, double baseError, out AForge.IntPoint center, out AForge.Point direction)
        {
            double d0 = points[1].DistanceTo(points[2]);
            double d1 = points[2].DistanceTo(points[0]);
            double d2 = points[1].DistanceTo(points[0]);


            AForge.IntPoint top, base1, base2;

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
            AForge.IntPoint leg1 = new AForge.IntPoint(top.X - base1.X , top.Y - base1.Y);
            AForge.IntPoint leg2 = new AForge.IntPoint(top.X - base2.X , top.Y - base2.Y);
            AForge.IntPoint baseV = new AForge.IntPoint(base1.X - base2.X, base1.Y - base2.Y);
            double scalarProduct1 = (baseV.X * leg1.X + baseV.Y * leg1.Y) / (baseV.EuclideanNorm() * leg1.EuclideanNorm());
            double baseAngle1 = Math.Acos(scalarProduct1);
            double scalarProduct2 = (baseV.X * leg2.X + baseV.Y * leg2.Y) / (baseV.EuclideanNorm() * leg2.EuclideanNorm());
            double baseAngle2 = Math.Acos(scalarProduct2);
            double baseLength = base1.DistanceTo(base2);
            double height = Math.Sqrt(top.DistanceTo(base1) * top.DistanceTo(base1) - (baseLength / 2) * (baseLength / 2));
            double diffBaseAngle = Math.Abs(baseAngle1-baseAngle2);
            System.Console.WriteLine("TriangleBase: " + baseLength + " Ideal: " + idealBase);
            System.Console.WriteLine("TriangleHight: " + height + "Ideal: " + idealHeight);
            if (height > (idealHeight - heightError) && height < idealHeight + heightError)
            {
                if (baseLength > (idealBase - baseError) && baseLength < idealBase + baseError)
                {
                    if (diffBaseAngle < 0.35)
                {
                        Console.WriteLine(diffBaseAngle);
                        center = new AForge.IntPoint((int)(top.X / 2 + (base1.X + base2.X) / 4), (int)(top.Y / 2 + (base1.Y + base2.Y) / 4));
                        direction = top - center;
                        direction = new AForge.Point(direction.X / direction.EuclideanNorm(), direction.Y / direction.EuclideanNorm());
                        return true;
                }
            }
        }
            center = new AForge.IntPoint(0, 0);
            direction = new AForge.Point(0, 0);
            return false;
        }
         * 
         * 
         * 
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
         * 
         * 
         * 
         */

        internal double getDeltaTime()
        {
            return deltaTime;
        }
    }
}
