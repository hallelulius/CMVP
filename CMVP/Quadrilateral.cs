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
    
    class Quadrilateral
    {
        private AForge.IntPoint[] corners = new AForge.IntPoint[4]; public AForge.IntPoint[] CORNERS {get{ return corners; } }
        private double[] lengths = new double[4]; public double[] LENGTHS { get { return lengths;} }
        private double[] angles = new double[4]; public double[] ANGLES { get { return angles; } }
        private AForge.IntPoint center; public AForge.IntPoint CENTER { get { return center; } }
        private double size; public double SIZE { get { return size; } }



        public Quadrilateral(AForge.IntPoint[] corners) 
        {
            List<AForge.IntPoint> cornerList = new List<AForge.IntPoint>();
            for(int k=0; k < corners.Length; k++)
            {
                cornerList.Add(corners[k]);
            }
            sortCorners(cornerList);
            initiateValues();
        }
        public Quadrilateral(List<AForge.IntPoint> corners)
        {
            sortCorners(corners);
            initiateValues();
        }
        private void sortCorners(List<AForge.IntPoint> corners)
        {
            corners.Sort(delegate(AForge.IntPoint p1, AForge.IntPoint p2)
            {
                if (p1.Y == p2.Y)
                    return p1.X - p2.X;
                else
                    return p1.Y - p2.Y;
            });
            this.corners[0] = corners[0];
            corners.RemoveAt(0);
            this.corners[1] = shortestPath(this.corners[0], corners);
            corners.Remove(this.corners[1]);
            this.corners[2] = shortestPath(this.corners[1], corners);
            corners.Remove(this.corners[2]);
            this.corners[3] = corners[0];
        }
        private AForge.IntPoint shortestPath(AForge.IntPoint p, List<AForge.IntPoint> goals)
        {
            AForge.IntPoint shortestSoFar = goals[0];
            double shortestDistanceSoFar = p.DistanceTo(shortestSoFar);
            foreach(AForge.IntPoint g in goals)
            {
                if(shortestDistanceSoFar > p.DistanceTo(g))
                {
                    shortestSoFar = g;
                    shortestDistanceSoFar = p.DistanceTo(g); 
                }
            }
            return shortestSoFar;
        }
        private void initiateValues()
        {
            angles[0] = angle(corners[3] - corners[0], corners[1] - corners[0]);

            //This is to prevent a bowtie
            if(angles[0]< Math.PI/4)
            {
                AForge.IntPoint swapVar = corners[2];
                corners[2] = corners[3];
                corners[3] = swapVar;
                angles[0] = angle(corners[3] - corners[0], corners[1] - corners[0]);
            }
            angles[1] = angle(corners[0] - corners[1], corners[2] - corners[1]);
            angles[2] = angle(corners[1] - corners[2], corners[3] - corners[2]);
            angles[3] = angle(corners[2] - corners[3], corners[0] - corners[3]);

            lengths[0] = corners[0].DistanceTo(corners[1]);
            lengths[1] = corners[1].DistanceTo(corners[2]);
            lengths[2] = corners[2].DistanceTo(corners[3]);
            lengths[3] = corners[3].DistanceTo(corners[0]);

            center = corners[0] + corners[1] + corners[2] + corners[3];
            center = new AForge.IntPoint(center.X / 4, center.Y / 4);
            size = corners[0].DistanceTo(corners[2]);
        }


        public bool Equals(Quadrilateral q)
        {
            for(int k=0; k < q.CORNERS.Length;k++)
            {
                if (!this.CORNERS[k].Equals(q.CORNERS[k]))
                    return false;
            }
            return true;
        }
        public bool square(double errorMargin)
        {
            double diff1 = Math.Abs(lengths[0] - lengths[2]);
            double diff2 = Math.Abs(lengths[1] - lengths[3]);

            double error1 = diff1 / lengths[0];
            double error2 = diff2 / lengths[1];

            double angleDiff0 = Math.Abs(angles[0] - Math.PI / 2);
            double angleDiff1 = Math.Abs(angles[1] - Math.PI / 2);
            double angleDiff2 = Math.Abs(angles[2] - Math.PI / 2);
            double angleDiff3 = Math.Abs(angles[3] - Math.PI / 2);

            double angleError0 = angleDiff0 / (Math.PI / 2);
            double angleError1 = angleDiff1 / (Math.PI / 2);
            double angleError2 = angleDiff2 / (Math.PI / 2);
            double angleError3 = angleDiff3 / (Math.PI / 2);

            return error1 < errorMargin && error2 < errorMargin && angleError0 < errorMargin && angleError1 < errorMargin && angleError2 < errorMargin && angleError3 < errorMargin;

        }
        public System.Drawing.Point[] getDrawingPoints()
        {
            return new System.Drawing.Point[] { convert(corners[0]),convert(corners[1]),convert(corners[2]),convert(corners[3]),convert(corners[0])};
        }
        public AForge.IntPoint[] getSize()
        {
            return corners;
        }
        public double getArea()
        {
            return crossProduct(corners[3]-corners[0],corners[1]-corners[0]);
        }
        private System.Drawing.Point convert(AForge.IntPoint p)
        {
            return new System.Drawing.Point(p.X,p.Y);
        } 
        private double angle(AForge.IntPoint a, AForge.IntPoint b)
        {
            double scalarProduct = (a.X * b.X + a.Y * b.Y)/a.EuclideanNorm() / b.EuclideanNorm();
            return Math.Acos(scalarProduct);
        }
        private double crossProduct(AForge.IntPoint a, AForge.IntPoint b)
        {
            return Math.Abs(a.X * b.Y - a.Y * b.X);
        }
        
    }
}
