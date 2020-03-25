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
    /// Interaction logic for RectangleWindow.xaml
    /// </summary>
    public partial class RectangleWindow : Window
    {
        public RectangleWindow()
        {
            InitializeComponent();
            if (MainWindow.RectangleDesigner != null)
            {
                widthTb.Text = (MainWindow.RectangleDesigner.Width).ToString();
                heightTb.Text = (MainWindow.RectangleDesigner.Height).ToString();
                borderThicknessTb.Text = MainWindow.RectangleDesigner.StrokeThickness.ToString();
                borderColorPicker.SelectedColor = GetColorFromPicker(MainWindow.RectangleDesigner.Stroke);
                fillColorPicker.SelectedColor = GetColorFromPicker(MainWindow.RectangleDesigner.Fill);
            }
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show("All fields must be filled in order to draw rectangle.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MainWindow.RectangleDesigner == null)
            {
                MainWindow.RectangleDesigner = new Rectangle();
                MainWindow.RectangleDesigner.Height = Int32.Parse(heightTb.Text);
                MainWindow.RectangleDesigner.Width = Int32.Parse(widthTb.Text);
                MainWindow.RectangleDesigner.Stroke = new SolidColorBrush((Color)borderColorPicker.SelectedColor);
                MainWindow.RectangleDesigner.Fill = new SolidColorBrush((Color)fillColorPicker.SelectedColor);
                MainWindow.RectangleDesigner.StrokeThickness = Int32.Parse(borderThicknessTb.Text);
            }
            else
            {
                MainWindow.RectangleDesigner.Height = Int32.Parse(heightTb.Text);
                MainWindow.RectangleDesigner.Width = Int32.Parse(widthTb.Text);
                MainWindow.RectangleDesigner.Stroke = new SolidColorBrush((Color)borderColorPicker.SelectedColor);
                MainWindow.RectangleDesigner.Fill = new SolidColorBrush((Color)fillColorPicker.SelectedColor);
                MainWindow.RectangleDesigner.StrokeThickness = Int32.Parse(borderThicknessTb.Text);
            }

            this.Close();
        }

        private Color GetColorFromPicker(Brush brush)
        {
            byte a = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).A;
            byte g = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).G;
            byte r = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).R;
            byte b = ((Color)brush.GetValue(SolidColorBrush.ColorProperty)).B;

            return Color.FromArgb(a, r, g, b);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.RectangleDesigner = null;
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private bool ValidateInput()
        {
            bool retVal = true;

            if (widthTb.Text == "" || widthTb.Text == null)
                retVal = false;

            if (heightTb.Text == "" || heightTb.Text == null)
                retVal = false;

            if (borderColorPicker.SelectedColor == null)
                retVal = false;

            if (fillColorPicker.SelectedColor == null)
                retVal = false;

            if (borderThicknessTb.Text == "" || borderThicknessTb.Text == null)
                retVal = false;

            return retVal;
        }
    }
}