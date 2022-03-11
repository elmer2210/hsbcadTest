using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace hsbcadTest
{
    public class HsbTriangle : IHsbShape
    {
        int _iCanvasXMinLimit = 1;
        int _iCanvasXMaxLimit = 900;

        int _iCanvasYMinLimit = 1;
        int _iCanvasYMaxLimit = 610;

        #region Properties

        public int Id { get; set; } = 0;
        public HsbShapeType ShapeType { get; set; } = HsbShapeType.Triangle;
        public int[] Pt1 { get; set; } = new int[2];
        public int[] Pt2 { get; set; } = new int[2];
        public int[] Pt3 { get; set; } = new int[2];
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
            int x = random1.Next(_iCanvasXMinLimit, _iCanvasXMaxLimit );
            int y = random1.Next(_iCanvasYMinLimit, _iCanvasYMaxLimit );

            //point Two
            float x1 = 0;
            float y1 = 0;

            //point Three
            float x3 = 0;
            float y3 = 0;
            
            //center Geomety
            float xCenter = 0;
            float yCenter = 0;

            float fBase = 0;
            float fHeight = 0;

            Func<float, float, float, float, float> calculateDistance = (px1, py1, px2, py2) => (float)(Math.Sqrt(((px1 - px2) * (px1 - px2)) + ((py1 - py2) * (py1 - py2))));

            //The base has been determined according to the area of the polygon and the radius of a triangle inscribed in a circle.
            //This number is the maximum to create an equilateral triangle

            do
            {
                x1 = random2.Next(settings.AverageRadius, _iCanvasXMaxLimit );
                y1 = random2.Next(_iCanvasYMinLimit, _iCanvasYMaxLimit );

                fBase = calculateDistance(x, y, x1, y1);
            } while (fBase > 180);


            //this point refers to half the distance between the two previous points
            float x2 = (x1 + x) / 2;
            float y2 = (y1 + y) / 2;

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
            Func<float, float> Fx = (px3) => ((m2 * px3) - m2 * x2 + y2);

            //find y3 based on x2

            int polarity = 0;
            if (m2 >= 0)
            {
                // if m2 is positive then search positive numbers
                var heightDistance = settings.AverageRadius * 3 * 0.5;
                x3 = x2;

                while (fHeight < heightDistance)
                {
                    x3++;
                    y3 = Fx(x3);

                    fHeight = calculateDistance(x3, y3, x2, y2);
                    polarity = 1;

                }
            }
            else
            {
                // if m2 is negative then search negative numbers
                var heightDistance = settings.AverageRadius * 3 * 0.5;
                x3 = x2;

                while (fHeight < heightDistance + 2)
                {
                    x3--;
                    y3 = Fx(x3);

                    fHeight = calculateDistance(x3, y3, x2, y2);
                    polarity = 2;
                }
            }





            if (x3 > _iCanvasXMaxLimit || y3 > _iCanvasYMaxLimit)
            {
                if (polarity == 1)
                {
                    // if m2 is positive then search positive numbers
                    var heightDistance = settings.AverageRadius * 3 * 0.5;
                    x3 = x2;

                    while (fHeight < heightDistance)
                    {
                        x3--;
                        y3 = Fx(x3);

                        fHeight = calculateDistance(x3, y3, x2, y2);
                        polarity = 2;

                    }
                }
                if (polarity == 2)
                {
                    // if m2 is positive then search positive numbers
                    var heightDistance = settings.AverageRadius * 3 * 0.5;
                    x3 = x2;

                    while (fHeight < heightDistance)
                    {
                        x3++;
                        y3 = Fx(x3);

                        fHeight = calculateDistance(x3, y3, x2, y2);


                        polarity = 1;

                    }
                }
            }

            if (x3 < 0)
            {
                x3 = (-1) * x3;
                y3 = Fx(x3);
            }


            if (y3 > _iCanvasYMaxLimit)
            {
                var diference = y3 - y2;
                y3 = y2 - diference;
            }


            //find yCenter based on x2
            //if (polarity == 1)
            //{
            //    // if m2 is positive then search positive numbers
            //    var CenterDistance = settings.AverageRadius / 3;
            //    xCenter = x2;
            //    fHeight = 0;

            //    while (fHeight < CenterDistance)
            //    {
            //        xCenter++;
            //        yCenter = Fx(xCenter);
            //        fHeight = calculateDistance(xCenter, yCenter, x2, y2);
            //    }
            //}

            //find yCenter based on x2
            /*if (m2 >= 0)
            {
                // if m2 is positive then search positive numbers
                var CenterDistance = settings.AverageRadius * 0.5;
                xCenter = x2;

                while (fHeight < CenterDistance)
                {
                    xCenter++;
                    yCenter = Fx(xCenter);
                    fHeight = calculateDistance(xCenter, yCenter, x2, y2);
                }
            }
            else
            {
                // if m2 is negative then search negative numbers
                var CenterDistance = settings.AverageRadius * 0.5;
                xCenter = x2;

                while (fHeight < CenterDistance)
                {
                    xCenter--;
                    yCenter = Fx(xCenter);
                    fHeight = calculateDistance(xCenter, yCenter, x2, y2);
                }
            }*/

            xCenter = (x + x1 + x3) / 3;
            yCenter = (y + y1 + y3) / 3;

            //Save all point into class

            this.Pt1[0] = x;
            this.Pt1[1] = y;

            this.Pt2[0] = Convert.ToInt32(x1);
            this.Pt2[1] = Convert.ToInt32(y1);

            this.Pt3[0] = Convert.ToInt32(x3);
            this.Pt3[1] = Convert.ToInt32(y3);
            this.Area = fBase * fHeight / 2;

            this.GeometricCenterPoint[0] = Convert.ToInt32(xCenter);
            this.GeometricCenterPoint[1] = Convert.ToInt32(yCenter);
        }

        #endregion
    }
}
