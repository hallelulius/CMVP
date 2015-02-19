using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CMVP
{
    class ImageProcessing : VideoStream
    {
        List<int> objects;
        private VideoStream videoStream;
        private Bitmap img;

        public ImageProcessing(VideoStream videoStream)
        {
            this.videoStream = videoStream;
        }
        public Bitmap getImage()
        {
            return img;
        }
        public ImageProcessing()
        {
            System.Console.WriteLine("CreatImageProcessingClass");
            objects = new List<int>();

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
