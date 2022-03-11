using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hsbcadTest
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonsOne_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            HsbShapesController shapeTest = HsbShapesController.Instance;

            shapeTest.DrawShapes(canvas1, texBlock1, texBlock2);

            if ((bool)CheckBoxPoint.IsChecked == true)
            {
                canvas2.Children.Clear();
                shapeTest.DrawPointsRectangle(canvas2);
                shapeTest.DrawPointsRhombus(canvas2);
                shapeTest.DrawPointsTriangles(canvas2);
                shapeTest.DrawPointsCircles(canvas2);
                shapeTest.DrawPointsOval(canvas2);
            }
            else
            {
                canvas2.Children.Clear();
            }

            //HsbShapesController.GetShapes(@"C:\Users\Usuario\Documents\Exam\hsbTest.json");

            Mouse.OverrideCursor = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.MessageBox.Show(dialog.SelectedPath.ToString());

                HsbShapesController shapeTest = HsbShapesController.Instance;

                shapeTest.ExportShapeJson(dialog.SelectedPath);
            }
            Mouse.OverrideCursor = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var dialog = new OpenFileDialog();

            dialog.Filter = "JSON (*.json)|*.json";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                HsbShapesController shapeTest = HsbShapesController.Instance;

                shapeTest.ReadJsonShape(dialog.FileName, canvas1, texBlock1, texBlock2);

                canvas2.Children.Clear();
                Mouse.OverrideCursor = null;
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            HsbShapesController shapeTest = HsbShapesController.Instance;

            shapeTest.DrawPointsRectangle(canvas2);
            shapeTest.DrawPointsRhombus(canvas2);
            shapeTest.DrawPointsTriangles(canvas2);
            shapeTest.DrawPointsCircles(canvas2);
            shapeTest.DrawPointsOval(canvas2);
        }

        private void CheckBoxPoint_Unchecked(object sender, RoutedEventArgs e)
        {
            canvas2.Children.Clear();
        }
    }
}
