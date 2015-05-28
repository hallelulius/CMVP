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

namespace CMVP
{

    // This class makes all the drawing to the graphical interface.
    class ImageProcessingGraphics
    {
        //static pens that is used for drawing
        static private Pen redPen = new Pen(Color.Red, 2);
        static private Pen bluePen = new Pen(Color.LightSkyBlue, 2);
        static private Pen greenPen = new Pen(Color.Green, 2);
        static private Pen yellowPen = new Pen(Color.Yellow, 2);
        static private Pen turquoisePen = new Pen(Color.Turquoise, 2);
        static private Pen[] penArray = { bluePen, greenPen, yellowPen, turquoisePen };
        private Graphics g;


        private Bitmap img;                     // The image to display.
        private ImageProcessing ip;             // The image process.
        private System.Timers.Timer timer;      // Update Timer.
        private Object locker;                  
        private Panel panel;                    // The panel which needs to be updated.

        //Variable used to determine what to draw
        public Boolean drawCirclesOnImg;
        public Boolean drawDirectionOnImg;
        public Boolean drawWindowsOnImg;
        public Boolean drawCenterOnImg;
        public Boolean drawTrackOnImg;
        public Boolean drawCarIdOnImg;
        public Boolean drawRefHeadingOnImg;
        public Boolean drawTailsOnImg;


        static private int blobMin = 2;         //Determine how big blobs should be accepted. Do not forgett to change in both ImageProcessing and ImageProcessingGraphics.
        static private int blobMax = 6;         //Determine how big blobs should be accepted. Do not forgett to change in both ImageProcessing and ImageProcessingGraphics.


        //Creates the imageProcessingGraphics
        public ImageProcessingGraphics(ImageProcessing ip)
        {
            standardSettings();
            this.ip = ip;
            locker = new Object();
            timer = new System.Timers.Timer(60);
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
        //Settings when the window is opened.
        private void standardSettings(){
            this.drawCirclesOnImg = false;
            this.drawCenterOnImg = false;
            this.drawWindowsOnImg = false;
            this.drawDirectionOnImg = false;
            this.drawTailsOnImg = false;
        }
        //Draws the circels that has been found by blobfinder as red circles.
        private void drawCircles(List<Blob> cirkels)
        {
            foreach (Blob cirkel in cirkels)
            {
                g.DrawEllipse(redPen, cirkel.Rectangle);
            }
        }
        //Draws features and updates the image if there is a panel to update-
        public void OnEvent(object source, ElapsedEventArgs e)
        {
            lock(locker){
                if (panel != null)
                {
                    panel.BackgroundImage = drawFeaturesOnImg();

                }
            }    
            
        }
        //Returns a bitmap that has all the animations on the image.
        Bitmap drawFeaturesOnImg()
        {
            //If it cant get a image from the image process then just make an empty bitmap.
            if (ip.getImage() != null)
                img = (Bitmap)ip.getImage();
            else
                return new Bitmap(10, 10);
            this.g = Graphics.FromImage(img);

            foreach (Car car in Program.cars)
            {
                //retain the blobs from the image process without disturbing it and draws them on the image.
                if (drawCirclesOnImg)
                {
                    List<Blob> cirkels = ip.getBlobsSlow(blobMin, blobMax, img);
                    drawCircles(cirkels);
                }
                //retain the position history of each car and draws it on the image.
                if (drawTailsOnImg)
                {
                    List<System.Drawing.Point> positionHistory = new List<System.Drawing.Point>();
                    foreach (AForge.IntPoint p in car.getPositionHistory())
                    {
                        positionHistory.Add(new System.Drawing.Point(p.X, p.Y));
                    }
                    g.DrawLines(turquoisePen, positionHistory.ToArray());

                }

                //Draws the tracks on the image if there is one otherwise it will write no track
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
                    //draws the direction to the next point.
                    if (drawRefHeadingOnImg)
                    {
                        float heading = car.getController().getRefHeading();
                        System.Drawing.Point pos = new System.Drawing.Point(car.getPosition().X, car.getPosition().Y);
                        System.Drawing.Point pointHeading = new System.Drawing.Point((int)(car.getPosition().X + 40 * Math.Cos(heading)), (int)(car.getPosition().Y + 40 * Math.Sin(heading)));
                        g.DrawLine(bluePen, pos, pointHeading);
                        g.DrawEllipse(yellowPen, new Rectangle(controller.getRefPoint().X - 5, controller.getRefPoint().Y - 5, 10, 10));
                    }

                }
                //Writes the car id on the image
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
            //Draws the window where the image process i croping the image.
            if (drawWindowsOnImg)
                foreach (Car car in Program.cars)
                {
                    AForge.IntPoint pos = car.getPosition();
                    //bör ta hänsyn till riktningen för minimera fönstret
                    Size windowSize = new Size(150, 150);
                    int cropX = pos.X - windowSize.Width / 2;
                    int cropY = pos.Y - windowSize.Height / 2;
                    if (cropX < 0)
                        cropX = 0;
                    else if (cropX > img.Width - windowSize.Width)
                        cropX = img.Width - windowSize.Width / 2;
                    if (cropY < 0)
                        cropY = 0;
                    else if (cropY > img.Height - windowSize.Height)
                        cropY = img.Height - windowSize.Height;
                    g.DrawRectangle(redPen, new Rectangle(cropX, cropY, windowSize.Width, windowSize.Width));
                }
            //Draws the center of each car on the image.
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
