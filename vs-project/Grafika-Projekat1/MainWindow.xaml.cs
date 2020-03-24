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

        public MainWindow()
        {
            InitializeComponent();
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
    }
}