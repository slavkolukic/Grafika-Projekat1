using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grafika_Projekat1
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        private Thickness leftSide = new Thickness(-39, 0, 0, 0);
        private Thickness rightSide = new Thickness(0, 0, -39, 0);

        private SolidColorBrush offColor = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        private SolidColorBrush onColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8737F"));

        private bool toggled = false;

        public bool Toggled { get => toggled; set => toggled = value; }

        public ToggleButton()
        {
            InitializeComponent();
            bck.Fill = offColor;
            toggled = false;
            dot.Margin = leftSide;
        }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!toggled)
            {
                bck.Fill = onColor;
                toggled = true;
                dot.Margin = rightSide;
            }
            else
            {
                bck.Fill = offColor;
                toggled = false;
                dot.Margin = leftSide;
            }
        }

        private void Bck_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!toggled)
            {
                bck.Fill = onColor;
                toggled = true;
                dot.Margin = rightSide;
            }
            else
            {
                bck.Fill = offColor;
                toggled = false;
                dot.Margin = leftSide;
            }
        }
    }
}