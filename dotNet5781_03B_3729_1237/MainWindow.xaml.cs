using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03B_3729_1237
{
    static class MyRandom
    {
        public static Random r = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// Returns an real random number between two ranges(min,max)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>an real random number between two ranges(min,max)</returns>
        public static double GetDoubleRandom(double min, double max)
        {
            return (r.NextDouble() * (max - min)) + min;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        // amount of buses
        const int NumBuses = 15;
        private ManageBuses buses = new ManageBuses();
        // create list of buses
        public MainWindow()
        {
            InitializeComponent();

            // randem id to the bus
            uint id;
            for (int i = 0; i < NumBuses; i++)
            {
                var DRA = new DateTime(MyRandom.r.Next(2016, 2020), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                if (DRA.Year < 2018)
                    id = (uint)MyRandom.r.Next(1000000, 9999999);
                else
                    id = (uint)MyRandom.r.Next(10000000, 99999999);
                buses.Buses.Add(new Bus(DRA, id, (uint)MyRandom.r.Next(10000, 400000)
                , lastCare: new DateTime(DRA.Year + 1, DRA.Month, DRA.Day)));
            }
            // the function do mass in evrey bus. (mileage, last care, etc..)
            massBuses(buses);
            lvBuses.DataContext = buses.Buses;
        }

        // the function do mass in evrey bus. (mileage, last care, etc..)
        public void massBuses(ManageBuses buses)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) // Creates a need to do care
                {
                    buses.Buses[0].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[1].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[2].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[3].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[4].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                }
                else if (i == 1)//Raises the mileage so that care will be needed soon
                {
                    buses.Buses[5].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[6].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[7].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[8].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[9].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                }
                else if (i == 2)
                {
                    buses.Buses[10].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[11].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[12].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[13].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[14].Fuel = MyRandom.r.Next(10, 100);
                }

            }
        }


        GridViewColumnHeader lastHeaderClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;


        private void GridViewColumnHeader_ClickedHandler(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is GridViewColumnHeader ch))
                return;
            var dir = ListSortDirection.Ascending;
            if (ch == lastHeaderClicked && lastDirection == ListSortDirection.Ascending)
                dir = ListSortDirection.Descending;
            sort(ch, dir);
            lastHeaderClicked = ch;
            lastDirection = dir;
        }
        private void bAddBus_Click(object sender, RoutedEventArgs e)
        {
            wAddBus wAdd = new wAddBus();
            wAdd.ShowDialog();

            if (wAdd.NewBus != null && buses.SearchBus(wAdd.NewBus.Id) != null)
                MessageBox.Show("This bus alrsdy exsist!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (wAdd.NewBus != null)
                buses.Buses.Add(wAdd.NewBus);

        }


        private void startDrive_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var bus = (Bus)button.DataContext;
            if (bus.State == States.care || bus.State == States.drive || bus.State == States.refueling)
                return;
            wStartDrive drive = new wStartDrive();
            drive.DataContext = button.DataContext;
            drive.ShowDialog();
            new Thread(() =>
            {
                while (drive.ThStartDrive != null && drive.ThStartDrive.IsAlive)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    continue;
                }
                this.Dispatcher.Invoke(() =>
                {
                    lvBuses.Items.Refresh();
                });
            }).Start();
            lvBuses.Items.Refresh();
        }

        private void reful_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.DataContext is Bus)
            {
                Bus tmp = (Bus)button.DataContext;
                if (tmp.State == States.refueling || tmp.State == States.drive || tmp.State == States.care)
                    return;
                Thread thMainFuel = new Thread(() =>
                {
                     tmp.State = States.refueling;
                     tmp.Image = "images\\yellow.png";
                     Thread.Sleep(new TimeSpan(0, 0, 12));
                     var st = tmp.Refueling();
                     MessageBox.Show(st, "Refuel", MessageBoxButton.OK, MessageBoxImage.Information);
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
                 });
                thMainFuel.Start();
                new Thread(() =>//for change time to ready
                {
                    for (tmp.Time = 12; tmp.Time > 0; --tmp.Time)
                    {
                       
                        Thread.Sleep(new TimeSpan(0, 0, 1));
                    }
                }).Start();
                new Thread(() =>
                {
                    while (thMainFuel.IsAlive)
                        continue;
                    this.Dispatcher.Invoke(() =>
                    {
                        lvBuses.Items.Refresh();
                    });
                }).Start();
            }
            lvBuses.Items.Refresh();
        }

        private void lbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wBusInfo busInfo = new wBusInfo();
            if (lvBuses.SelectedItem is Bus)
            {
                Bus bus = (Bus)lvBuses.SelectedItem;
                busInfo.DataContext = lvBuses.SelectedItem;
                if (bus.Fuel < 1200)
                    busInfo.bRefuel.IsEnabled = true;
                if(bus.Time!=0)
                {
                    busInfo.tb1StatusBar.Visibility = Visibility.Visible;
                    busInfo.tb2StatusBar.Visibility = Visibility.Visible;
                    new Thread(() => 
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            if (bus.State == States.refueling)
                                busInfo.bRefuel.IsEnabled = false;
                        });
                        while (bus.Time!=0)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                busInfo.tb2StatusBar.Text =bus.Time.ToString();
                            });
                            Thread.Sleep(new TimeSpan(0, 0, 1));
                        }
                        this.Dispatcher.Invoke(() =>
                        {
                            busInfo.tb2fuel.Text = bus.Fuel.ToString();
                            busInfo.tb2MileageLastCare.Text = bus.LastCareMileage.ToString();
                            busInfo.tb2DateLastCare.Text = bus.LastCare.ToString(@"dd/MM/yyyy");
                            busInfo.tb2Mileage.Text = bus.Mileage.ToString();
                            busInfo.tb2status.Text = bus.State.ToString();
                            busInfo.Im2Status.Source = new BitmapImage(new Uri(bus.Image, UriKind.Relative));
                            busInfo.tb1StatusBar.Visibility = Visibility.Hidden;
                            busInfo.tb2StatusBar.Visibility = Visibility.Hidden;
                        });                       
                    }).Start();
                }
                busInfo.ShowDialog();
                new Thread(() =>
                {
                    while (busInfo.ThCare != null && busInfo.ThCare.IsAlive)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                        continue;
                    }
                    while (busInfo.ThFuel != null && busInfo.ThFuel.IsAlive)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                        continue;
                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        lvBuses.Items.Refresh();
                    });
                }).Start();
                lvBuses.Items.Refresh();
            }
        }

        private void sort(GridViewColumnHeader ch, ListSortDirection dir)
        {
            var bn = (ch.Column.DisplayMemberBinding as Binding)?.Path.Path;
            if (bn == "PrintId")
                bn = "Id" ?? ch.Column.Header as string;//sort header of PrintId according to Id
            else
            {
                bn = bn ?? ch.Column.Header as string;
                if (bn == "Options")
                    return;//not should sorted. 
                if (bn == "Status")
                    bn = "State";//sort header of Status according to State
            }
            var dv = CollectionViewSource.GetDefaultView(lvBuses.ItemsSource);
            dv.SortDescriptions.Clear();
            var sd = new SortDescription(bn, dir);
            dv.SortDescriptions.Add(sd);
            dv.Refresh();
        }



        private void Click_bDelBus(object sender, RoutedEventArgs e)
        {

        }

    }
}


