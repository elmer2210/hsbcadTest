using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace hsbcadTest
{
    public class HsbCircle
    {
        int _iXMinLimit = 270;
        int _iXMaxLimit = 630;

        int _iYMinLimit = 270;
        int _iYMaxLimit = 340;

        int _iCanvasXMaxLimit = 900;

        #region Properties

        public int Id { get; set; } = 0;
        public HsbShapeType ShapeType { get; set; } = HsbShapeType.Circle;
        public int[] PtLeftTop { get; set; } = new int[2];
        public double Radius { get; set; } = 0.0;
        public double Area { get; set; } = 0.0;
        public int[] GeometricCenterPoint { get; set; } = new int[2];

        #endregion

        #region Create Point

        public void CreatePoint()
        {
            Random random1 = new Random(DateTime.Now.Millisecond);

            Thread.Sleep(10);

            Random random2 = new Random(DateTime.Now.Millisecond);

            ShapeSettings settings = new ShapeSettings();

            ///////////////////////WARNING////////////////////////////
            //the construction of this circle is based on the data provided by "ShapeSettings",

            //Point One
            int x = random1.Next(_iXMinLimit, _iXMaxLimit);
            int y = random1.Next(_iYMinLimit, _iYMaxLimit);

            //Point Radius
            int x1 = 0;
            int y1 = 0;

            Func<float, float, float, float, float> calculateDistance = (px1, py1, px2, py2) => (float)(Math.Sqrt(((px1 - px2) * (px1 - px2)) + ((py1 - py2) * (py1 - py2))));

            float cDiameter = 0;

            //radius 
            do
            {
                x1 = random2.Next(x, _iCanvasXMaxLimit);
                y1 = y;

                cDiameter = calculateDistance(x, y, x1, y1);
            } 
            while (cDiameter > _iXMinLimit);

            //circle´s radius 
            int radius = Convert.ToInt32(cDiameter)/2;

            //PI value
            double PI = 3.141592;

            //Center Geometry
            int xCenter = x1 - radius;
            int yCenter = y1 + radius;

            //Save all point into the class
            this.PtLeftTop[0] = x;
            this.PtLeftTop[1] = y;

            this.Radius = radius;

            this.GeometricCenterPoint[0] = xCenter;
            this.GeometricCenterPoint[1] = yCenter;

            this.Area = PI * (radius * radius);
        }

        #endregion
    }
}
