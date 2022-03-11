using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Newtonsoft.Json;

namespace hsbcadTest
{
    public sealed class HsbShapesController
    {
        #region Attributes

        private static HsbShapesController _instance = new HsbShapesController();

        public static HsbListShapes ListShapes { get; set; } = new HsbListShapes();

        public static double TotalArea { get; set; } = 610*900;

        public static double AreaSum { get; set; } = 0.0;

        #endregion

        #region ConstructorHsbShape

        private HsbShapesController()
        {}

        #endregion

        #region GetInstance

        public static HsbShapesController Instance
        {
            get { return _instance; }
        }

        #endregion

        #region Create Shape

        private Polygon CreateTriangle(int index, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (index < 1) return null;

            HsbTriangle triangle = new HsbTriangle();
            triangle.CreatePoints();
            triangle.Id = index;

            Polygon polygon = new Polygon();

            polygon.StrokeThickness = 1;

            SolidColorBrush triangleSolidColorBrush = new SolidColorBrush();
            triangleSolidColorBrush.Color = Color.FromRgb(249, 231, 159);
            triangleSolidColorBrush.Opacity = 0.5;
            polygon.Fill = triangleSolidColorBrush;

            SolidColorBrush triangleSolidColorStroke = new SolidColorBrush();
            triangleSolidColorStroke.Color = Color.FromRgb(252, 243, 207);
            triangleSolidColorStroke.Opacity = 0.5;
            polygon.Stroke = triangleSolidColorStroke;

            var p1 = new System.Windows.Point(triangle.Pt1[0], triangle.Pt1[1]);
            var p2 = new System.Windows.Point(triangle.Pt2[0], Convert.ToInt32(triangle.Pt2[1]));
            var p3 = new System.Windows.Point(triangle.Pt3[0], Convert.ToInt32(triangle.Pt3[1]));

            polygon.Points.Add(p1);
            polygon.Points.Add(p2);
            polygon.Points.Add(p3);

            AreaSum += triangle.Area;

            TextPopupTriangle(triangle, textBlock1, textBlock2, polygon);

            ListShapes.HsbTrianglesList.Add(triangle);

            return polygon;
        }

        private Polygon CreateRectangle(int index, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (index < 1) return null;

            HsbRectangle rectangle = new HsbRectangle();

            rectangle.CreatePoints();
            rectangle.Id = index;

            Polygon polygon = new Polygon();

            polygon.StrokeThickness = 1;

            SolidColorBrush rectangleSolidColorBrush = new SolidColorBrush();
            rectangleSolidColorBrush.Color = Color.FromRgb(133, 193, 233);
            rectangleSolidColorBrush.Opacity = 0.5;
            polygon.Fill = rectangleSolidColorBrush;

            SolidColorBrush rectangleSolidColorStroke = new SolidColorBrush();
            rectangleSolidColorStroke.Color = Color.FromRgb(46, 134, 193);
            polygon.Stroke = rectangleSolidColorStroke;

            polygon.Points.Add(new System.Windows.Point(rectangle.Pt1[0], rectangle.Pt1[1]));
            polygon.Points.Add(new System.Windows.Point(rectangle.Pt2[0], rectangle.Pt2[1]));
            polygon.Points.Add(new System.Windows.Point(rectangle.Pt3[0], rectangle.Pt3[1]));
            polygon.Points.Add(new System.Windows.Point(rectangle.Pt4[0], rectangle.Pt4[1]));



            AreaSum += rectangle.Area;

            TextPopupRectangle(rectangle, textBlock1, textBlock2, polygon);

            ListShapes.HsbRectanglesList.Add(rectangle);

            return polygon;
        }

        private Polygon CreateRhombus(int index, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (index < 1) return null;

            HsbRhombus rhombus = new HsbRhombus();

            rhombus.CreatePoints();
            rhombus.Id = index;

            Polygon polygon = new Polygon();

            polygon.StrokeThickness = 1;

            SolidColorBrush rhombusSolidColorBrush = new SolidColorBrush();
            rhombusSolidColorBrush.Color = Color.FromRgb(174, 213, 129);
            rhombusSolidColorBrush.Opacity = 0.5;
            polygon.Fill = rhombusSolidColorBrush;

            SolidColorBrush rhombusSolidColorStroke = new SolidColorBrush();
            rhombusSolidColorStroke.Color = Color.FromRgb(197, 225, 165);
            polygon.Stroke = rhombusSolidColorStroke;

            polygon.Points.Add(new System.Windows.Point(rhombus.Pt1[0], rhombus.Pt1[1]));
            polygon.Points.Add(new System.Windows.Point(rhombus.Pt2[0], rhombus.Pt2[1]));
            polygon.Points.Add(new System.Windows.Point(rhombus.Pt3[0], rhombus.Pt3[1]));
            polygon.Points.Add(new System.Windows.Point(rhombus.Pt4[0], rhombus.Pt4[1]));

            AreaSum += rhombus.Area;

            TextPopupRhombus(rhombus, textBlock1, textBlock2, polygon);

            ListShapes.HsbRhombusList.Add(rhombus);

            return polygon;
        }

