using AForge;
using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace CMVP.GUI
{
    class ImageProcessingDrawing
    {
        //used for drawing
        static private Pen redPen = new Pen(Color.Red, 2);
        static private Pen bluePen = new Pen(Color.LightSkyBlue, 2);
        static private Pen greenPen = new Pen(Color.Green, 2);
        static private Pen yellowPen = new Pen(Color.Yellow, 2);
        static private Pen turquoisePen = new Pen(Color.Turquoise, 2);
        static private Pen[] penArray = { bluePen, greenPen, yellowPen, turquoisePen };
        private Graphics g;

        private Bitmap img;
        private Bitmap drawImg;
        private ImageProcessing ip;
        private System.Timers.Timer timer;
        private Object locker;
        private Panel panel;
        public Boolean drawCirkelsOnImg;
        public Boolean drawDirectionOnImg;
        public Boolean drawWindowsOnImg;
        public Boolean drawCenterOnImg;
        public Boolean drawTrackOnImg;
        public Boolean drawCarIdOnImg;
        public Boolean drawRefHeadingOnImg;
        public Boolean drawTailsOnImg;


        static private int blobMin = 2;
        static private int blobMax = 6;

        public ImageProcessingDrawing(ImageProcessing ip)
        {
            standardSettings();
            this.ip = ip;
            locker = new Object();
            timer = new System.Timers.Timer(50);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnEvent);
            timer.Enabled = true;
        }

        public void start()
        {
            timer.Enabled = true;
        }

        public void setPanel(Panel panel)
        {
            this.panel = panel;
        }
        private void standardSettings(){
            this.drawCirkelsOnImg = false;
            this.drawCenterOnImg = false;
            this.drawWindowsOnImg = false;
            this.drawDirectionOnImg = false;
            this.drawTailsOnImg = false;
        }
        private void drawCirkels(List<Blob> cirkels)
        {
            foreach (Blob cirkel in cirkels)
            {
                g.DrawEllipse(redPen, cirkel.Rectangle);
            }
        }

        public void OnEvent(object source, ElapsedEventArgs e)
        {
            lock(locker){
                if (panel != null)
                {

                    drawImg = drawFeaturesOnImg();
                    panel.BackgroundImage = new Bitmap(drawImg, panel.Size);

                }
            }    
            
        }
        Bitmap drawFeaturesOnImg()
        {

            if (ip.getImage() != null)
                img = (Bitmap)ip.getImage();
            else
                return new Bitmap(10, 10);
            this.g = Graphics.FromImage(img);
            /*
            int k = 0;
            foreach (Quadrilateral q in squares)
            {
                g.DrawLines(penArray[k % 4], q.getDrawingPoints());
                k++;
            }
            System.Drawing.Point[] dp = idealTriangle.getDrawingPoints();
            dp[0].Offset(600, 600);
            dp[1].Offset(600, 600);
            dp[2].Offset(600, 600);
            dp[3].Offset(600, 600);
            g.DrawLines(yellowPen, dp);
            */
            foreach (Car car in Program.cars)
            {
                if (drawTailsOnImg)
                {
                    //turquoise if the car is found else red
                    List<System.Drawing.Point> positionHistory = new List<System.Drawing.Point>();
                    
                    /*IntPoint p1 = car.getPositionHistory().First();
                    bool b1 = car.getFoundList().First();
                    for (int i = 0; i < car.HISTORY_LENGTH; i++)
                    {
                        IntPoint p2 = car.getPositionHistory().ElementAt(i);
                        bool b2 = car.getFoundList().ElementAt(i);
                        if (b1)
                        {
                            g.DrawLine(turquoisePen, new System.Drawing.Point(p1.X, p1.Y), new System.Drawing.Point(p2.X, p2.Y));

                        }
                        else
                        {
                            g.DrawLine(redPen, new System.Drawing.Point(p1.X, p1.Y), new System.Drawing.Point(p2.X, p2.Y));
                        }
                        p1 = p2;
                        b1 = b2;
                    }
                     * */
                    foreach (AForge.IntPoint p in car.getPositionHistory())
                    {
                        positionHistory.Add(new System.Drawing.Point(p.X, p.Y));
                    }
                    g.DrawLines(turquoisePen, positionHistory.ToArray());

                }

                Controller controller = car.getController();
                ControlStrategy controlStra = car.getControlStrategy();
                float dir = controller.getRefHeading();
                if (controlStra != null)
                {
                    if (drawTrackOnImg && controlStra.getTrack() != null)
                    {
                        List<IntPoint> track = controlStra.getTrack().getPoints();
                        System.Drawing.PointF[] pointTrack = new System.Drawing.PointF[track.Count];
                        for (int i = 0; i < track.Count; i++)
                        {
                            pointTrack[i] = new System.Drawing.PointF((float)track.ElementAt(i).X, (float)track.ElementAt(i).Y);
                        }
                        g.DrawLines(greenPen, pointTrack);
                    }
                    else if (drawTrackOnImg)
                    {
                        System.Drawing.Point pos = new System.Drawing.Point(car.getPosition().X - 100, car.getPosition().Y + 100 - 20);
                        g.DrawString("This Car has no track", new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular), Brushes.Green, pos);

                    }
                    if (drawRefHeadingOnImg)
                    {
                        float heading = car.getController().getRefHeading();
                        System.Drawing.Point pos = new System.Drawing.Point(car.getPosition().X, car.getPosition().Y);
                        System.Drawing.Point pointHeading = new System.Drawing.Point((int)(car.getPosition().X + 40 * Math.Cos(heading)), (int)(car.getPosition().Y + 40 * Math.Sin(heading)));
                        g.DrawLine(bluePen, pos, pointHeading);
                        g.DrawEllipse(yellowPen, new Rectangle(controller.getRefPoint().X - 5, controller.getRefPoint().Y - 5, 10, 10));
                    }

                }
                if (drawCarIdOnImg)
                {
                    Font f = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular);
                    Brush b = Brushes.Green;
                    System.Drawing.PointF idPos = new System.Drawing.PointF(car.getPosition().X - 100, car.getPosition().Y - 100);
                    g.DrawString(car.ID.ToString(), f, b, idPos);
                }
                /*
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
                 */
            }
            if (drawWindowsOnImg)
                foreach (Car car in Program.cars)
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
                    g.DrawRectangle(redPen, new Rectangle(cropX, cropY, 200, 200));
                }
            if (drawCirkelsOnImg)
            {
                List<Blob> cirkels = ip.getBlobs(blobMin, blobMax, img);
                drawCirkels(cirkels);
            }
            foreach (Car car in Program.cars)
            {
                if (drawCenterOnImg)
                    g.DrawEllipse(bluePen, new Rectangle((int)car.getPosition().X - 2, (int)car.getPosition().Y - 2, 2, 2));
                if (drawDirectionOnImg)
                    g.DrawLine(yellowPen, new System.Drawing.Point(car.getPosition().X, car.getPosition().Y), new System.Drawing.Point(car.getPosition().X + (int)car.getDirection().X * 40, car.getPosition().Y + (int)car.getDirection().Y * 40));
            }
            return img;
        }
    }
}
