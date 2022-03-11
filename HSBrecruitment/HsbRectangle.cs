using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace hsbcadTest
{
    public class HsbRectangle : IHsbShape
    {
        int _iCanvasXMinLimit = 1;
        int _iCanvasXMaxLimit = 900;

        int _iCanvasYMinLimit = 1;
        int _iCanvasYMaxLimit = 610;

        #region Properties

        public int Id { get; set; } = 0;
        public HsbShapeType ShapeType { get; set; } = HsbShapeType.Rectangle;
        public int[] Pt1 { get; set; } = new int[2];
        public int[] Pt2 { get; set; } = new int[2];
        public int[] Pt3 { get; set; } = new int[2];
        public int[] Pt4 { get; set; } = new int[2];
        public int[] GeometricCenterPoint { get; set; } = new int[2];
        public double Area { get; set; } = 0.0;

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
            int x = random1.Next(settings.AverageRadius, _iCanvasXMaxLimit - settings.AverageRadius);
            int y = random1.Next(settings.AverageRadius, _iCanvasYMaxLimit - settings.AverageRadius);

            //point Two
            float x1 = 0;
            float y1 = 0;

            //point Three
            float x3 = 0;
            float y3 = 0;

            //point Four
            float x4 = 0;
            float y4 = 0;

            //point Five
            float x5 = 0;
            float y5 = 0;

            //point Six
            float x6 = 0;
            float y6 = 0;

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
                x1 = random2.Next(settings.AverageRadius, _iCanvasXMaxLimit - settings.AverageRadius);
                y1 = random2.Next(settings.AverageRadius, _iCanvasYMaxLimit - settings.AverageRadius);

                fBase = calculateDistance(x, y, x1, y1);
            } while (fBase > settings.AverageDiagonal);


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
            Func<float, float, float, float> Fx = (px, px1, py1) => ((m2 * px) - m2 * px1 + py1);


            //find y3 based on x

            var heightDistance = settings.AverageRadius / 2;
            float UpperLeftSide = 0;
            x3 = x;

            while (UpperLeftSide < heightDistance - 1)
            {
                x3++;
                y3 = Fx(x3, x, y);

                UpperLeftSide = calculateDistance(x3, y3, x, y);
            }

            //find y4 based on x
            float LowerLeftSide = 0;

            x4 = x;

            while (LowerLeftSide < heightDistance - 1)
            {
                x4--;
                y4 = Fx(x4, x, y);

                LowerLeftSide = calculateDistance(x4, y4, x, y);
            }



            //find y5 based on x1

            float UpperRightSide = 0;
            x5 = x1;

            while (UpperRightSide < heightDistance - 1)
            {
                x5++;
                y5 = Fx(x5, x1, y1);

                UpperRightSide = calculateDistance(x5, y5, x1, y1);
            }

            //find y6 based on x
            float LowerRightSide = 0;

            x6 = x1;

            while (LowerRightSide < heightDistance - 1)
            {
                x6--;
                y6 = Fx(x6, x1, y1);

                LowerRightSide = calculateDistance(x6, y6, x1, y1);
            }









            fBase = LowerRightSide + UpperRightSide;
            fHeight = calculateDistance(x6, y6, x4, y4); ;


            this.Pt1[0] = Convert.ToInt32(x3);
            this.Pt1[1] = Convert.ToInt32(y3);

            this.Pt2[0] = Convert.ToInt32(x4);
            this.Pt2[1] = Convert.ToInt32(y4);

            this.Pt3[0] = Convert.ToInt32(x6);
            this.Pt3[1] = Convert.ToInt32(y6);

            this.Pt4[0] = Convert.ToInt32(x5);
            this.Pt4[1] = Convert.ToInt32(y5);

            this.GeometricCenterPoint[0] = Convert.ToInt32(x2);
            this.GeometricCenterPoint[1] = Convert.ToInt32(y2);

            this.Area = fBase * fHeight;
        }

        #endregion
    }
}