        private Ellipse CreateCircle(int index, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (index < 1) return null;

            HsbCircle circle = new HsbCircle();

            circle.CreatePoint();
            circle.Id = index;

            Ellipse ellipse = new Ellipse();

            ellipse.StrokeThickness = 1;

            SolidColorBrush circlesSolidColorBrush = new SolidColorBrush();
            circlesSolidColorBrush.Color = Color.FromRgb(206, 147, 216);
            circlesSolidColorBrush.Opacity = 0.5;
            ellipse.Fill = circlesSolidColorBrush;

            SolidColorBrush circlesSolidColorStroke = new SolidColorBrush();
            circlesSolidColorStroke.Color = Color.FromRgb(186, 104, 200);
            ellipse.Stroke = circlesSolidColorStroke;

            ellipse.Width = circle.Radius*2;
            ellipse.Height = circle.Radius*2;

            Canvas.SetLeft(ellipse, circle.PtLeftTop[0]);
            Canvas.SetTop(ellipse, circle.PtLeftTop[1]);

            AreaSum += circle.Area;

            TextPopupCircles(circle, textBlock1, textBlock2, ellipse);

            ListShapes.HsbCirclesList.Add(circle);

            return ellipse;
        }

        private Ellipse CreateEllipse(int index, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (index < 1) return null;

            HsbEllipse oval = new HsbEllipse();

            oval.CreatePoint();
            oval.Id = index;

            Ellipse ellipse = new Ellipse();

            ellipse.StrokeThickness = 1;

            SolidColorBrush ovalSolidColorBrush = new SolidColorBrush();
            ovalSolidColorBrush.Color = Color.FromRgb(248, 187, 208);
            ellipse.Fill = ovalSolidColorBrush;

            SolidColorBrush ovalSolidColorStroke = new SolidColorBrush();
            ovalSolidColorStroke.Color = Color.FromRgb(240, 98, 146);
            ellipse.Stroke = ovalSolidColorStroke;

            ellipse.Width = oval.WidthRadius * 2;
            ellipse.Height = oval.HeightRadius * 2;

            Canvas.SetLeft(ellipse, oval.PtLeftTop[0]);
            Canvas.SetTop(ellipse, oval.PtLeftTop[1]);

            AreaSum += oval.Area;

            TextPopupEllipse(oval, textBlock1, textBlock2, ellipse);

            ListShapes.HsbEllipsesList.Add(oval);

            return ellipse;

        }

        #endregion

        #region Event

        static void TextPopupTriangle(HsbTriangle triangle, TextBlock textBlock1, TextBlock textBlock2, Polygon polygon)
        {
            // raised when mouse cursor enter the area occupied by the element

            void OnMouseEnterHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush TriangleSolidColorBrush = new SolidColorBrush();
                TriangleSolidColorBrush.Color = Color.FromRgb(247, 220, 111);
                TriangleSolidColorBrush.Opacity = 0.5;
                polygon.Fill = TriangleSolidColorBrush;

                SolidColorBrush TriangleSolidColorStroke = new SolidColorBrush();
                TriangleSolidColorStroke.Color = Color.FromRgb(249, 231, 159);
                TriangleSolidColorStroke.Opacity = 0.5;
                polygon.Stroke = TriangleSolidColorStroke;

                textBlock1.Text = $"Total area: {TotalArea} {Environment.NewLine}" +
                                  $"Indice Shape: {triangle.Id} {Environment.NewLine}" +
                                  $"Point One: ( { Convert.ToString(triangle.Pt1[0])}; {Convert.ToString(triangle.Pt1[1])} ). {Environment.NewLine}" +
                                  $"Point Three: ( {Convert.ToString(triangle.Pt3[0])}; {Convert.ToString(triangle.Pt3[1])} ). {Environment.NewLine}";

                textBlock2.Text = $"Total sum of Areas: {AreaSum.ToString("N2")} Total percentage used:{(100*AreaSum/TotalArea).ToString("N2")}% {Environment.NewLine}" +
                                 $"Shape Type: {triangle.ShapeType} {Environment.NewLine}" +
                                 $"Point Two: ( { Convert.ToString(triangle.Pt2[0])}; {Convert.ToString(triangle.Pt2[1])} ). {Environment.NewLine}" +
                                 $"Shape Area: {triangle.Area}";

            }
            // raised when mouse cursor leaves the area occupied by the element
            void OnMouseLeaveHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush TriangleSolidColorBrush = new SolidColorBrush();
                TriangleSolidColorBrush.Color = Color.FromRgb(249, 231, 159);
                TriangleSolidColorBrush.Opacity = 0.5;
                polygon.Fill = TriangleSolidColorBrush;

                SolidColorBrush TriangleSolidColorStroke = new SolidColorBrush();
                TriangleSolidColorStroke.Color = Color.FromRgb(252, 243, 207);
                TriangleSolidColorStroke.Opacity = 0.5;
                polygon.Stroke = TriangleSolidColorStroke;

                textBlock1.Text = "";
                textBlock2.Text = "";
            }

