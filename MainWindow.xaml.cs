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

namespace WpfPainter
{
    public partial class MainWindow : Window
    {
        enum Sizes
        {
            Small = 4, Medium = 8, Large = 12
        }

        Brush brush = Brushes.Black;
        int diameter = (int)Sizes.Medium;
        bool shouldPaint = false;
        bool shouldErase = false;
        
        public MainWindow()
        {
            InitializeComponent();

            paintCanvas.MouseLeftButtonDown += (s, e) => shouldPaint = true;
            paintCanvas.MouseRightButtonDown += (s, e) => shouldErase = true;
            paintCanvas.MouseLeftButtonUp += (s, e) => shouldPaint = false;
            paintCanvas.MouseRightButtonUp += (s, e) => shouldErase = false;

            clear.Click += (s, e) => paintCanvas.Children.Clear();
        }

        void colorChoice(object sender, RoutedEventArgs e)
        {
            brush = (bool)red.IsChecked ? Brushes.Red
                : (bool)blue.IsChecked ? Brushes.Blue
                : (bool)green.IsChecked ? Brushes.Green
                : Brushes.Black;

        }

        void sizeChoice(object sender, RoutedEventArgs e)
        {
            diameter = (int)((bool)small.IsChecked ? Sizes.Small
                : (bool)medium.IsChecked ? Sizes.Medium
                : Sizes.Large);
        }

        void canvasMouseMove(object sender, MouseEventArgs e)
        {
            if(shouldPaint || shouldErase)
            {
                DrawPoint(e.GetPosition(paintCanvas),
                    shouldPaint ? brush : paintCanvas.Background);
            }
        }

        void DrawPoint(Point point, Brush brush)
        {
            Ellipse newPoint = new()
            {
                Width = diameter,
                Height = diameter,
                Fill = brush,
                
            };
            Canvas.SetLeft(newPoint, point.X);
            Canvas.SetTop(newPoint, point.Y);
            paintCanvas.Children.Add(newPoint);
        }

        void undoPaint(object sender, RoutedEventArgs e)
        {
            int count = paintCanvas.Children.Count;
            if(count > 0)
            {
                paintCanvas.Children.RemoveAt(count - 1);
            }
        }
    }
}
