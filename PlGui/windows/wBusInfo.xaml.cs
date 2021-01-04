//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PlGui
//{
//    /// <summary>
//    /// Interaction logic for wBusInfo.xaml
//    /// </summary>
//    public partial class wBusInfo : Window
//    {

//        public wBusInfo()
//        {
//            InitializeComponent();

//        }
//        Thread thCare;
//        Thread thFuel;
//        public Thread ThCare { get => thCare; private set => thCare = value; }
//        public Thread ThFuel { get => thFuel; private set => thFuel = value; }


//        /// <summary>
//        /// start Thread for refuel the bus
//        /// </summary>
//        private void bRefuel_Click(object sender, RoutedEventArgs e)
//        {
//            Bus tmp = (Bus)this.DataContext;
//            if (tmp.State == States.refueling) return;//Protection test
//            ThFuel = new Thread(() =>
//            {
//                tmp.State = States.refueling;
//                tmp.Image = "images\\yellow.png";
//                // update GUI
//                this.Dispatcher.Invoke(() =>
//                {
//                    bCare.IsEnabled = false;
//                    bRefuel.IsEnabled = false;
//                    tb1StatusBar.Visibility = Visibility.Visible;
//                });
//                tmp.Time = 12;
//                for (; tmp.Time > 0; --tmp.Time)
//                {
//                    Thread.Sleep(new TimeSpan(0, 0, 1));
//                }
//                var st = tmp.Refueling();
//               // update GUI
//                this.Dispatcher.Invoke(() =>
//                {
//                    if (tmp.CheckCare())
//                    {
//                        tmp.State = States.mustCare;
//                        tmp.Image = "images\\red.png";
//                    }
//                    else
//                    {
//                        tmp.State = States.ready;
//                        tmp.Image = "images\\green.png";
//                    }
//                    bCare.IsEnabled = true;
//                    tb1StatusBar.Visibility = Visibility.Hidden;
//                });
//                MessageBox.Show(st, "Refuel", MessageBoxButton.OK, MessageBoxImage.Information);
//            });
//            ThFuel.Start();
//        }

//        /// <summary>
//        /// start Thread for start care the bus
//        /// </summary>
//        private void bCare_Click(object sender, RoutedEventArgs e)
//        {
//            Bus tmp = (Bus)this.DataContext;
//            if (tmp.State == States.care) return; //Protection test
//            ThCare = new Thread(
//            () =>
//            {
//                tmp.State = States.care;
//                tmp.Image = "images\\orange.png";
//                // update GUI
//                this.Dispatcher.Invoke(() =>
//                {
//                    bCare.IsEnabled = false;
//                    bRefuel.IsEnabled = false;
//                    tb1StatusBar.Visibility = Visibility.Visible;
//                });
//                tmp.Time = 144;
//                for (; tmp.Time > 0; --tmp.Time)
//                {
//                    Thread.Sleep(new TimeSpan(0, 0, 1));
//                }
//                var str = tmp.Care();
//                // update GUI
//                this.Dispatcher.Invoke(() =>
//                {
//                    tb1StatusBar.Visibility = Visibility.Hidden;
//                    tmp.State = States.ready;
//                    bCare.IsEnabled = true;
//                });
//                MessageBox.Show(str, "Care", MessageBoxButton.OK, MessageBoxImage.Information);
//            });
//            ThCare.Start();
//        }
//    }
//}
