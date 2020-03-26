using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Grafika_Projekat1
{
    /// <summary>
    /// Interaction logic for PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        public PolygonWindow()
        {
            InitializeComponent();
            if (MainWindow.PolygonDesigner != null)
            {
                borderThicknessTb.Text = MainWindow.PolygonDesigner.StrokeThickness.ToString();
                borderColorPicker.SelectedColor = GetColorFromPicker(MainWindow.PolygonDesigner.Stroke);
                fillColorPicker.SelectedColor = GetColorFromPicker(MainWindow.PolygonDesigner.Fill);
            }
        }

        private Color GetColorFromPicker(Brush brush)
        {
            byte a = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).A;
            byte g = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).G;
            byte r = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).R;
            byte b = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).B;

            return Color.FromArgb(a, r, g, b);
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show("All fields must be filled in order to draw polygon.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MainWindow.PolygonDesigner == null)
            {
                MainWindow.PolygonDesigner = new Polygon();
                MainWindow.PolygonDesigner.Stroke = new SolidColorBrush((Color)borderColorPicker.SelectedColor);
                MainWindow.PolygonDesigner.Fill = new SolidColorBrush((Color)fillColorPicker.SelectedColor);
                MainWindow.PolygonDesigner.StrokeThickness = Int32.Parse(borderThicknessTb.Text);
            }
            else
            {
                MainWindow.PolygonDesigner.Stroke = new SolidColorBrush((Color)borderColorPicker.SelectedColor);
                MainWindow.PolygonDesigner.Fill = new SolidColorBrush((Color)fillColorPicker.SelectedColor);
                MainWindow.PolygonDesigner.StrokeThickness = Int32.Parse(borderThicknessTb.Text);
            }

            this.Close();
        }

        private bool ValidateInput()
        {
            bool retVal = true;

            if (borderColorPicker.SelectedColor == null)
                retVal = false;

            if (fillColorPicker.SelectedColor == null)
                retVal = false;

            if (borderThicknessTb.Text == "" || borderThicknessTb.Text == null)
                retVal = false;

            return retVal;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PolygonDesigner = null;
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}