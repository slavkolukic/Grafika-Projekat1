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
    /// Interaction logic for ElipseWindow.xaml
    /// </summary>
    public partial class EllipseWindow : Window
    {
        public EllipseWindow()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.EllipseDesigner = null;
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

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show("All fields must be filled in order to draw ellipse.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow.EllipseDesigner = new Ellipse();
            MainWindow.EllipseDesigner.Height = Int32.Parse(radyTb.Text) * 2;
            MainWindow.EllipseDesigner.Width = Int32.Parse(radxTb.Text) * 2;
            MainWindow.EllipseDesigner.Stroke = new SolidColorBrush((Color)borderColorPicker.SelectedColor);
            MainWindow.EllipseDesigner.Fill = new SolidColorBrush((Color)fillColorPicker.SelectedColor);
            MainWindow.EllipseDesigner.StrokeThickness = Int32.Parse(borderThicknessTb.Text);

            this.Close();
        }

        private bool ValidateInput()
        {
            bool retVal = true;

            if (radxTb.Text == "" || radxTb.Text == null)
                retVal = false;

            if (radyTb.Text == "" || radyTb.Text == null)
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