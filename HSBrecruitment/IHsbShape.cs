using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsbcadTest
{
    interface IHsbShape
    {
        int Id { get; set; }

        HsbShapeType ShapeType { get; set; }

        int[] Pt1 { get; set; }

        int[] Pt2 { get; set; }

        int[] Pt3 { get; set; }

        int[] GeometricCenterPoint { get; set; }

        void CreatePoints();
    }
}
