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

namespace kostka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[,] pole3D = {{ 0, 0, 0 },
                                { 100,0, 0 },
                                { 100,0, 100},
                                { 0, 0, 100},
                                { 0, 0, 0 },
                                { 0, 100,0 },
                                { 100,100,0 },
                                { 100,100,100},
                                { 0, 100,100},
                                { 0, 100,0 },
                                { 100, 100,0},
                                { 100, 0,0},
                                { 100, 0,100},
                                { 100,100,100},
                                { 0, 100,100},
                                { 0, 0,100} };

        public MainWindow()
        {
            InitializeComponent();


        }

        int alf = 30, bet = 30, uhel = 70;
        string vyberOsy = "x";

        void Vykresli()
        {
            gridik.Children.Clear();
            Line cara = new Line();
            cara.Stroke = Brushes.White;
            cara.X1 = Axon3Dto2D(alf, bet, 0, 0, 0, 1, "nic", 0).X;
            cara.Y1 = Axon3Dto2D(alf, bet, 0, 0, 0, 1, "nic", 0).Y;
            cara.X2 = Axon3Dto2D(alf, bet, 300, 0, 0, 1, "nic", 0).X;
            cara.Y2 = Axon3Dto2D(alf, bet, 300, 0, 0, 1, "nic", 0).Y;
            gridik.Children.Add(cara);
            cara = new Line();
            cara.Stroke = Brushes.White;
            cara.X1 = Axon3Dto2D(alf, bet, 0, 0, 0, 1, "nic", 0).X;
            cara.Y1 = Axon3Dto2D(alf, bet, 0, 0, 0, 1, "nic", 0).Y;
            cara.X2 = Axon3Dto2D(alf, bet, 0, 300, 0, 1, "nic", 0).X;
            cara.Y2 = Axon3Dto2D(alf, bet, 0, 300, 0, 1, "nic", 0).Y;
            gridik.Children.Add(cara);
            cara = new Line();
            cara.Stroke = Brushes.White;
            cara.X1 = Axon3Dto2D(alf, bet, 0, 0, 0, 1, "nic", 0).X;
            cara.Y1 = Axon3Dto2D(alf, bet, 0, 0, 0, 1, "nic", 0).Y;
            cara.X2 = Axon3Dto2D(alf, bet, 0, 0, 300, 1, "nic", 0).X;
            cara.Y2 = Axon3Dto2D(alf, bet, 0, 0, 300, 1, "nic", 0).Y;
            gridik.Children.Add(cara);

            Point bod1 = Axon3Dto2D(alf, bet, pole3D[0, 0], pole3D[0, 1], pole3D[0, 2], 1, vyberOsy, uhel);
            for (int i = 1; i < pole3D.Length / 3; i++)
            {
                Point bod2 = Axon3Dto2D(alf, bet, pole3D[i, 0], pole3D[i, 1], pole3D[i, 2], 1, vyberOsy, uhel);
                cara = new Line();
                cara.Stroke = Brushes.Red;
                cara.X1 = bod1.X;
                cara.Y1 = bod1.Y;
                cara.X2 = bod2.X;
                cara.Y2 = bod2.Y;
                gridik.Children.Add(cara);
                bod1 = bod2;
            }



        }

        private void sliderUhel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            uhel = (int)sliderUhel.Value;
            Vykresli();
        }

        private void cbOsa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb)
            {
                if (cb.SelectedValue is string str)
                {
                    vyberOsy = str;
                }
                else
                {
                    vyberOsy = "x";
                }
                Vykresli();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Vykresli();
        }

        public Point Axon3Dto2D(int alfa, int beta, double x, double y, double z, double zoom, string osaOtoc, int uhelOtoc)
        {
            double X, Y, Z;
            double radUhelOtoc = uhelOtoc * Math.PI / 180;
            switch (osaOtoc)
            {
                case "x":
                    X = x;
                    Y = y * Math.Cos(radUhelOtoc) - (z * Math.Sin(radUhelOtoc));
                    Z = y * Math.Sin(radUhelOtoc) + (z * Math.Cos(radUhelOtoc));
                    break;
                case "y":
                    X = x * Math.Cos(radUhelOtoc) + (z * Math.Sin(radUhelOtoc));
                    Y = y;
                    Z = -x * Math.Sin(radUhelOtoc) + (z * Math.Cos(radUhelOtoc));
                    break;
                case "z":
                    X = x * Math.Cos(radUhelOtoc) - (y * Math.Sin(radUhelOtoc));
                    Y = x * Math.Sin(radUhelOtoc) + (y * Math.Cos(radUhelOtoc));
                    Z = z;
                    break;
                default: X = x; Y = y; Z = z; break;
            }
            Point bod2D = new Point();
            double alfaR = alfa * Math.PI / 180;
            double betaR = beta * Math.PI / 180;

            bod2D.X = -(Math.Cos(alfaR) * X * zoom) + (Math.Cos(betaR) * Y * zoom) + (gridik.ActualWidth / 2);
            bod2D.Y = gridik.ActualHeight - (-Math.Sin(alfaR) * X * zoom - (Math.Sin(betaR) * Y * zoom) + (Z * zoom) + (gridik.ActualHeight / 2));
            return bod2D;
        }

    }
}
