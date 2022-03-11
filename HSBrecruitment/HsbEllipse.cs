using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace hsbcadTest
{
    public class HsbEllipse
    {
        int _iXMinLimit = 120;
        int _iXMaxLimit = 630;

        int _iYMinLimit = 270;
        int _iYMaxLimit = 340;


        int _iCanvasXMaxLimit = 900;

        #region Properties

        public int Id { get; set; } = 0;
        public HsbShapeType ShapeType { get; set; } = HsbShapeType.Ellipse;
        public int[] PtLeftTop { get; set; } = new int[2];
        public double WidthRadius { get; set; } = 0.0;
        public double HeightRadius { get; set; } = 0.0;
        public double Area { get; set; } = 0.0;
        public int[] GeometricCenterPoint { get; set; } = new int[2];

        #endregion

        #region Create Points

        public void CreatePoint()
        {
            Random random1 = new Random(DateTime.Now.Millisecond);

            Thread.Sleep(10);
            Random random2 = new Random(DateTime.Now.Millisecond);

            Random random3 = new Random(DateTime.Now.Millisecond);

            ShapeSettings settings = new ShapeSettings();

            ///////////////////////WARNING////////////////////////////
            //the construction of this circle is based on the data provided by "ShapeSettings",

            //Point One
            int x = random1.Next(_iXMinLimit, _iXMaxLimit);
            int y = random1.Next(_iYMinLimit, _iYMaxLimit);

            //Point Larger Radius
            int heigthRadius = 0;

            //Point Minor Radius
            int x1 = 0;
            int y1 = 0;

            Func<float, float, float, float, float> calculateDistance = (px1, py1, px2, py2) => (float)(Math.Sqrt(((px1 - px2) * (px1 - px2)) + ((py1 - py2) * (py1 - py2))));
            
            float fWidthRadius = 0;

            //Minor Radius
            do
            {
                x1 = random2.Next(x, _iCanvasXMaxLimit);
                y1 = y;

                fWidthRadius = calculateDistance(x, y, x1, y1);
            } 
            while (fWidthRadius > _iXMinLimit);

            //Larger Radius
            do
            {
                heigthRadius = random3.Next(1, Convert.ToInt32(fWidthRadius)+10);                 
            }
            while (heigthRadius > fWidthRadius);

            //circle´s radius 
            int widthRadius1 = Convert.ToInt32(fWidthRadius);

            int heigthRadius1 = Convert.ToInt32(heigthRadius);

            //PI value

            double PI = 3.141592;

            //Ellipse Area
            int ellipseArea = Convert.ToInt32(heigthRadius1 * widthRadius1 * PI);

            //Center Geometry
            int xCenter = x1;
            int yCenter = y1 + heigthRadius1;

            //Save all point into de properties
            this.PtLeftTop[0] = x;
            this.PtLeftTop[1] = y;

            this.WidthRadius = widthRadius1;
            this.HeightRadius = heigthRadius1;

            this.Area = ellipseArea;

            this.GeometricCenterPoint[0] = xCenter;
            this.GeometricCenterPoint[1] = yCenter;
        }

        #endregion
    }
}
