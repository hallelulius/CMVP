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
        private Graphics g;

        public Boolean drawCirkelsOnImg;
        public Boolean drawDirectionOnImg;
        public Boolean drawWindowsOnImg;
        public Boolean drawCenterOnImg;
        public Boolean drawTrackOnImg;
        public Boolean drawCarIdOnImg;
        public Boolean drawRefHeadingOnImg;

        
        private VideoStream videoStream;
        private Bitmap img;
        private Bitmap croppedImg;
        private Bitmap canvas;

        private List<AForge.IntPoint> Acenters;
        private List<AForge.Point> Adirections;
        private List<Panel> panelsToUpdate;
        //private System.Timers.Timer imgProcesTimer;
        private System.Windows.Forms.Timer imgProcesTimer;
        private System.Windows.Forms.Timer drawTimer;
        
        List<Blob> cirkels;
        Dictionary<int, Car> carMap;
        List<Car> objects;
        
        //variables used for calculating time difference between updates
        private double deltaTime;
        private double prevTime;

        //sets ideal triangle base and height
        private double idealHeight = 44; // 44 on table 33 on floor
        private double idealBase = 13;  //  13 on table 10 on floor
        private double heightError = 7;
        private double baseError = 3;
        

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
            this.carMap = new Dictionary<int, Car>();

            this.cirkels = new List<Blob>();
            this.Acenters = new List<AForge.IntPoint>();
            this.Adirections = new List<AForge.Point>();

            this.drawCirkelsOnImg = false;
            this.drawCenterOnImg = false;
            this.drawWindowsOnImg = false;
            this.drawDirectionOnImg = false;
            drawTimer.Start();
            prevTime = videoStream.getTime();
            System.Console.WriteLine("Image processing OK");

        }
        void updatePanels(object sender, EventArgs e)
        {
            Bitmap panelImage = drawFeaturesOnImg();
            foreach (Panel panel in panelsToUpdate)
            {
                panel.BackgroundImage = panelImage;
            }
        }
        Bitmap drawFeaturesOnImg()
        {
            //Console.WriteLine("draw: " + System.DateTime.Now.Millisecond); 
            if (img != null)
                canvas = (Bitmap)img.Clone();
            else
                return new Bitmap(10, 10);
            this.g = Graphics.FromImage(canvas);

            foreach(Car car in objects)
            {

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
                    List<Blob> cirkels = getBlobs(2, 13, croppedImg);
                    List<AForge.IntPoint> points = getPoints(cirkels);
                    List<AForge.IntPoint[]> triangles = getTriangels(points);
                    triangles = filterDubblets(triangles);
                    foreach (AForge.IntPoint[] triangle in triangles)
                    {
                        AForge.IntPoint Acenter;
                        AForge.Point Adirektion;

                        if (getInformationFromTriangle(triangle, idealHeight, idealBase, heightError, baseError, out Acenter, out Adirektion))
                        {
                            System.Drawing.Point p1 = new System.Drawing.Point(triangle[1].X, triangle[1].Y);
                            System.Drawing.Point p2 = new System.Drawing.Point(triangle[2].X, triangle[2].Y);
                            System.Drawing.Point p3 = new System.Drawing.Point(triangle[0].X, triangle[0].Y);
                            g.DrawLine(yellowPen, p1, p2);
                            g.DrawLine(yellowPen, p2, p3);
                            g.DrawLine(yellowPen, p3, p1);
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
                List<Blob> cirkels = getBlobs(2, 13, img);
                drawCirkels(cirkels);
            }
            for (int k = 0; k < Acenters.Count; k++)
            {
                if (drawCenterOnImg)
                {
                    g.DrawEllipse(bluePen, new Rectangle((int)Acenters[k].X - 2, (int)Acenters[k].Y - 2, 2, 2));
                }
                if (drawDirectionOnImg)
                {
                    g.DrawLine(yellowPen, new System.Drawing.Point((int)Acenters[k].X, (int)Acenters[k].Y), new System.Drawing.Point((int)(Acenters[k].X + Adirections[k].X * 40), (int)(Acenters[k].Y + Adirections[k].Y * 40)));
                }
            }
            return canvas;
        }
        public void start()
        {
            imgProcesTimer.Start();
        }
        public void initiate()
        {
            objects.Clear();
            img = videoStream.getImage();
            List<Blob> cirkels = getBlobs(2, 13,img);
            Acenters = new List<AForge.IntPoint>();
            Adirections = new List<AForge.Point>();
            List<AForge.IntPoint> points = getPoints(cirkels);
            List<AForge.IntPoint[]> triangles = getTriangels(points);
            triangles = filterDubblets(triangles);

            foreach (AForge.IntPoint[] triangle in triangles)
            {
                AForge.IntPoint Acenter;
                AForge.Point Adirektion;



                if (getInformationFromTriangle(triangle, idealHeight, idealBase, heightError, baseError, out Acenter, out Adirektion))
                {
                    Acenters.Add(Acenter);
                    Adirections.Add(Adirektion);
                }
            }
            for (int k = 0; k < Acenters.Count; k++)
            {
                Console.WriteLine(k);
                int id = getId(Acenters[k], Adirections[k], cirkels);
                if (id != 0)
                {
                    Console.WriteLine("ID: " + id);
                    Car car = new Car(id, Acenters[k], Adirections[k]);
                    objects.Add(car);
                }
            }
            List<int> intList = new List<int>();
            foreach(Car car in objects)
            {
                intList.Add(car.ID);
            }
            MessageBox.Show("The following cars where found: " + String.Join(",",intList.ToArray()));
        }

        private void processImage(object sender, EventArgs e)
        {
            //Console.WriteLine("ImgProcess Start: " + System.DateTime.Now.Millisecond);
            img = videoStream.getImage();
            double tempTime = videoStream.getTime();
            deltaTime = tempTime-prevTime;
            Console.WriteLine("Delta time "+ prevTime);
            Console.WriteLine("System Time " + System.DateTime.Now.Millisecond);
            prevTime = tempTime;

            foreach (Car car in objects)
            {
                AForge.IntPoint pos=car.getPosition();
                //bör ta hänsyn till riktningen för minimera fönstret
                int cropX,cropY;
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


                Acenters = new List<AForge.IntPoint>();
                Adirections = new List<AForge.Point>();

                List<Blob> cirkels = getBlobs(2, 13, croppedImg);
                List<AForge.IntPoint> points = getPoints(cirkels);
                List<AForge.IntPoint[]> triangles = getTriangels(points);
                triangles = filterDubblets(triangles);
                bool carFoundThisTime = false;
                foreach (AForge.IntPoint[] triangle in triangles)
                {
                    AForge.IntPoint Acenter;
                    AForge.Point Adirektion;

                    if (!carFoundThisTime && getInformationFromTriangle(triangle, idealHeight, idealBase, heightError, baseError, out Acenter, out Adirektion))
                    {
                        //AForge.IntPoint translation = pos - new AForge.IntPoint(100, 100);
                        AForge.IntPoint translation = new AForge.IntPoint(cropX,cropY);
                        Acenters.Add(translation+Acenter);
                        Adirections.Add(Adirektion);
                        int triangleId = getId(Acenter, Adirektion, cirkels);
                        Console.WriteLine("id: " + car.ID);
                        if (car.ID == triangleId)
                        {
                            car.setPositionAndOrientation(Acenter + translation, Adirektion, deltaTime);
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
        private List<AForge.IntPoint> getPoints(List<Blob> blobs)
        {
            List<AForge.IntPoint> points = new List<AForge.IntPoint>();

            foreach(Blob b in blobs)
            {
                points.Add(b.CenterOfGravity.Round());
            }
            return points;
        }
        //function to get triangels in se
        private List<AForge.IntPoint[]> getTriangels(List<AForge.IntPoint> points)
        {
            List<AForge.IntPoint[]> pointList = new List<AForge.IntPoint[]>();
            foreach(AForge.IntPoint a in points)
            {
                foreach (AForge.IntPoint b in points)
                {
                    foreach (AForge.IntPoint c in points)
                    {
                        if (a != b && a != c && b != c)
                        {
                            //We need to check a that there doesn't exist any duplicates
                            AForge.IntPoint[] locations = new AForge.IntPoint[3];

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
        bool getInformationFromTriangle(AForge.IntPoint[] points,double idealHeight, double idealBase, double heightError, double baseError, out AForge.IntPoint center, out AForge.Point direction)
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
       
        /* NOT USED?
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
          */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangles"></param>
        /// <returns></returns>
        private List<AForge.IntPoint[]> filterDubblets(List<AForge.IntPoint[]> triangles)
        {
            List<AForge.IntPoint[]> filteredTriangles = new List<AForge.IntPoint[]>();

            foreach (AForge.IntPoint[] triangle in triangles)
            {
                Array.Sort(triangle,
                    delegate(AForge.IntPoint p1, AForge.IntPoint p2)
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
            foreach (AForge.IntPoint[] t1 in triangles)
            {
                Boolean add = true;
                foreach (AForge.IntPoint[] t2 in filteredTriangles)
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
        private List<Blob> getBlobs(int minHeight, int maxHeight,Bitmap img)
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.BackgroundThreshold = new RGB(150, 150, 150).Color;
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
    }
}
