using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool isFullscreen = false;
        private static string activeShape = "none";
        private static List<Button> buttons = new List<Button>();
        private static List<PackIcon> icons = new List<PackIcon>();

        public MainWindow()
        {
            InitializeComponent();
            buttons.Add(elipseBtn);
            buttons.Add(rectangleBtn);
            buttons.Add(polygonBtn);
            buttons.Add(imageBtn);
            icons.Add(elipseBtnIcon);
            icons.Add(rectangleBtnIcon);
            icons.Add(polygonBtnIcon);
            icons.Add(imageBtnIcon);
        }

        private void TopBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (isFullscreen)
            {
                WindowState = WindowState.Normal;
                isFullscreen = false;
                fullScreenButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8737F"));
                fullScreenButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#475C7A"));
                fullScreenButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Fullscreen;
            }
            else
            {
                WindowState = WindowState.Maximized;
                isFullscreen = true;
                fullScreenButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8737F"));
                fullScreenButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#475C7A"));
                fullScreenButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TurnOffButtons()
        {
            foreach (var btn in buttons)
            {
                if (btn.Name.Contains(activeShape))
                    btn.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8737F"));
                else
                    btn.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }

            foreach (var icon in icons)
            {
                if (icon.Name.Contains(activeShape))
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8737F"));
                else
                    icon.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void ElipseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (activeShape == "elipse")
                activeShapeLbl.Content = "none";
            else
                activeShapeLbl.Content = "elipse";

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (activeShape == "rectangle")
                activeShapeLbl.Content = "none";
            else
                activeShapeLbl.Content = "rectangle";

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void PolygonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (activeShape == "polygon")
                activeShapeLbl.Content = "none";
            else
                activeShapeLbl.Content = "polygon";

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void ImageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (activeShape == "image")
                activeShapeLbl.Content = "none";
            else
                activeShapeLbl.Content = "image";

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }
    }
}