using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsbcadTest
{
    class ShapeSettings
    {
        public double AreaTotal { get; set; } = 0.0;
        public double PercentageUse { get; set; } = 0.0;
        public double AvailableArea { get; set; } = 0.0;
        public double PolygonUnitArea { get; set; } = 0.0;
        public Int32 sideShape { get; set; } = 0;
        public Int32 AverageDiagonal { get; set; } = 0;
        public Int32 AverageRadius { get; set; } = 0;

        public ShapeSettings(double AreaTotal = 900*610, double PercentageUse = 80, int ShapesNumber = 20)
        {
            this.AreaTotal = AreaTotal;
            this.PercentageUse = PercentageUse * 2;
            this.AvailableArea = this.PercentageUse * this.AreaTotal / 100;
            this.PolygonUnitArea = this.AvailableArea / ShapesNumber;

            this.sideShape = Convert.ToInt32(Math.Sqrt(this.PolygonUnitArea));
            this.AverageDiagonal = Convert.ToInt32(Math.Sqrt((this.sideShape)*(this.sideShape)+(this.sideShape)*(this.sideShape)));
            this.AverageRadius = Convert.ToInt32(this.AverageDiagonal / 2);


        }


    }
}
