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



namespace CMVP
{
    class ImageProcessing : VideoStream
    {
        //used for drawing
        static private Pen redPen=new Pen(Color.Red, 2);
        static private Pen bluePen = new Pen(Color.LightSkyBlue,2);
        static private Pen greenPen = new Pen(Color.Green, 2);
        static private Pen yellowPen = new Pen(Color.Yellow, 2);
        static private Pen turkosPen = new Pen(Color.Turquoise, 2);
        static private Pen[] penArray = { bluePen, greenPen, yellowPen, turkosPen };
        private Graphics g;

        public Boolean drawCirkelsOnImg;
        public Boolean drawDirectionOnImg;
        public Boolean drawWindowsOnImg;
        public Boolean drawCenterOnImg;
        public Boolean drawTrackOnImg;
        public Boolean drawCarIdOnImg;
        public Boolean drawRefHeadingOnImg;
        public Boolean drawTailsOnImg;

        
        private VideoStream videoStream;
        private Bitmap img;
        private Bitmap croppedImg;
        private Bitmap canvas;

        private List<Panel> panelsToUpdate;
        public Panel panelToUpdate;
        //private System.Timers.Timer imgProcesTimer;
        private System.Windows.Forms.Timer imgProcesTimer;
        private System.Windows.Forms.Timer drawTimer;
        
        List<Blob> cirkels;
        List<Car> objects;
        List<Quadrilateral> squares = new List<Quadrilateral>();
        Dictionary<Car, Triangle> prevTriangles = new Dictionary<Car,Triangle>();
        
        //variables used for calculating time difference between updates
        private double deltaTime;
        private double prevTime;

        //sets ideal triangle base and height
        static private double idealHeight = 35; // 44 on table 35 on floor
        static private double idealBase = 12;  //  18 on table 12 on the floor.
        static private double heightError = 4;
        static private double baseError = 4;
        static private int blobMin = 2;
        static private int blobMax = 6;
        static private Triangle idealTriangle = new Triangle(idealHeight, idealBase);
        static private double worstAccepted = 0;
        

