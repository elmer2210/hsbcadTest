using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace hsbcadTest
{
    public class HsbRhombus : IHsbShape
    {
        int _iCanvasXMinLimit = 1;
        int _iCanvasXMaxLimit = 900;

        int _iCanvasYMinLimit = 1;
        int _iCanvasYMaxLimit = 610;

        #region Properties

        public int Id { get; set; } = 0;
        public HsbShapeType ShapeType { get; set; } = HsbShapeType.Rhombus;
        public int[] Pt1 { get; set; } = new int[2];
        public int[] Pt2 { get; set; } = new int[2];
        public int[] Pt3 { get; set; } = new int[2];
        public int[] Pt4 { get; set; } = new int[2];
        public double Area { get; set; } = 0.0;
        public int[] GeometricCenterPoint { get; set; } = new int[2];

        #endregion

        #region Create Points

        public void CreatePoints()
        {
            Random random1 = new Random(DateTime.Now.Millisecond);

            Thread.Sleep(10);
            Random random2 = new Random(DateTime.Now.Millisecond);


            ShapeSettings settings = new ShapeSettings();

            ///////////////////////WARNING////////////////////////////
            //the construction of this triangle is based on the data provided by "ShapeSettings",
            //which allow us to build an equilateral triangle

            //point One
            int x = random1.Next(_iCanvasXMinLimit + settings.AverageRadius, _iCanvasXMaxLimit - settings.AverageRadius);
            int y = random1.Next(_iCanvasYMinLimit + settings.AverageRadius, _iCanvasYMaxLimit - settings.AverageRadius);

            Debug.WriteLine($"x: {x} - y: {y}");

            //point Two
            float y1 = 0;
            float x1 = 0;

            //point Three
            float y3 = 0;
            float x3 = 0;

            //point Four
            float y4 = 0;
            float x4 = 0;

            float majorDiagonal = 0;
            float minusDiagonal = 0;

            Func<float, float, float, float, float> d = (px1, py1, px2, py2) => (float)Math.Sqrt(((px1 - px2) * (px1 - px2)) + ((py1 - py2) * (py1 - py2)));

            //The base has been determined according to the area of the polygon and the radius of a triangle inscribed in a circle.
            //This number is the maximum to create an equilateral triangle

            do
            {
                x1 = random2.Next(_iCanvasXMinLimit + settings.AverageRadius, _iCanvasXMaxLimit - settings.AverageRadius);
                y1 = random2.Next(_iCanvasYMinLimit + settings.AverageRadius, _iCanvasYMaxLimit - settings.AverageRadius);

                while (y1 <= 0 && y1 >= _iCanvasYMaxLimit)
                {
                    y1 = random2.Next(_iCanvasYMinLimit, _iCanvasYMaxLimit - settings.AverageRadius);
                }

                while (x1 <= 0)
                {
                    x1 = random2.Next(_iCanvasXMinLimit + settings.AverageRadius, _iCanvasXMaxLimit);
                }

                majorDiagonal = d(x, y, x1, y1);
            }
            while (majorDiagonal > settings.AverageDiagonal);

            //this point refers to half the distance between the two previous points
            float y2 = (y1 + y) / 2;
            float x2 = (x1 + x) / 2;

            //Between point one and two we will obtain the angle of inclination
            // m = tg(α)
            float m = (y1 - y) / (x1 - x);

            //To create a function that is perpendicular, we must invert the function of the slope
            float m2 = 0;

            if (m != 0)
                m2 = -1 / m;
            else
                m2 = -1;

            //We use function point at slope
            Func<float, float> Fx = (px3) => (float)((m2 * px3) - m2 * x2 + y2);
           // Func<float, float> Fy = (py3) => (float)((py3 - m2 * x2 + y2)/ m2);

            //find y3 based on x2

            // if m2 is positive then search positive numbers
            var heightDistance = d(x, y, x1, y1) / 2;
            float SuperiorMinorDiagonal = 0;
            x3 = x2;

            while (SuperiorMinorDiagonal < heightDistance-1)
            {
                x3++;
                y3 = Fx(x3);
                
                SuperiorMinorDiagonal = d(x3, y3, x2, y2);
            }

            // if m2 is negative then search negative numbers
            float LowerMinorDiagonal = 0;

            x4 = x2;

            while (LowerMinorDiagonal < heightDistance -1)
            {
                x4--;
                y4 = Fx(x4);

                LowerMinorDiagonal = d(x4, y4, x2, y2);
            }

           minusDiagonal = LowerMinorDiagonal + SuperiorMinorDiagonal;

            //Save all point into class

            this.Pt1[0] = x;
            this.Pt1[1] = y;

            this.Pt2[0] = Convert.ToInt32(x4);
            this.Pt2[1] = Convert.ToInt32(y4);

            this.Pt3[0] = Convert.ToInt32(x1);
            this.Pt3[1] = Convert.ToInt32(y1);

            this.Pt4[0] = Convert.ToInt32(x3);
            this.Pt4[1] = Convert.ToInt32(y3);

            this.Area = majorDiagonal * minusDiagonal * 0.5;

            this.GeometricCenterPoint[0] = Convert.ToInt32(x2);
            this.GeometricCenterPoint[1] = Convert.ToInt32(y2);

        }

        #endregion
    }
}
