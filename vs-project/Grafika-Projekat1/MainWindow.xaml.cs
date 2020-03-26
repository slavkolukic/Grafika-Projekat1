using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private static Stack<Canvas> undoStack = new Stack<Canvas>();
        private static Stack<Canvas> redoStack = new Stack<Canvas>();

        private static bool isFullscreen = false;
        private static string activeShape = "none";
        private static List<Button> buttons = new List<Button>();
        private static List<PackIcon> icons = new List<PackIcon>();
        private static ObservableCollection<Point> polyPoints = new ObservableCollection<Point>();

        private static Ellipse ellipseDesigner = null;
        private static Rectangle rectangleDesigner = null;
        private static Polygon polygonDesigner = null;
        private static Image imageDesigner = null;

        public static Ellipse EllipseDesigner { get => ellipseDesigner; set => ellipseDesigner = value; }
        public static Rectangle RectangleDesigner { get => rectangleDesigner; set => rectangleDesigner = value; }
        public static Polygon PolygonDesigner { get => polygonDesigner; set => polygonDesigner = value; }
        public static ObservableCollection<Point> PolyPoints { get => polyPoints; set => polyPoints = value; }
        public static Image ImageDesigner { get => imageDesigner; set => imageDesigner = value; }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            buttons.Add(ellipseBtn);
            buttons.Add(rectangleBtn);
            buttons.Add(polygonBtn);
            buttons.Add(imageBtn);
            icons.Add(ellipseBtnIcon);
            icons.Add(rectangleBtnIcon);
            icons.Add(polygonBtnIcon);
            icons.Add(imageBtnIcon);

            cnvs.Cursor = ((TextBlock)this.Resources["PencilCursor"]).Cursor;

            var dp = DependencyPropertyDescriptor.FromProperty(Label.ContentProperty, typeof(Label));
            dp.AddValueChanged(vertexesLbl, (sender, args) =>
            {
                if (polyPoints.Count >= 3)
                    vertexesLbl.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8737F"));
                else
                    vertexesLbl.Foreground = new SolidColorBrush(Colors.LightGray);
            });
        }

        private void TopBarGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            polyPoints.Clear();

            if (activeShape == "ellipse")
            {
                activeShapeLbl.Content = "none";
                usageTb.Text = "";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }
            else
            {
                activeShapeLbl.Content = "ellipse";
                usageTb.Text = "Right click on canvas to create new ellipse. Left click on existing ellipse to edit.";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            polyPoints.Clear();

            if (activeShape == "rectangle")
            {
                activeShapeLbl.Content = "none";
                usageTb.Text = "";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }
            else
            {
                activeShapeLbl.Content = "rectangle";
                usageTb.Text = "Right click on canvas to create new rectangle. Left click on existing rectangle to edit.";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void PolygonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (activeShape == "polygon")
            {
                polyPoints.Clear();
                activeShapeLbl.Content = "none";
                usageTb.Text = "";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }
            else
            {
                activeShapeLbl.Content = "polygon";
                usageTb.Text = "Right click on canvas to choose at least 3 vertexes and left click on canvas to create polygon. Left click on existing polygon to edit.";

                vertexesLbl.Visibility = Visibility.Visible;
                vertexesSelectedLbl.Visibility = Visibility.Visible;
            }

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void ImageBtn_Click(object sender, RoutedEventArgs e)
        {
            polyPoints.Clear();

            if (activeShape == "image")
            {
                activeShapeLbl.Content = "none";
                usageTb.Text = "";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }
            else
            {
                activeShapeLbl.Content = "image";
                usageTb.Text = "Right click on canvas to choose image from hard drive. Left click on existing image to replace it.";

                vertexesLbl.Visibility = Visibility.Hidden;
                vertexesSelectedLbl.Visibility = Visibility.Hidden;
            }

            activeShape = activeShapeLbl.Content.ToString();
            TurnOffButtons();
        }

        private void Cnvs_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = new Point();
            switch (activeShape)
            {
                case "ellipse":
                    p = Mouse.GetPosition(cnvs);
                    EllipseDesigner = null;
                    EllipseWindow elipseWindow = new EllipseWindow();
                    elipseWindow.ShowDialog();
                    if (EllipseDesigner != null)
                    {
                        Canvas.SetLeft(EllipseDesigner, p.X);
                        Canvas.SetTop(EllipseDesigner, p.Y);
                        Ellipse ellipse = new Ellipse();
                        ellipse = EllipseDesigner;
                        EllipseDesigner = null;
                        ellipse.MouseLeftButtonDown += OnEllipseMouseLeftButtonDown;

                        AddCurrentCanvasToUndo();

                        cnvs.Children.Add(ellipse);
                        clearBtn.IsEnabled = true;
                    }
                    break;

                case "rectangle":
                    p = Mouse.GetPosition(cnvs);
                    RectangleDesigner = null;
                    RectangleWindow rectangleWindow = new RectangleWindow();
                    rectangleWindow.ShowDialog();
                    if (RectangleDesigner != null)
                    {
                        Canvas.SetLeft(RectangleDesigner, p.X);
                        Canvas.SetTop(RectangleDesigner, p.Y);
                        Rectangle rectangle = new Rectangle();
                        rectangle = RectangleDesigner;
                        RectangleDesigner = null;
                        rectangle.MouseLeftButtonDown += OnRectangleMouseLeftButtonDown;

                        AddCurrentCanvasToUndo();

                        cnvs.Children.Add(rectangle);
                        clearBtn.IsEnabled = true;
                    }
                    break;

                case "polygon":
                    if (activeShape == "polygon")
                    {
                        polyPoints.Add(Mouse.GetPosition(cnvs));
                    }
                    break;

                case "image":
                    p = Mouse.GetPosition(cnvs);
                    ImageDesigner = null;
                    ImageWindow imageWindow = new ImageWindow();
                    imageWindow.ShowDialog();
                    if (ImageDesigner != null)
                    {
                        Canvas.SetLeft(ImageDesigner, p.X);
                        Canvas.SetTop(ImageDesigner, p.Y);
                        Image image = new Image();
                        image = ImageDesigner;
                        ImageDesigner = null;
                        image.MouseLeftButtonDown += OnImageMouseLeftButtonDown;

                        AddCurrentCanvasToUndo();

                        cnvs.Children.Add(image);
                        clearBtn.IsEnabled = true;
                    }
                    break;
            }
        }

        private void OnImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (activeShape == "polygon" && polyPoints.Count > 3)
                return;

            var image = e.OriginalSource;
            ImageDesigner = (Image)image;
            ImageWindow iw = new ImageWindow();

            Canvas temp = new Canvas();
            CopyCanvas(cnvs, temp);

            iw.ShowDialog();

            if (ImageDesigner != null)
            {
                undoStack.Push(temp);
                if (undoStack.Count > 0)
                {
                    undoBtn.IsEnabled = true;
                }
            }
        }

        private void OnRectangleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (activeShape == "polygon" && polyPoints.Count > 3)
                return;

            var rectangle = e.OriginalSource;
            RectangleDesigner = (Rectangle)rectangle;
            RectangleWindow rw = new RectangleWindow();

            Canvas temp = new Canvas();
            CopyCanvas(cnvs, temp);

            rw.ShowDialog();

            if (RectangleDesigner != null)
            {
                undoStack.Push(temp);
                if (undoStack.Count > 0)
                {
                    undoBtn.IsEnabled = true;
                }
            }
        }

        private void OnEllipseMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (activeShape == "polygon" && polyPoints.Count > 3)
                return;

            var ellipse = e.OriginalSource;
            EllipseDesigner = (Ellipse)ellipse;
            EllipseWindow ew = new EllipseWindow();

            Canvas temp = new Canvas();
            CopyCanvas(cnvs, temp);

            ew.ShowDialog();

            if (EllipseDesigner != null)
            {
                undoStack.Push(temp);
                if (undoStack.Count > 0)
                {
                    undoBtn.IsEnabled = true;
                }
            }
        }

        private void Cnvs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (activeShape == "polygon")
            {
                if (polyPoints.Count < 3)
                {
                    polyPoints.Clear();
                    return;
                }

                PolygonDesigner = null;
                PolygonWindow polygonWindow = new PolygonWindow();
                polygonWindow.ShowDialog();
                if (PolygonDesigner != null)
                {
                    Polygon poly = new Polygon();
                    foreach (var point in polyPoints)
                    {
                        poly.Points.Add(point);
                    }
                    poly.Fill = PolygonDesigner.Fill;
                    poly.Stroke = PolygonDesigner.Stroke;
                    poly.StrokeThickness = PolygonDesigner.StrokeThickness;
                    PolygonDesigner = null;
                    poly.MouseLeftButtonDown += OnPolygonMouseLeftButtonDown;

                    AddCurrentCanvasToUndo();

                    cnvs.Children.Add(poly);
                    clearBtn.IsEnabled = true;
                }
                polyPoints.Clear();
            }
        }

        private void AddCurrentCanvasToUndo()
        {
            Canvas temp = new Canvas();
            CopyCanvas(cnvs, temp);
            undoStack.Push(temp);

            if (undoStack.Count > 0)
            {
                undoBtn.IsEnabled = true;
            }
        }

        private void OnPolygonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (activeShape == "polygon" && polyPoints.Count >= 3)
                return;

            var polygon = e.OriginalSource;
            PolygonDesigner = (Polygon)polygon;
            PolygonWindow pw = new PolygonWindow();

            Canvas temp = new Canvas();
            CopyCanvas(cnvs, temp);

            pw.ShowDialog();

            if (PolygonDesigner != null)
            {
                undoStack.Push(temp);
                if (undoStack.Count > 0)
                {
                    undoBtn.IsEnabled = true;
                }
            }
        }

        private void CopyCanvas(Canvas from, Canvas to)
        {
            to.Children.Clear();
            foreach (UIElement child in from.Children)
            {
                var xaml = System.Windows.Markup.XamlWriter.Save(child);
                UIElement deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                if ((deepCopy as Ellipse) != null)
                {
                    deepCopy.MouseLeftButtonDown += OnEllipseMouseLeftButtonDown;
                }
                else if ((deepCopy as Rectangle) != null)
                {
                    deepCopy.MouseLeftButtonDown += OnRectangleMouseLeftButtonDown;
                }
                else if ((deepCopy as Polygon) != null)
                {
                    deepCopy.MouseLeftButtonDown += OnPolygonMouseLeftButtonDown;
                }
                else
                {
                    deepCopy.MouseLeftButtonDown += OnImageMouseLeftButtonDown;
                }
                to.Children.Add(deepCopy);
            }
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            polyPoints.Clear();

            if (undoStack.Count > 0)
            {
                AddCurrentCanvasToRedo();
                CopyCanvas(undoStack.Pop(), cnvs);

                if (cnvs.Children.Count <= 0)
                    clearBtn.IsEnabled = false;
                else
                    clearBtn.IsEnabled = true;

                if (undoStack.Count <= 0)
                    undoBtn.IsEnabled = false;
            }
        }

        private void AddCurrentCanvasToRedo()
        {
            Canvas temp = new Canvas();
            CopyCanvas(cnvs, temp);
            redoStack.Push(temp);

            if (redoStack.Count > 0)
            {
                redoBtn.IsEnabled = true;
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            polyPoints.Clear();
            AddCurrentCanvasToUndo();

            cnvs.Children.Clear();
            clearBtn.IsEnabled = false;
        }

        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            polyPoints.Clear();

            if (redoStack.Count > 0)
            {
                AddCurrentCanvasToUndo();
                CopyCanvas(redoStack.Pop(), cnvs);

                if (cnvs.Children.Count <= 0)
                    clearBtn.IsEnabled = false;
                else
                    clearBtn.IsEnabled = true;

                if (redoStack.Count <= 0)
                    redoBtn.IsEnabled = false;
                else
                    redoBtn.IsEnabled = true;
            }
        }
    }
}