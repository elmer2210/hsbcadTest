using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsbcadTest
{
    public class HsbListShapes
    {
        public List<HsbTriangle> HsbTrianglesList { get; set; } = new List<HsbTriangle>();
        public List<HsbRectangle> HsbRectanglesList { get; set; } = new List<HsbRectangle>();
        public List<HsbRhombus> HsbRhombusList { get; set; } = new List<HsbRhombus>();
        public List<HsbCircle> HsbCirclesList { get; set; } = new List<HsbCircle>();
        public List<HsbEllipse> HsbEllipsesList { get; set; } = new List<HsbEllipse>();
    }
}