            polygon.MouseEnter += OnMouseEnterHandler;
            polygon.MouseLeave += OnMouseLeaveHandler;

        }

        static void TextPopupRectangle(HsbRectangle rectangle, TextBlock textBlock1, TextBlock textBlock2, Polygon polygon)
        {
            // raised when mouse cursor enter the area occupied by the element
            void OnMouseEnterHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush RectangleSolidColorBrush = new SolidColorBrush();
                RectangleSolidColorBrush.Color = Color.FromRgb(93, 173, 226);
                RectangleSolidColorBrush.Opacity = 0.5;
                polygon.Fill = RectangleSolidColorBrush;

                SolidColorBrush RectangleSolidColorStroke = new SolidColorBrush();
                RectangleSolidColorStroke.Color = Color.FromRgb(40, 116, 166);
                RectangleSolidColorStroke.Opacity = 0.5;
                polygon.Stroke = RectangleSolidColorStroke;

                textBlock1.Text = $"Total area: {TotalArea} {Environment.NewLine}" +
                                  $"Indice Shape: {rectangle.Id} {Environment.NewLine}" +
                                  $"Point One: ( { Convert.ToString(rectangle.Pt1[0])}; {Convert.ToString(rectangle.Pt1[1])} ). {Environment.NewLine}" +
                                  $"Point Three: ( {Convert.ToString(rectangle.Pt3[0])}; {Convert.ToString(rectangle.Pt3[1])} ). {Environment.NewLine}" +
                                  $"Shape Area: {rectangle.Area}";
                textBlock2.Text = $"Total sum of Areas: {AreaSum.ToString("N2")} Total percentage used:{(100*AreaSum/TotalArea).ToString("N2")}% {Environment.NewLine}" +
                                  $"Shape Type: {rectangle.ShapeType} {Environment.NewLine}" +
                                  $"Point Two: ( { Convert.ToString(rectangle.Pt2[0])}; {Convert.ToString(rectangle.Pt2[1])} ). {Environment.NewLine}" +
                                  $"Point Four: ({Convert.ToString(rectangle.Pt4[0])}; {Convert.ToString(rectangle.Pt4[1])} ). {Environment.NewLine}";
            }
            // raised when mouse cursor leaves the area occupied by the element
            void OnMouseLeaveHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush RectangleSolidColorBrush = new SolidColorBrush();
                RectangleSolidColorBrush.Color = Color.FromRgb(133, 193, 233);
                RectangleSolidColorBrush.Opacity = 0.5;
                polygon.Fill = RectangleSolidColorBrush;

                SolidColorBrush RectangleSolidColorStroke = new SolidColorBrush();
                RectangleSolidColorStroke.Color = Color.FromRgb(46, 134, 193);
                RectangleSolidColorStroke.Opacity = 0.5;
                polygon.Stroke = RectangleSolidColorStroke;

                textBlock1.Text = "";
                textBlock2.Text = "";
            }

            polygon.MouseEnter += OnMouseEnterHandler;
            polygon.MouseLeave += OnMouseLeaveHandler;

            
        }

        static void TextPopupRhombus(HsbRhombus rhombus, TextBlock textBlock1, TextBlock textBlock2 , Polygon polygon)
        {
            // raised when mouse cursor enter the area occupied by the element
            void OnMouseEnterHandler(object sender, MouseEventArgs e)
            {
                SolidColorBrush RhombusSolidColorBrush = new SolidColorBrush();
                RhombusSolidColorBrush.Color = Color.FromRgb(156, 204, 101);
                RhombusSolidColorBrush.Opacity = 0.5;
                polygon.Fill = RhombusSolidColorBrush;

                SolidColorBrush RhombusSolidColorStroke = new SolidColorBrush();
                RhombusSolidColorStroke.Color = Color.FromRgb(104, 159, 56);
                RhombusSolidColorStroke.Opacity = 0.5;
                polygon.Stroke = RhombusSolidColorStroke;



                textBlock1.Text = $"Total area: {TotalArea} {Environment.NewLine}" +
                                  $"Indice Shape: {rhombus.Id} {Environment.NewLine}" +
                                  $"Point One: ( { Convert.ToString(rhombus.Pt1[0])}; {Convert.ToString(rhombus.Pt1[1])} ).{Environment.NewLine}" +
                                  $"Point Three: ( {Convert.ToString(rhombus.Pt3[0])}; {Convert.ToString(rhombus.Pt3[1])} ).{Environment.NewLine}" +
                                  $"Shape Area: {rhombus.Area}";

                textBlock2.Text = $"Total sum of Areas: {AreaSum.ToString("N2")} Total percentage used:{(100*AreaSum/TotalArea).ToString("N2")}% {Environment.NewLine}" +
                                  $"Shape Type: {rhombus.ShapeType} {Environment.NewLine}" +
                                  $"Point Two: ( { Convert.ToString(rhombus.Pt2[0])}; {Convert.ToString(rhombus.Pt2[1])} ).{Environment.NewLine}" +
                                  $"Point Four: ({Convert.ToString(rhombus.Pt4[0])}; {Convert.ToString(rhombus.Pt4[1])} ) ";
            }
            // raised when mouse cursor leaves the area occupied by the element
            void OnMouseLeaveHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush RhombusSolidColorBrush = new SolidColorBrush();
                RhombusSolidColorBrush.Color = Color.FromRgb(174, 213, 129);
                RhombusSolidColorBrush.Opacity = 0.5;
                polygon.Fill = RhombusSolidColorBrush;

                SolidColorBrush RhombusSolidColorStroke = new SolidColorBrush();
                RhombusSolidColorStroke.Color = Color.FromRgb(197, 225, 165);
                RhombusSolidColorStroke.Opacity = 0.5;
                polygon.Stroke = RhombusSolidColorStroke;
                textBlock1.Text = "";
                textBlock2.Text = "";
            }

            polygon.MouseEnter += OnMouseEnterHandler;
            polygon.MouseLeave += OnMouseLeaveHandler; 


        }

        static void TextPopupCircles(HsbCircle circle, TextBlock textBlock1, TextBlock textBlock2, Ellipse ellipse)
        {
            // raised when mouse cursor enter the area occupied by the element
            void OnMouseEnterHandler(object sender, MouseEventArgs e)
            {
                SolidColorBrush CircleSolidColorBrush = new SolidColorBrush();
                CircleSolidColorBrush.Color = Color.FromRgb(225, 190, 231);
                CircleSolidColorBrush.Opacity = 0.5;
                ellipse.Fill = CircleSolidColorBrush;

                SolidColorBrush CircleSolidColorStroke = new SolidColorBrush();
                CircleSolidColorStroke.Color = Color.FromRgb(186, 104, 200);
                ellipse.Stroke = CircleSolidColorStroke;



                textBlock1.Text = $"Total area: {TotalArea} {Environment.NewLine}" +
                                  $"Indice Shape: {circle.Id} {Environment.NewLine}" +
                                  $"Shape Area: {circle.Area}";

                textBlock2.Text = $"Total sum of Areas: {AreaSum.ToString("N2")} Total percentage used:{(100 * AreaSum / TotalArea).ToString("N2")}% {Environment.NewLine}" +
                                  $"Shape Type: {circle.ShapeType} {Environment.NewLine}" +
                                  $"Radius: {circle.Radius} {Environment.NewLine}" +
                                  $"Geometric Center Point: ( {circle.GeometricCenterPoint[0]}, {circle.GeometricCenterPoint[1]} )";
            }
            // raised when mouse cursor leaves the area occupied by the element
            void OnMouseLeaveHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush circlesSolidColorBrush = new SolidColorBrush();
                circlesSolidColorBrush.Color = Color.FromRgb(206, 147, 216);
                circlesSolidColorBrush.Opacity = 0.5;
                ellipse.Fill = circlesSolidColorBrush;

                SolidColorBrush circlesSolidColorStroke = new SolidColorBrush();
                circlesSolidColorStroke.Color = Color.FromRgb(186, 104, 200);
                ellipse.Stroke = circlesSolidColorStroke;
                textBlock1.Text = "";
                textBlock2.Text = "";
            }

            ellipse.MouseEnter += OnMouseEnterHandler;
            ellipse.MouseLeave += OnMouseLeaveHandler;


        }

        static void TextPopupEllipse(HsbEllipse oval, TextBlock textBlock1, TextBlock textBlock2, Ellipse ellipse)
        {
            // raised when mouse cursor enter the area occupied by the element
            void OnMouseEnterHandler(object sender, MouseEventArgs e)
            {
                SolidColorBrush OvalSolidColorBrush = new SolidColorBrush();
                OvalSolidColorBrush.Color = Color.FromRgb(248, 187, 208);
                OvalSolidColorBrush.Opacity = 0.5;
                ellipse.Fill = OvalSolidColorBrush;

                SolidColorBrush OvalSolidColorStroke = new SolidColorBrush();
                OvalSolidColorStroke.Color = Color.FromRgb(240, 98, 146);
                ellipse.Stroke = OvalSolidColorStroke;



                textBlock1.Text = $"Total area: {TotalArea} {Environment.NewLine}" +
                                  $"Indice Shape: {oval.Id} {Environment.NewLine}" +
                                  $"Heigth Radius: {oval.HeightRadius} {Environment.NewLine}" +
                                  $"Shape Area: {oval.Area}";

                textBlock2.Text = $"Total sum of Areas: {AreaSum.ToString("N2")} Total percentage used:{(100 * AreaSum / TotalArea).ToString("N2")}% {Environment.NewLine}" +
                                  $"Shape Type: {oval.ShapeType} {Environment.NewLine}" +
                                  $"width Radius: {oval.WidthRadius} {Environment.NewLine}" +
                                  $"Geometric Center Point: ( {oval.GeometricCenterPoint[0]}, {oval.GeometricCenterPoint[1]} )";
            }
            // raised when mouse cursor leaves the area occupied by the element
            void OnMouseLeaveHandler(object sender, MouseEventArgs e)
            {

                SolidColorBrush ovalSolidColorBrush = new SolidColorBrush();
                ovalSolidColorBrush.Color = Color.FromRgb(244, 143, 177);
                ovalSolidColorBrush.Opacity = 0.5;
                ellipse.Fill = ovalSolidColorBrush;

                SolidColorBrush ovalSolidColorStroke = new SolidColorBrush();
                ovalSolidColorStroke.Color = Color.FromRgb(240, 98, 146);
                ellipse.Stroke = ovalSolidColorStroke;
                textBlock1.Text = "";
                textBlock2.Text = "";
            }

            ellipse.MouseEnter += OnMouseEnterHandler;
            ellipse.MouseLeave += OnMouseLeaveHandler;
        }

        #endregion

        #region Run

        public void DrawShapes(Canvas canvas1, TextBlock textBlock1, TextBlock textBlock2)
        {
            //We always need to control that the objects are not null.
            if (canvas1 == null) return;

            canvas1.Children.Clear();
            AreaSum = 0.0;

            ListShapes.HsbTrianglesList.Clear();
            ListShapes.HsbRectanglesList.Clear();
            ListShapes.HsbRhombusList.Clear();
            ListShapes.HsbCirclesList.Clear();
            ListShapes.HsbEllipsesList.Clear();

            for (int i = 1; i <= 25; i++)
            {
                //It waits 1/2 second.
                
                Thread.Sleep(500);

                Random rnd = new Random();

                var type = (HsbShapeType)rnd.Next(1,6);

                Polygon  polygonAndHsbShape = null;

                switch (type)
                {
                    case HsbShapeType.Triangle:
                        
                        polygonAndHsbShape = CreateTriangle(i , textBlock1, textBlock2);
                        
                        break;

                    case HsbShapeType.Rectangle:
                       
                        polygonAndHsbShape = CreateRectangle(i, textBlock1, textBlock2);

                        break;

                    case HsbShapeType.Rhombus:

                        polygonAndHsbShape = CreateRhombus(i, textBlock1, textBlock2);
                        
                        break;
                    case HsbShapeType.Circle:
                        canvas1.Children.Add(CreateCircle(i, textBlock1, textBlock2));
                        break;
                    case HsbShapeType.Ellipse:
                        canvas1.Children.Add(CreateEllipse(i, textBlock1, textBlock2));
                        break;
                }

                if (polygonAndHsbShape == null) continue;
                
                canvas1.Children.Add(polygonAndHsbShape);

              
            }
        }

        public void DrawPointsTriangles(Canvas canvas2) 
        {
            for (int i = 0; i < ListShapes.HsbTrianglesList.Count; i++)
            {
                var triangle = ListShapes.HsbTrianglesList[i];

                //Point one
                Ellipse corner = new Ellipse();
                corner.Height = 8;
                corner.Width = 8;

                Canvas.SetLeft(corner, triangle.Pt1[0]-4);
                Canvas.SetTop(corner, triangle.Pt1[1]-4);

                SolidColorBrush TriangleSolidColorBrush = new SolidColorBrush();
                TriangleSolidColorBrush.Color = Color.FromRgb(244, 208, 63);
                corner.Fill = TriangleSolidColorBrush;

                //Point two
                Ellipse corner1 = new Ellipse();
                corner1.Height = 8;
                corner1.Width = 8;

                Canvas.SetLeft(corner1, triangle.Pt2[0]-4);
                Canvas.SetTop(corner1, Convert.ToInt32( triangle.Pt2[1])-4);

                SolidColorBrush TriangleSolidColorBrush1 = new SolidColorBrush();
                TriangleSolidColorBrush1.Color = Color.FromRgb(244, 208, 63);
                corner1.Fill = TriangleSolidColorBrush1;

                //Point three
                Ellipse corner2 = new Ellipse();
                corner2.Height = 8;
                corner2.Width = 8;

                Canvas.SetLeft(corner2, triangle.Pt3[0]-4);
                Canvas.SetTop(corner2, Convert.ToInt32(triangle.Pt3[1])-4);

                SolidColorBrush TriangleSolidColorBrush2 = new SolidColorBrush();
                TriangleSolidColorBrush2.Color = Color.FromRgb(244, 208, 63);
                corner2.Fill = TriangleSolidColorBrush2;
                
                //Center Point
                Ellipse center = new Ellipse();
                center.Height = 8;
                center.Width = 8;

                Canvas.SetLeft(center, triangle.GeometricCenterPoint[0]-4);
                Canvas.SetTop(center, triangle.GeometricCenterPoint[1]-4);

                SolidColorBrush TriangleSolidCenter = new SolidColorBrush();
                TriangleSolidCenter.Color = Color.FromRgb(36, 113, 163);
                center.Fill = TriangleSolidCenter;

                //Text Indice
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Convert.ToString(triangle.Id);
                
                Canvas.SetLeft(textBlock, triangle.GeometricCenterPoint[0]-14);
                Canvas.SetTop(textBlock, triangle.GeometricCenterPoint[1]-14);


                canvas2.Children.Add(corner);
                canvas2.Children.Add(corner1);
                canvas2.Children.Add(corner2);
                canvas2.Children.Add(center);
                canvas2.Children.Add(textBlock);
                
            }
        }

        public void DrawPointsRectangle(Canvas canvas2)
        {
            for (int i = 0; i < ListShapes.HsbRectanglesList.Count; i++)
            {
                var rectangle = ListShapes.HsbRectanglesList[i];

                //Point one
                Ellipse corner = new Ellipse();
                corner.Height = 8;
                corner.Width = 8;

                Canvas.SetLeft(corner, rectangle.Pt1[0]-4);
                Canvas.SetTop(corner, rectangle.Pt1[1]-4);

                SolidColorBrush RectangleSolidColorBrush = new SolidColorBrush();
                RectangleSolidColorBrush.Color = Color.FromRgb(75, 0, 130);
                corner.Fill = RectangleSolidColorBrush;



                //Point two
                Ellipse corner1 = new Ellipse();
                corner1.Height = 8;
                corner1.Width = 8;

                Canvas.SetLeft(corner1, rectangle.Pt2[0]-4);
                Canvas.SetTop(corner1, rectangle.Pt2[1]-4);

                SolidColorBrush RectangleSolidColorBrush1 = new SolidColorBrush();
                RectangleSolidColorBrush1.Color = Color.FromRgb(75, 0, 130);
                corner1.Fill = RectangleSolidColorBrush1;


                //Point three
                Ellipse corner2 = new Ellipse();
                corner2.Height = 8;
                corner2.Width = 8;

                Canvas.SetLeft(corner2, rectangle.Pt3[0]-4);
                Canvas.SetTop(corner2, rectangle.Pt3[1]-4);

                SolidColorBrush RectangleSolidColorBrush2 = new SolidColorBrush();
                RectangleSolidColorBrush2.Color = Color.FromRgb(75, 0, 130);
                corner2.Fill = RectangleSolidColorBrush2;



                //Point four
                Ellipse corner3 = new Ellipse();
                corner3.Height = 8;
                corner3.Width = 8;

                Canvas.SetLeft(corner3, rectangle.Pt4[0]-4);
                Canvas.SetTop(corner3, rectangle.Pt4[1]-4);

                SolidColorBrush RectangleSolidColorBrush3 = new SolidColorBrush();
                RectangleSolidColorBrush3.Color = Color.FromRgb(75, 0, 130);
                corner3.Fill = RectangleSolidColorBrush3;



                //Center Point
                Ellipse center = new Ellipse();
                center.Height = 8;
                center.Width = 8;

                Canvas.SetLeft(center, rectangle.GeometricCenterPoint[0]-4);
                Canvas.SetTop(center, rectangle.GeometricCenterPoint[1]-4);

                SolidColorBrush RectangleSolidCenter = new SolidColorBrush();
                RectangleSolidCenter.Color = Color.FromRgb(36, 113, 163);
                center.Fill = RectangleSolidCenter;


                //Text Indice
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Convert.ToString(rectangle.Id);

                Canvas.SetLeft(textBlock, rectangle.GeometricCenterPoint[0] - 14);
                Canvas.SetTop(textBlock, rectangle.GeometricCenterPoint[1] - 14);


                //add all points
                canvas2.Children.Add(corner);
                canvas2.Children.Add(corner1);
                canvas2.Children.Add(corner2);
                canvas2.Children.Add(corner3);
                canvas2.Children.Add(center);
                canvas2.Children.Add(textBlock);
                
            }
        }

        public void DrawPointsRhombus(Canvas canvas2)
        {
            for (int i = 0; i < ListShapes.HsbRhombusList.Count; i++)
            {
                var rhombus = ListShapes.HsbRhombusList[i];

                //Point one
                Ellipse corner = new Ellipse();
                corner.Height = 8;
                corner.Width = 8;

                Canvas.SetLeft(corner, rhombus.Pt1[0]-4);
                Canvas.SetTop(corner, rhombus.Pt1[1]-4);

                SolidColorBrush RhombusSolidColorBrush = new SolidColorBrush();
                RhombusSolidColorBrush.Color = Color.FromRgb(39, 174, 96);
                corner.Fill = RhombusSolidColorBrush;

               

                //Point two
                Ellipse corner1 = new Ellipse();
                corner1.Height = 8;
                corner1.Width = 8;

                Canvas.SetLeft(corner1, rhombus.Pt2[0]-4);
                Canvas.SetTop(corner1, rhombus.Pt2[1]-4);

                SolidColorBrush RhombusSolidColorBrush1 = new SolidColorBrush();
                RhombusSolidColorBrush1.Color = Color.FromRgb(39, 174, 96);
                corner1.Fill = RhombusSolidColorBrush1;

                

                //Point three
                Ellipse corner2 = new Ellipse();
                corner2.Height = 8;
                corner2.Width = 8;

                Canvas.SetLeft(corner2, rhombus.Pt3[0]-4);
                Canvas.SetTop(corner2, rhombus.Pt3[1]-4);

                SolidColorBrush RhombusSolidColorBrush2 = new SolidColorBrush();
                RhombusSolidColorBrush2.Color = Color.FromRgb(39, 174, 96);
                corner2.Fill = RhombusSolidColorBrush2;

                

                //Point four
                Ellipse corner3 = new Ellipse();
                corner3.Height = 8;
                corner3.Width = 8;

                Canvas.SetLeft(corner3, rhombus.Pt4[0]-4);
                Canvas.SetTop(corner3, rhombus.Pt4[1]-4);

                SolidColorBrush RhombusSolidColorBrush3 = new SolidColorBrush();
                RhombusSolidColorBrush3.Color = Color.FromRgb(39, 174, 96);
                corner3.Fill = RhombusSolidColorBrush3;



                //Center Point
                Ellipse center = new Ellipse();
                center.Height = 8;
                center.Width = 8;

                Canvas.SetLeft(center, rhombus.GeometricCenterPoint[0]-4);
                Canvas.SetTop(center, rhombus.GeometricCenterPoint[1]-4);

                SolidColorBrush RhombusSolidCenter = new SolidColorBrush();
                RhombusSolidCenter.Color = Color.FromRgb(36, 113, 163);
                center.Fill = RhombusSolidCenter;

                //Text Indice
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Convert.ToString(rhombus.Id);

                Canvas.SetLeft(textBlock, rhombus.GeometricCenterPoint[0] - 14);
                Canvas.SetTop(textBlock, rhombus.GeometricCenterPoint[1] - 14);

                //add all points
                canvas2.Children.Add(corner);
                canvas2.Children.Add(corner1);
                canvas2.Children.Add(corner2);
                canvas2.Children.Add(corner3);
                canvas2.Children.Add(center);
                canvas2.Children.Add(textBlock);
            }
        }

        public void DrawPointsCircles(Canvas canvas2)
        {
            for (int i = 0; i < ListShapes.HsbCirclesList.Count; i++)
            {
                var circles = ListShapes.HsbCirclesList[i];

                //Center Point
                Ellipse center = new Ellipse();
                center.Height = 8;
                center.Width = 8;

                Canvas.SetLeft(center, circles.GeometricCenterPoint[0]-4);
                Canvas.SetTop(center, circles.GeometricCenterPoint[1]-4);

                SolidColorBrush RhombusSolidCenter = new SolidColorBrush();
                RhombusSolidCenter.Color = Color.FromRgb(36, 113, 163);
                center.Fill = RhombusSolidCenter;

                //Text Indice
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Convert.ToString(circles.Id);

                Canvas.SetLeft(textBlock, circles.GeometricCenterPoint[0] - 14);
                Canvas.SetTop(textBlock, circles.GeometricCenterPoint[1] - 14);

                //add all points
                canvas2.Children.Add(center);
                canvas2.Children.Add(textBlock);
            }
        }

        public void DrawPointsOval(Canvas canvas2)
        {

            for (int i = 0; i < ListShapes.HsbEllipsesList.Count; i++)
            {
                var oval = ListShapes.HsbEllipsesList[i];

                //Center Point
                Ellipse center = new Ellipse();
                center.Height = 8;
                center.Width = 8;

                Canvas.SetLeft(center, oval.GeometricCenterPoint[0] - 4);
                Canvas.SetTop(center, oval.GeometricCenterPoint[1] - 4);

                SolidColorBrush OvalSolidCenter = new SolidColorBrush();
                OvalSolidCenter.Color = Color.FromRgb(36, 113, 163);
                center.Fill = OvalSolidCenter;

                //Text Indice
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Convert.ToString(oval.Id);

                Canvas.SetLeft(textBlock, oval.GeometricCenterPoint[0] - 14);
                Canvas.SetTop(textBlock, oval.GeometricCenterPoint[1] - 14);

                //add all points
                canvas2.Children.Add(center);
                canvas2.Children.Add(textBlock);
            }
        }


        #endregion

        #region Read and Write JSON

        public void ExportShapeJson(string path) 
        {
            
            Object[] geometryFigures = { ListShapes };
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonFile = js.Serialize(geometryFigures);

            if (File.Exists(path))
            {
                File.Delete(path);
                System.IO.File.WriteAllText(path + @"\hsbTest.json", jsonFile);
                MessageBox.Show("File updated");
            }
            else
            {
                System.IO.File.WriteAllText(path + @"\hsbTest.json", jsonFile);
                MessageBox.Show("File created");
            }

            Process process = new Process();
            process.StartInfo.FileName =  path+@"\hsbTest.json";
            
            process.Start();
        }

        public void ReadJsonShape(string Path, Canvas canvas1, TextBlock textBlock1, TextBlock textBlock2)
        {
            canvas1.Children.Clear();
            
            StreamReader readJson = new StreamReader(Path);
            string jsonString = readJson.ReadToEnd();

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic blogObject = JsonConvert.DeserializeObject<List<HsbListShapes>>(jsonString);

            HsbListShapes importListFigures = blogObject[0];

            DrawShapes(importListFigures, canvas1, textBlock1, textBlock2);
        }

        #endregion

        #region Reconstruction JSON

        //overload
        public void DrawShapes(HsbListShapes list, Canvas canvas1, TextBlock textBlock1, TextBlock textBlock2)
        {
            //We always need to control that the objects are not null.
            if (canvas1 == null) return;

            ListShapes.HsbRectanglesList.Clear();
            ListShapes.HsbTrianglesList.Clear();
            ListShapes.HsbRhombusList.Clear();
            ListShapes.HsbEllipsesList.Clear();
            ListShapes.HsbCirclesList.Clear();

            AreaSum = 0.0;

            canvas1.Children.Clear();
            
            //Create only triangles
            for (int i = 0; i < list.HsbTrianglesList.Count; i++)
            {
                //It waits 1/2 second.
                Thread.Sleep(500);

                HsbTriangle triangle = list.HsbTrianglesList[i];
                
                Polygon polygonAndHsbShape = CreateTriangle(triangle, textBlock1, textBlock2);

                if (polygonAndHsbShape == null) continue;

                canvas1.Children.Add(polygonAndHsbShape);
            }

            //Create only rhombus
            for (int i = 0; i < list.HsbRhombusList.Count; i++)
            {
                //It waits 1/2 second.
                Thread.Sleep(500);

                HsbRhombus rhombus = list.HsbRhombusList[i];

                Polygon polygonAndHsbShape = CreateRhombus(rhombus, textBlock1, textBlock2);

                if (polygonAndHsbShape == null) continue;

                canvas1.Children.Add(polygonAndHsbShape);
            }

            //Create only rectangle
            for (int i = 0; i < list.HsbRectanglesList.Count; i++)
            {
                //It waits 1/2 second.
                Thread.Sleep(500);

                HsbRectangle rectangle = list.HsbRectanglesList[i];

                Polygon polygonAndHsbShape = CreateRectangle(rectangle, textBlock1, textBlock2);

                if (polygonAndHsbShape == null) continue;

                canvas1.Children.Add(polygonAndHsbShape);
            }

            //Create only Circle
            for (int i = 0; i < list.HsbCirclesList.Count; i++)
            {
                //It waits 1/2 second.
                Thread.Sleep(500);

                HsbCircle circle = list.HsbCirclesList[i];

                Ellipse ellipseAndHsbShape = CreateCircle(circle, textBlock1, textBlock2);

                if (ellipseAndHsbShape == null) continue;

                canvas1.Children.Add(ellipseAndHsbShape);
            }

            //Create only Oval
            for (int i = 0; i < list.HsbEllipsesList.Count; i++)
            {
                //It waits 1/2 second.
                Thread.Sleep(500);

                HsbEllipse oval = list.HsbEllipsesList[i];

                Ellipse ellipseAndHsbShape = CreateEllipse(oval, textBlock1, textBlock2);

                if (ellipseAndHsbShape == null) continue;

                canvas1.Children.Add(ellipseAndHsbShape);
            }
        }

        private Polygon CreateTriangle(HsbTriangle triangle, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (triangle.Id <= 0) return null;

            

            Polygon polygon = new Polygon();

            polygon.StrokeThickness = 1;

            SolidColorBrush TriangleSolidColorBrush = new SolidColorBrush();
            TriangleSolidColorBrush.Color = Color.FromRgb(249, 231, 159);
            polygon.Fill = TriangleSolidColorBrush;

            SolidColorBrush TriangleSolidColorStroke = new SolidColorBrush();
            TriangleSolidColorStroke.Color = Color.FromRgb(252, 243, 207);
            polygon.Stroke = TriangleSolidColorStroke;

            var P1 = new System.Windows.Point(triangle.Pt1[0], triangle.Pt1[1]);
            var P2 = new System.Windows.Point(triangle.Pt2[0], Convert.ToInt32(triangle.Pt2[1]));
            var P3 = new System.Windows.Point(triangle.Pt3[0], Convert.ToInt32(triangle.Pt3[1]));

            polygon.Points.Add(P1);
            polygon.Points.Add(P2);
            polygon.Points.Add(P3);

            AreaSum += triangle.Area;

            TextPopupTriangle(triangle, textBlock1, textBlock2, polygon);

            ListShapes.HsbTrianglesList.Add(triangle);

            return polygon;
        }

        private Polygon CreateRectangle(HsbRectangle rectangle, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (rectangle.Id <= 0) return null;

            
            Polygon polygon = new Polygon();

            polygon.StrokeThickness = 1;

            SolidColorBrush RectangleSolidColorBrush = new SolidColorBrush();
            RectangleSolidColorBrush.Color = Color.FromRgb(133, 193, 233);
            polygon.Fill = RectangleSolidColorBrush;

            SolidColorBrush RectangleSolidColorStroke = new SolidColorBrush();
            RectangleSolidColorStroke.Color = Color.FromRgb(46, 134, 193);
            polygon.Stroke = RectangleSolidColorStroke;

            polygon.Points.Add(new System.Windows.Point(rectangle.Pt1[0], rectangle.Pt1[1]));
            polygon.Points.Add(new System.Windows.Point(rectangle.Pt2[0], rectangle.Pt2[1]));
            polygon.Points.Add(new System.Windows.Point(rectangle.Pt3[0], rectangle.Pt3[1]));
            polygon.Points.Add(new System.Windows.Point(rectangle.Pt4[0], rectangle.Pt4[1]));

            AreaSum += rectangle.Area;

            TextPopupRectangle(rectangle, textBlock1, textBlock2, polygon);

            ListShapes.HsbRectanglesList.Add(rectangle);

            return polygon;
        }

        private Polygon CreateRhombus(HsbRhombus rhombus, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (rhombus.Id <= 0) return null;


            Polygon polygon = new Polygon();

            polygon.StrokeThickness = 1;

            SolidColorBrush RhombusSolidColorBrush = new SolidColorBrush();
            RhombusSolidColorBrush.Color = Color.FromRgb(174, 213, 129);
            polygon.Fill = RhombusSolidColorBrush;

            SolidColorBrush RhombusSolidColorStroke = new SolidColorBrush();
            RhombusSolidColorStroke.Color = Color.FromRgb(197, 225, 165);
            polygon.Stroke = RhombusSolidColorStroke;

            polygon.Points.Add(new System.Windows.Point(rhombus.Pt1[0], rhombus.Pt1[1]));
            polygon.Points.Add(new System.Windows.Point(rhombus.Pt2[0], rhombus.Pt2[1]));
            polygon.Points.Add(new System.Windows.Point(rhombus.Pt3[0], rhombus.Pt3[1]));
            polygon.Points.Add(new System.Windows.Point(rhombus.Pt4[0], rhombus.Pt4[1]));

            AreaSum += rhombus.Area;

            TextPopupRhombus(rhombus, textBlock1, textBlock2, polygon);

            ListShapes.HsbRhombusList.Add(rhombus);

            return polygon;
        }

        private Ellipse CreateCircle(HsbCircle circle, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (circle.Id < 1) return null;


            Ellipse ellipse = new Ellipse();

            ellipse.StrokeThickness = 1;

            SolidColorBrush CircleSolidColorBrush = new SolidColorBrush();
            CircleSolidColorBrush.Color = Color.FromRgb(225, 190, 231);
            CircleSolidColorBrush.Opacity = 0.5;
            ellipse.Fill = CircleSolidColorBrush;

            SolidColorBrush CircleSolidColorStroke = new SolidColorBrush();
            CircleSolidColorStroke.Color = Color.FromRgb(186, 104, 200);
            ellipse.Stroke = CircleSolidColorStroke;

            ellipse.Width = circle.Radius * 2;
            ellipse.Height = circle.Radius * 2;

            Canvas.SetLeft(ellipse, circle.PtLeftTop[0]);
            Canvas.SetTop(ellipse, circle.PtLeftTop[1]);

            AreaSum += circle.Area;

            TextPopupCircles(circle, textBlock1, textBlock2, ellipse);

            ListShapes.HsbCirclesList.Add(circle);

            return ellipse;
        }

        private Ellipse CreateEllipse(HsbEllipse oval, TextBlock textBlock1, TextBlock textBlock2)
        {
            if (oval.Id < 1) return null;

            Ellipse ellipse = new Ellipse();

            ellipse.StrokeThickness = 1;

            SolidColorBrush OvalSolidColorBrush = new SolidColorBrush();
            OvalSolidColorBrush.Color = Color.FromRgb(248, 187, 208);
            OvalSolidColorBrush.Opacity = 0.5;
            ellipse.Fill = OvalSolidColorBrush;

            SolidColorBrush OvalSolidColorStroke = new SolidColorBrush();
            OvalSolidColorStroke.Color = Color.FromRgb(240, 98, 146);
            ellipse.Stroke = OvalSolidColorStroke;

            ellipse.Width = oval.WidthRadius * 2;
            ellipse.Height = oval.HeightRadius * 2;

            Canvas.SetLeft(ellipse, oval.PtLeftTop[0]);
            Canvas.SetTop(ellipse, oval.PtLeftTop[1]);

            AreaSum += oval.Area;

            TextPopupEllipse(oval, textBlock1, textBlock2, ellipse);

            ListShapes.HsbEllipsesList.Add(oval);

            return ellipse;
        }

        #endregion

        #region Get Shapes

        public static HsbListShapes GetShapes(string strFilePath)
        {
            string jsonString = "";

            if (File.Exists(strFilePath))
            {
                StreamReader srJson = new StreamReader(strFilePath);
                jsonString = srJson.ReadToEnd();
            }
            else
            {
                MessageBox.Show("File don´t found");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic blogObject = JsonConvert.DeserializeObject<List<HsbListShapes>>(jsonString);

            HsbListShapes shapesList = blogObject[0];
              
           

            return shapesList;
        }

        #endregion
    }
}