        public ImageProcessing(VideoStream videoStream,List<Car> objects)
        {
            //this.imgProcesTimer = new System.Timers.Timer();
            this.imgProcesTimer = new System.Windows.Forms.Timer();
            this.drawTimer = new System.Windows.Forms.Timer();
            this.imgProcesTimer.Interval=7;
            this.drawTimer.Interval = 50;
            //this.imgProcesTimer.Elapsed += processImage;
            this.imgProcesTimer.Tick += new EventHandler(processImage);
            this.drawTimer.Tick += new EventHandler(updatePanels);
            this.objects = objects;
            this.panelsToUpdate = new List<Panel>();
            this.videoStream = videoStream;

            this.cirkels = new List<Blob>();

            this.drawCirkelsOnImg = false;
            this.drawCenterOnImg = false;
            this.drawWindowsOnImg = false;
            this.drawDirectionOnImg = false;
            this.drawTailsOnImg = false;
            prevTime = videoStream.getTime();
            this.start();
            System.Console.WriteLine("Image processing OK");

        }
        void updatePanels(object sender, EventArgs e)
        {
            if (panelsToUpdate.Count == 0)
                Console.WriteLine("No Panel to update");
            else
            {
            Bitmap panelImage = drawFeaturesOnImg();
                foreach (Panel p in panelsToUpdate)
            {
                    p.BackgroundImage = panelImage;
            }
        }

        }
        Bitmap drawFeaturesOnImg()
        {

            if (img != null)
                canvas = (Bitmap)img.Clone();
            else
                return new Bitmap(10, 10);
            this.g = Graphics.FromImage(canvas);

            int k = 0;
            foreach (Quadrilateral q in squares)
            {
                g.DrawLines(penArray[k%4], q.getDrawingPoints());
                k++;
            }
            System.Drawing.Point[] dp = idealTriangle.getDrawingPoints();
            dp[0].Offset(600,600);
            dp[1].Offset(600,600);
            dp[2].Offset(600,600);
            dp[3].Offset(600, 600);
            g.DrawLines(yellowPen,dp);

            foreach(Car car in objects)
            {
                List<System.Drawing.Point> positionHistory = new List<System.Drawing.Point>();
                foreach(AForge.IntPoint p in car.getPositionHistory())
                {
                    positionHistory.Add(new System.Drawing.Point(p.X,p.Y));
                }
                g.DrawLines(turkosPen,positionHistory.ToArray());


                Controller controller = car.getController();
                ControlStrategy controlStra = car.getControlStrategy();
                float dir = controller.getRefHeading();
                if (controlStra != null)
                {
                    if (drawTrackOnImg && controlStra.getTrack()!=null)
                    {
                        List<IntPoint> track = controlStra.getTrack().getPoints();
                        System.Drawing.PointF[] pointTrack = new System.Drawing.PointF[track.Count];
                        for (int i = 0; i < track.Count; i++)
                        {
                            pointTrack[i] = new System.Drawing.PointF((float)track.ElementAt(i).X, (float)track.ElementAt(i).Y);
                        }
                        g.DrawLines(greenPen, pointTrack);
                    }
                    else if(drawTrackOnImg)
                    {
                        System.Drawing.Point pos = new System.Drawing.Point(car.getPosition().X-100,car.getPosition().Y+100-20);
                        g.DrawString("This Car has no track",new Font(FontFamily.GenericSansSerif,12.0F,FontStyle.Regular), Brushes.Green,pos);
                       
                    }
                    if (drawRefHeadingOnImg)
                    {
                        float heading = car.getController().getRefHeading();
                        System.Drawing.Point pos = new System.Drawing.Point(car.getPosition().X, car.getPosition().Y);
                        System.Drawing.Point pointHeading = new System.Drawing.Point((int)(car.getPosition().X + 40 * Math.Cos(heading)), (int)(car.getPosition().Y + 40 * Math.Sin(heading)));
                        g.DrawLine(bluePen, pos, pointHeading);
                        g.DrawEllipse(yellowPen,new Rectangle(controller.getRefPoint().X - 5, controller.getRefPoint().Y - 5, 10, 10));
                    }
                    
                }
                if (drawCarIdOnImg)
                {
                    Font f = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular);
                    Brush b = Brushes.Green;
                    System.Drawing.PointF idPos = new System.Drawing.PointF(car.getPosition().X-100, car.getPosition().Y-100);
                    g.DrawString(car.ID.ToString(),f, b,idPos);
                }
                if (drawCenterOnImg)
                {
                    // Only for testing triangles
                    List<Blob> cirkels = getBlobs(blobMin, blobMax, croppedImg);
                    List<AForge.IntPoint> points = getPoints(cirkels);
                    List<Triangle> triangles = getTriangles(points);
                    triangles = filterTriangleDubblets(triangles);
                    foreach (Triangle triangle in triangles)
                    {
                        if (triangle.compare(idealTriangle))
                        {
                            g.DrawLines(yellowPen, triangle.getDrawingPoints());
                        }
                    }
                }
            }
            if(drawWindowsOnImg)
                foreach(Car car in objects)
                {
                    AForge.IntPoint pos = car.getPosition();
                    //bör ta hänsyn till riktningen för minimera fönstret
                    int cropX = pos.X - 100;
                    int cropY = pos.Y - 100;
                    if (cropX < 0)
                        cropX = 0;
                    else if (cropX > img.Width - 200)
                        cropX = img.Width - 200;
                    if (cropY < 0)
                        cropY = 0;
                    else if (cropY > img.Height - 200)
                        cropY = img.Height - 200;
                    g.DrawRectangle(redPen, new Rectangle(cropX,cropY, 200, 200));
                }
            if (drawCirkelsOnImg)
            {
                List<Blob> cirkels = getBlobs(blobMin, blobMax, img);
                drawCirkels(cirkels);
            }
            foreach(Car car in objects)
            {
                if(drawCenterOnImg)
                    g.DrawEllipse(bluePen, new Rectangle((int)car.getPosition().X - 2, (int)car.getPosition().Y - 2, 2, 2));
                if(drawDirectionOnImg)
                    g.DrawLine(yellowPen, new System.Drawing.Point(car.getPosition().X, car.getPosition().Y), new System.Drawing.Point(car.getPosition().X + (int)car.getDirection().X * 40, car.getPosition().Y + (int)car.getDirection().Y * 40));
                }
            return canvas;
        }
        public void start()
        {
            imgProcesTimer.Start();
            drawTimer.Start();
        }
        public void initiate()
        {
            
            img = videoStream.getImage();
            List<Blob> cirkels = getBlobs(blobMin,blobMax,img);
            List<AForge.IntPoint> points = getPoints(cirkels);
            initiateCars(points);
            initiateBlocks(points);

            List<int> intList = new List<int>();
            foreach(Car car in objects)
            {
                intList.Add(car.ID);
            }
            MessageBox.Show("The following cars where found: " + String.Join(",",intList.ToArray()) + " \n " + Program.obstacle.Count + " obstecles was found");
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
                List<AForge.IntPoint> idPoints = getIdPoints(triangle,points);
                int id = idPoints.Count;
                if (id != 0)
                {
                    //Remove used points
                    foreach (AForge.IntPoint p in triangle.getPoints())
                        points.Remove(p);
                    foreach (AForge.IntPoint p in idPoints)
                       points.Remove(p);
                    Console.WriteLine("ID: " + id);
                    //Size need to be calculated implement later.
                    Car car = new Car(id, triangle.CENTER, triangle.DIRECTION, 50);
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
        private void processImage(object sender, EventArgs e)
        {
            //Console.WriteLine("ImgProcess Start: " + System.DateTime.Now.Millisecond);
            img = videoStream.getImage();
            double tempTime = videoStream.getTime();
            deltaTime = tempTime-prevTime;
            //Console.WriteLine("Delta time "+ prevTime);
            //Console.WriteLine("System Time " + System.DateTime.Now.Millisecond);
            prevTime = tempTime;

            foreach (Car car in objects)
            {
                AForge.IntPoint pos = car.getPosition();
                //bör ta hänsyn till riktningen för minimera fönstret
                int cropX, cropY;
                if (car.found)
                {
                    cropX = pos.X - 100;
                    cropY = pos.Y - 100;
                    if (cropX < 0)
                        cropX = 0;
                    else if (cropX > img.Width - 200)
                        cropX = img.Width - 200;
                    if (cropY < 0)
                        cropY = 0;
                    else if (cropY > img.Height - 200)
                        cropY = img.Height - 200;
                    croppedImg = img.Clone(new Rectangle(cropX, cropY, 200, 200), img.PixelFormat);
                }
                else
                {
                    cropX = 0;  
                    cropY = 0;
                    croppedImg = img;
                }

                List<Blob> cirkels = getBlobs(blobMin, blobMax, croppedImg);
                List<AForge.IntPoint> points = getPoints(cirkels);
                List<Triangle> triangles = getTriangles(points);
                triangles = filterTriangleDubblets(triangles);

                Triangle prevTriangle = null;
                prevTriangles.TryGetValue(car, out prevTriangle);

                triangles.Sort(delegate(Triangle t1, Triangle t2)
                {

                    return (t1.compareTo(prevTriangle).CompareTo(t2.compareTo(prevTriangle)));
                });
                List<double> d = new List<double>();
                foreach(Triangle t in triangles)
                {
                    d.Add(t.compareTo(prevTriangle));
                }
                bool carFoundThisTime = false;
                foreach(Triangle triangle in triangles)
                {
                    if (triangle.compareTo(prevTriangle) < 25)
                    {
                        AForge.IntPoint translation = new AForge.IntPoint(cropX, cropY);
                        List<AForge.IntPoint> idPoints = getIdPoints(triangle, points);
                        int triangleId = idPoints.Count;
                        if (car.ID == triangleId)
                        {
                            //Remove used points
                            foreach (AForge.IntPoint p in triangle.getPoints())
                                points.Remove(p);
                            foreach (AForge.IntPoint p in idPoints)
                                points.Remove(p);
                            car.setPositionAndOrientation(triangle.CENTER + translation, triangle.DIRECTION, deltaTime);
                            car.found = true;
                            carFoundThisTime = true;
                            break;
                        }
                    }
                }
                if (!carFoundThisTime)
                    car.found = false;

                    /*
                    foreach (Triangle triangle in triangles)
                    {

                        if (!carFoundThisTime && triangle.compare(idealTriangle))
                        {

                        AForge.IntPoint translation = new AForge.IntPoint(cropX,cropY);
                            List<AForge.IntPoint> idPoints = getIdPoints(triangle,points);
                            int triangleId = idPoints.Count;
                        if (car.ID == triangleId)
                        {
                                //Remove used points
                                foreach (AForge.IntPoint p in triangle.getPoints())
                                    points.Remove(p);
                                foreach(AForge.IntPoint p in idPoints)
                                    points.Remove(p);
                                car.setPositionAndOrientation(triangle.CENTER + translation, triangle.DIRECTION, deltaTime);
                            car.found = true;
                            carFoundThisTime = true;
                            break;
                        }
                    }
                }
                
                if(!carFoundThisTime)
                {
                    car.found = false;
                }
                    */
            }
           // Console.WriteLine("ImgProcess end: " + System.DateTime.Now.Millisecond);
        }
        private void drawCirkels(List<Blob> cirkels)
        {
            foreach(Blob cirkel in cirkels)
            {
                g.DrawEllipse(redPen, cirkel.Rectangle);
            }
        }
        private List<AForge.IntPoint> getPoints(List<Blob> blobs)
        {
            List<AForge.IntPoint> points = new List<AForge.IntPoint>();

            foreach(Blob b in blobs)
            {
                points.Add(b.CenterOfGravity.Round());
            }
            return points;
        }
        private List<Triangle> getTriangles(List<AForge.IntPoint> points)
        {
            List<Triangle> triangles = new List<Triangle>();
            foreach(AForge.IntPoint a in points)
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
                if (error == 0)
                {
                    Boolean add = true;
                    foreach(AForge.IntPoint c in triangle.getPoints())
                    {
                        if(c.Equals(p))
                        {
                            add = false;
                            break;
                        }

                    }
                    if(add)
                        idPoints.Add(p);
                }
            }
            return idPoints;
        }
        private List<Blob> getBlobs(int minHeight, int maxHeight,Bitmap img)
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.BackgroundThreshold = new RGB(90, 90, 90).Color;
            blobCounter.MinHeight = minHeight;
            blobCounter.MaxHeight = maxHeight;
            blobCounter.FilterBlobs = true;
            blobCounter.ProcessImage(img);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            return blobs.ToList<Blob>();
        }
        public void stop()
        {
            imgProcesTimer.Stop();
            drawTimer.Stop();
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
        public double getTime()
                {
            return videoStream.getTime();
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
    }
}
