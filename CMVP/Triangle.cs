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
    class Triangle
    {
        private AForge.IntPoint top; public AForge.IntPoint TOP { get {return top; } }
        private AForge.IntPoint base1; public AForge.IntPoint BASE1 { get { return base1; } }
        private AForge.IntPoint base2; public AForge.IntPoint BASE2 { get { return base2; } }
        private AForge.IntPoint center; public AForge.IntPoint CENTER{get { return center; } }
        private AForge.Point direction; public AForge.Point DIRECTION { get { return direction; } }
        private double topAngle; public double TOP_ANGLE { get { return topAngle; } }
        private double baseAngle1; public double BASE_ANGLE_1 { get { return baseAngle1; } }
        private double baseAngle2; public double BASE_ANGLE_2 { get { return baseAngle2; } }
        private double[] sides = new double[3]; public double[] SIDES { get { return sides; } }

        //This is the hight and base only if it is a isosceles triangle.
        private double height; public double HEIGHT {get {return height;}}
        private double length; public double LENGTH {get {return length;}}
        

        public Triangle(AForge.IntPoint[] points)
        {
            sortPoints(points);
            initiateValues();
        }
        public Triangle(List<AForge.IntPoint> points)
        {
            sortPoints(points.ToArray());
            initiateValues();
        }
        public Triangle(double height, double length)
        {
            base1 = new AForge.IntPoint(0, 0);
            base2 = new AForge.IntPoint((int)length,0);
            top = new AForge.IntPoint((int)Math.Round(length/2),(int)Math.Round(height));
            initiateValues();
        }
        public double getArea()
        {
            return crossProduct(base1-top,base2-top)/2;
        }
        
        public AForge.IntPoint[] getPoints()
        {
            return new AForge.IntPoint[]{top,base1,base2};
        }
        public Quadrilateral getRectangle()
        {
            //Return a Quadrilateral with corners in the base points and has the height the same as the triangle.
            AForge.IntPoint top1 = new AForge.IntPoint((int)Math.Round((base1.X + direction.X * height)),(int)Math.Round(base1.Y + direction.Y * height));
            AForge.IntPoint top2 = new AForge.IntPoint((int)Math.Round((base2.X + direction.X * height)),(int)Math.Round(base2.Y + direction.Y * height));
            AForge.IntPoint top3 = base2;
            AForge.IntPoint top4 = base1;
            return new Quadrilateral(new AForge.IntPoint[] { top3, top4, top1,top2});
        }
        public System.Drawing.Point[] getDrawingPoints()
        {

            return new System.Drawing.Point[]{convert(top),convert(base1),convert(base2),convert(top)};
        }
        public Boolean Equals(Triangle t)
        {
            return top.Equals(t.TOP) && base1.Equals(t.BASE1) && base2.Equals(t.BASE2);
        }
        public bool compare( Triangle ideal, double heightError, double lengthError)
        {

            double diffHeight = Math.Abs(this.HEIGHT - ideal.HEIGHT);
            double diffLength = Math.Abs(this.LENGTH - ideal.LENGTH);

            double topAngleDiff = Math.Abs(this.TOP_ANGLE - ideal.TOP_ANGLE);
            double baseAngleDiff1 = Math.Abs(this.baseAngle1 - ideal.baseAngle1);
            double baseAngleDiff2 = Math.Abs(this.baseAngle1 - ideal.baseAngle2);

            double errorTopAngle = topAngleDiff / ideal.TOP_ANGLE;
            double errorBaseAngle1 = baseAngleDiff1 / ideal.baseAngle1;
            double errorBaseAngle2 = baseAngleDiff2 / ideal.baseAngle2;

            double sideDiff0 = Math.Abs(this.SIDES[0] - ideal.SIDES[0]);
            double sideDiff1 = Math.Abs(this.SIDES[1] - ideal.SIDES[1]);
            double sideDiff2 = Math.Abs(this.SIDES[2] - ideal.SIDES[2]);

            double errorSide0 = sideDiff0 / ideal.SIDES[0];
            double errorSide1 = sideDiff1 / ideal.SIDES[1];
            double errorSide2 = sideDiff2 / ideal.SIDES[2];


            if (diffHeight < heightError && diffLength < lengthError && errorTopAngle < 0.40 && errorBaseAngle1 < 0.05 && errorBaseAngle2 < 0.05 && errorSide0 < 0.17 && errorSide1 < 0.17 && errorSide2 < 0.17)
            {
                return true;
            }
            return false;
        }
        
        
        private void sortPoints(AForge.IntPoint[] points)
        {
            double d0 = points[1].DistanceTo(points[2]);
            double d1 = points[2].DistanceTo(points[0]);
            double d2 = points[1].DistanceTo(points[0]);

            if (d0 < d1 && d0 < d2)
            {
                //Console.WriteLine("d0 base");
                top = points[0];
                base1 = points[1];
                base2 = points[2];
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
            if(base2.X < base1.X)
            {
                AForge.IntPoint tempPoint = base1;
                base1 = base2;
                base2 = tempPoint;
            }
            else if(base2.X == base1.X)
            {
                if(base2.Y < base1.Y)
                {
                    AForge.IntPoint tempPoint = base1;
                    base1 = base2;
                    base2 = tempPoint;
                }
            }
        }
        private void initiateValues()
        {
            length = base1.DistanceTo(base2);
            height = Math.Sqrt(top.DistanceTo(base1) * top.DistanceTo(base1) - (length / 2) * (length / 2));
            center = new AForge.IntPoint((int)(top.X / 2 + (base1.X + base2.X) / 4), (int)(top.Y / 2 + (base1.Y + base2.Y) / 4));
            direction = top - center;
            direction = new AForge.Point(direction.X / direction.EuclideanNorm(), direction.Y / direction.EuclideanNorm());

            sides[0] = base1.DistanceTo(base2);
            sides[1] = base2.DistanceTo(top);
            sides[2] = base1.DistanceTo(top);

            //This does not work for all triangles. But it works for us.
            topAngle = 2 * Math.Atan(length/height/2);
            baseAngle1 = baseAngle2 = (Math.PI - topAngle) / 2;
            
        }
        private System.Drawing.Point convert(AForge.IntPoint p)
        {
            return new System.Drawing.Point(p.X,p.Y);
        } 
        private double crossProduct(AForge.IntPoint a, AForge.IntPoint b)
        {
            return Math.Abs(a.X * b.Y - a.Y * b.X);
        }
        private double angle(AForge.IntPoint a, AForge.IntPoint b)
        {
            double scalarProduct = (a.X * b.X + a.Y * b.Y) / a.EuclideanNorm() / b.EuclideanNorm();
            return Math.Acos(scalarProduct);
        }
    }
}
