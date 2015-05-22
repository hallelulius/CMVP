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
using System.Diagnostics;

namespace CMVP
{
    /* The class represent a Triangle with 3 points.
     * The class is made to use isosceles triangles and will not work very well with other
     * types of triangles.
    */
    class Triangle
    {
        private AForge.IntPoint top; public AForge.IntPoint TOP { get {return top; } }                  //The point which has the narrowest angle. 
        private AForge.IntPoint base1; public AForge.IntPoint BASE1 { get { return base1; } }           //First basepoints.
        private AForge.IntPoint base2; public AForge.IntPoint BASE2 { get { return base2; } }           //Second basepoint.
        private AForge.IntPoint center; public AForge.IntPoint CENTER{get { return center; } }          //Calculated centrum for the triangle.
        private AForge.Point direction; public AForge.Point DIRECTION { get { return direction; } }     //The direction is directed towards the top point from the center.
        private double topAngle; public double TOP_ANGLE { get { return topAngle; } }                   //The angle of the top point.
        private double baseAngle1; public double BASE_ANGLE_1 { get { return baseAngle1; } }            //The angle of the first base point.
        private double baseAngle2; public double BASE_ANGLE_2 { get { return baseAngle2; } }            //The angle of the second basepoint.
        private double[] sides = new double[3]; public double[] SIDES { get { return sides; } }         //An array that represents the sides lengths.


        //This is the hight and base only if it is a isosceles triangle.
        private double height; public double HEIGHT {get {return height;}}      //The height of an isolceles triangle.
        private double length; public double LENGTH {get {return length;}}      //The length of an isolcels triangle.
        
        //Constructors
        //Constructs the triangle from an array.
        public Triangle(AForge.IntPoint[] points)
        {
            //The points have to be sorted before the initiation.
            sortPoints(points);
            initiateValues();
        }
       //Constructs a triangle from a List
        public Triangle(List<AForge.IntPoint> points)
        {
            //The points have to be sorted before the initiation.
            sortPoints(points.ToArray());
            initiateValues();
        }
        //Constructs a perfect isosceles triangle that is located with first basepoint at origo.
        //The direction will be vertical.
        public Triangle(double height, double length)
        {
            AForge.IntPoint[] points = new AForge.IntPoint[3];
            points[0] = new AForge.IntPoint(0, 0);  //base
            points[1] = new AForge.IntPoint((int)length,0); //base
            points[2] = new AForge.IntPoint((int)Math.Round(length/2),(int)Math.Round(height)); //top

            sortPoints(points);
            initiateValues();
        }
        // A Triangle constructed with specification of position, direction height and length.
        public Triangle(AForge.IntPoint position, AForge.Point direction, double height, double length)
        {
            //The top point i translated from the center to its place according to the direction and height.
            AForge.IntPoint[] points = new AForge.IntPoint[3];
            AForge.IntPoint tran = new AForge.IntPoint((int)Math.Round(direction.X*height/2),(int)Math.Round(direction.Y*height/2));
            points[0] = position + tran;
            
            // A point between the two base points is calculated to be use further.
            AForge.IntPoint middlePointBase = position - new AForge.IntPoint((int)Math.Round(direction.X * height / 2), (int)Math.Round(direction.Y * height / 2));
            //Basepoints
            //The basepoints has to be translated in two directions therefor the orthogonal of the Direction is calculated.
            AForge.Point orthogonolDirection = giveOrthogonol(direction);
            points[1] = middlePointBase + new AForge.IntPoint(-(int)Math.Round(orthogonolDirection.X * length / 2), (int)Math.Round(orthogonolDirection.Y * length / 2));
            points[2] = middlePointBase - new AForge.IntPoint(-(int)Math.Round(orthogonolDirection.X * length / 2), (int)Math.Round(orthogonolDirection.Y * length / 2));

            sortPoints(points);
            initiateValues();
        }
        //Sort the point so that the order of the points will not matter.
        private void sortPoints(AForge.IntPoint[] points)
        {

            //The shortest side of the traingle will be on the oposite side from the top.
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
            if (base2.X < base1.X)
            {
                AForge.IntPoint tempPoint = base1;
                base1 = base2;
                base2 = tempPoint;
            }
            else if (base2.X == base1.X)
            {
                if (base2.Y < base1.Y)
                {
                    AForge.IntPoint tempPoint = base1;
                    base1 = base2;
                    base2 = tempPoint;
                }
            }
        }
        //Initiate the values for the traingle and calculates sides angles.
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
            topAngle = 2 * Math.Atan(length / height / 2);
            baseAngle1 = baseAngle2 = (Math.PI - topAngle) / 2;

        }
        // Move the triangle according to the translation
        public void offset(AForge.IntPoint translation)
        {
            top += translation;
            base1 += translation;
            base2 += translation;
            center += translation;
        }
        //The areais calculated by using the crossProduct.
        public double getArea()
        {
            return crossProduct(base1-top,base2-top)/2;
        }
        //Returns the points of the Triangle
        public AForge.IntPoint[] getPoints()
        {
            return new AForge.IntPoint[]{top,base1,base2};
        }
        //Returns a rectangle that will include all the points and has 2 of its corners in the base points
        public Quadrilateral getRectangle()
        {
            //Return a Quadrilateral with corners in the base points and has the height the same as the triangle.
            AForge.IntPoint top1 = new AForge.IntPoint((int)Math.Round((base1.X + direction.X * height)),(int)Math.Round(base1.Y + direction.Y * height));
            AForge.IntPoint top2 = new AForge.IntPoint((int)Math.Round((base2.X + direction.X * height)),(int)Math.Round(base2.Y + direction.Y * height));
            AForge.IntPoint top3 = base2;
            AForge.IntPoint top4 = base1;
            return new Quadrilateral(new AForge.IntPoint[] { top3, top4, top1,top2});
        }
        //Return a set of points that could be used for drawing.
        public System.Drawing.Point[] getDrawingPoints()
        {
            return new System.Drawing.Point[]{convert(top),convert(base1),convert(base2),convert(top)};
        }
        //Returns true if it is exactly the same triangle.
        public Boolean Equals(Triangle t)
        {
            return top.Equals(t.TOP) && base1.Equals(t.BASE1) && base2.Equals(t.BASE2);
        }

