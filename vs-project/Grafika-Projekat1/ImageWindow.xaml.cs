using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window, INotifyPropertyChanged
    {
        private string imgPath = "";

        public string ImgPath
        {
            get
            {
                return imgPath;
            }
            set
            {
                if (imgPath != value)
                {
                    imgPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ImageWindow()
        {
            DataContext = this;
            InitializeComponent();
            if (MainWindow.ImageDesigner != null)
            {
                widthTb.IsEnabled = false;
                heightTb.IsEnabled = false;

                widthTb.Text = (MainWindow.ImageDesigner.Width).ToString();
                heightTb.Text = (MainWindow.ImageDesigner.Height).ToString();
                imgPath = MainWindow.ImageDesigner.Source.ToString();
                pathTb.Text = MainWindow.ImageDesigner.Source.ToString();
            }
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show("All fields must be filled in order to draw image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MainWindow.ImageDesigner == null)
            {
                MainWindow.ImageDesigner = new Image();
                MainWindow.ImageDesigner.Height = Int32.Parse(heightTb.Text);
                MainWindow.ImageDesigner.Width = Int32.Parse(widthTb.Text);
                MainWindow.ImageDesigner.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
                MainWindow.ImageDesigner.Stretch = Stretch.Fill;
            }
            else
            {
                MainWindow.ImageDesigner.Height = Int32.Parse(heightTb.Text);
                MainWindow.ImageDesigner.Width = Int32.Parse(widthTb.Text);
                MainWindow.ImageDesigner.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
                MainWindow.ImageDesigner.Stretch = Stretch.Fill;
            }

            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ImageDesigner = null;
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

            if (pathTb.Text == "" || pathTb.Text == null)
                retVal = false;

            return retVal;
        }

        private void PathBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                ImgPath = dlg.FileName;
            }
        }
    }
}