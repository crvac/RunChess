using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace ChessWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(Board);
            Test.Content = $"{Math.Truncate(point.X)} \n{Math.Truncate(point.Y)}";
            circle.Margin = new Thickness((Math.Truncate(point.X / 50) * 50), (Math.Truncate(point.Y / 50) * 50), 0, 0);
        }

        private void Coord_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (double.TryParse(xCoord.Text, out double x) && 0 < x && x < 400
                && double.TryParse(yCoord.Text, out double y) && 0 < y && y < 400)
            {
                Test.Content = $"{(Math.Truncate(x / 50) * 50) + 25} \n{(Math.Truncate(y / 50) * 50) + 25}";
                circle.Margin = new Thickness((Math.Truncate(y / 50) * 50), (Math.Truncate(x / 50) * 50), 0, 0);
            }
               
        }
    }
}
