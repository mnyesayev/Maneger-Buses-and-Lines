using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dotNet5781_03B_3729_1237
{
    /// <summary>
    /// Interaction logic for wBusInfo.xaml
    /// </summary>
    public partial class wBusInfo : Window
    {

        public wBusInfo()
        {
            InitializeComponent();

        }
        Thread thCare;
        Thread thFuel;
        public Thread ThCare { get => thCare; private set => thCare = value; }
        public Thread ThFuel { get => thFuel; private set => thFuel = value; }

        private void bReful_Click(object sender, RoutedEventArgs e)
        {
            Bus tmp = (Bus)this.DataContext;
            if (tmp.State == States.refueling) return;//Protection test
            ThFuel = new Thread(() =>
            {
                tmp.State = States.refueling;
                tmp.Image = "images\\yellow.png";
                this.Dispatcher.Invoke(() =>
                {
                    bRefuel.IsEnabled = false;
                    tb2status.Text = tmp.State.ToString();
                    Im2Status.Source = new BitmapImage(new Uri(tmp.Image, UriKind.Relative));
                    tb1StatusBar.Visibility = Visibility.Visible;
                    tb2StatusBar.Visibility = Visibility.Visible;
                });
                Thread.Sleep(new TimeSpan(0, 0, 12));
                var st = tmp.Refueling();
                this.Dispatcher.Invoke(() =>
                {
                     tb2fuel.Text = tmp.Fuel.ToString();
                     if (tmp.CheckCare())
                     {
                         tmp.State = States.mustCare;
                         tmp.Image = "images\\red.png";
                     }
                     else
                     {
                         tmp.State = States.ready;
                         tmp.Image = "images\\green.png";
                     }
                     tb2status.Text = tmp.State.ToString();
                     Im2Status.Source = new BitmapImage(new Uri(tmp.Image, UriKind.Relative));
                     bRefuel.IsEnabled = true;
                });
                MessageBox.Show(st, "Refuel", MessageBoxButton.OK, MessageBoxImage.Information);
            });
            ThFuel.Start();
            new Thread(() =>
            {
                for (tmp.Time = 12; tmp.Time > 0; --tmp.Time)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                    tb2StatusBar.Text = tmp.Time.ToString();
                    });
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                }
                this.Dispatcher.Invoke(() =>
                {
                tb1StatusBar.Visibility = Visibility.Hidden;
                tb2StatusBar.Visibility = Visibility.Hidden;
                });
            }).Start();

        }
        private void bCare_Click(object sender, RoutedEventArgs e)
        {
            Bus tmp = (Bus)this.DataContext;
            if (tmp.State == States.care) return; //Protection test
            ThCare = new Thread(
            () =>
            {
                tmp.State = States.care;
                tmp.Image = "images\\orange.png";
                this.Dispatcher.Invoke(() =>
                {
                    bCare.IsEnabled = false;
                    tb2status.Text = tmp.State.ToString();
                    Im2Status.Source = new BitmapImage(new Uri(tmp.Image, UriKind.Relative));
                    tb1StatusBar.Visibility = Visibility.Visible;
                    tb2StatusBar.Visibility = Visibility.Visible;
                });
                Thread.Sleep(new TimeSpan(0, 0, 144));
                var str = tmp.Care();
                this.Dispatcher.Invoke(() =>
                {
                    tb2DateLastCare.Text = tmp.LastCare.ToString(@"dd/MM/yyyy");
                    tb2MileageLastCare.Text = tmp.LastCareMileage.ToString();
                    tb2fuel.Text = tmp.Fuel.ToString();
                    tmp.State = States.ready;
                    tb2status.Text = tmp.State.ToString();
                    Im2Status.Source = new BitmapImage(new Uri(tmp.Image, UriKind.Relative));
                    bCare.IsEnabled = true;
                });
                MessageBox.Show(str, "Care", MessageBoxButton.OK, MessageBoxImage.Information);
            });
            ThCare.Start();
            new Thread(() =>//for change time to ready
            {
                for (tmp.Time = 144; tmp.Time > 0; --tmp.Time)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        tb2StatusBar.Text = tmp.Time.ToString();
                    });
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                }
                this.Dispatcher.Invoke(() =>
                {
                    tb1StatusBar.Visibility = Visibility.Hidden;
                    tb2StatusBar.Visibility = Visibility.Hidden;
                });
            }).Start();
        }
    }
}