        //compare to returns a value that is based on the error of points, sides, height and angles.
        //The error is the power of the e function to get a more stable value.
        public double compareTo(Triangle t)
        {
            Triangle mold = new Triangle(t.HEIGHT , t.LENGTH);
            Triangle thisTriangle = new Triangle(this.HEIGHT , this.LENGTH);
            
            AForge.IntPoint[] moldPoints = mold.getPoints();
            AForge.IntPoint[] thisPoints = thisTriangle.getPoints();

            double sideDiff0 = Math.Abs(this.SIDES[0] - mold.SIDES[0]);
            double sideDiff1 = Math.Abs(this.SIDES[1] - mold.SIDES[1]);
            double sideDiff2 = Math.Abs(this.SIDES[2] - mold.SIDES[2]);

             

            double error = Math.Exp(thisPoints[0].DistanceTo(moldPoints[0])) +
                    Math.Exp(thisPoints[1].DistanceTo(moldPoints[1])) +
                    Math.Exp(thisPoints[2].DistanceTo(moldPoints[2])) +
                    Math.Exp(thisTriangle.CENTER.DistanceTo(mold.CENTER)) +
                    Math.Exp(Math.Abs(thisTriangle.HEIGHT - mold.HEIGHT)) +
                    Math.Exp(sideDiff0) +
                    Math.Exp(sideDiff1) +
                    Math.Exp(sideDiff2);

            return error;
        }
        //A smal funciton that convert a AForge point to a Drawing point
        private System.Drawing.Point convert(AForge.IntPoint p)
        {
            return new System.Drawing.Point(p.X,p.Y);
        } 
        private double crossProduct(AForge.IntPoint a, AForge.IntPoint b)
        {
            try
            {
                return Math.Abs(a.X * b.Y - a.Y * b.X);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception! Should be investigated");
                Debug.WriteLine(e.ToString());
                return 0;
            }
        }
        private double angle(AForge.IntPoint a, AForge.IntPoint b)
        {
            double scalarProduct = (a.X * b.X + a.Y * b.Y) / a.EuclideanNorm() / b.EuclideanNorm();
            return Math.Acos(scalarProduct);
        }
        private AForge.Point giveOrthogonol(AForge.Point p)
        {
            return new AForge.Point(-p.Y, p.X);
        }
    }
    /*
//Compare if the triangle should be considered equal to the ideal triangle.
//Is not in use.
public bool compare( Triangle ideal)
{

    double diffHeight = Math.Abs(this.HEIGHT - ideal.HEIGHT);
    double diffLength = Math.Abs(this.LENGTH - ideal.LENGTH);

    double topAngleDiff = Math.Abs(this.TOP_ANGLE - ideal.TOP_ANGLE);
    double baseAngleDiff1 = Math.Abs(this.baseAngle1 - ideal.baseAngle1);
    double baseAngleDiff2 = Math.Abs(this.baseAngle1 - ideal.baseAngle2);


    double sideDiff0 = Math.Abs(this.SIDES[0] - ideal.SIDES[0]);
    double sideDiff1 = Math.Abs(this.SIDES[1] - ideal.SIDES[1]);
    double sideDiff2 = Math.Abs(this.SIDES[2] - ideal.SIDES[2]);

    double errorHeight = diffHeight / ideal.HEIGHT;
    double errorLength = diffLength / ideal.LENGTH;

    double errorTopAngle = topAngleDiff / ideal.TOP_ANGLE;
    double errorBaseAngle1 = baseAngleDiff1 / ideal.baseAngle1;
    double errorBaseAngle2 = baseAngleDiff2 / ideal.baseAngle2;

    double errorSide0 = sideDiff0 / ideal.SIDES[0];
    double errorSide1 = sideDiff1 / ideal.SIDES[1];
    double errorSide2 = sideDiff2 / ideal.SIDES[2];


    if (errorHeight < 0.1 && errorLength < 0.1 && errorTopAngle < 1 && errorBaseAngle1 < 0.1 && errorBaseAngle2 < 0.1 && errorSide0 < 0.17 && errorSide1 < 0.17 && errorSide2 < 0.17)
    {
        return true;
    }
    return false;
}
*/
}
