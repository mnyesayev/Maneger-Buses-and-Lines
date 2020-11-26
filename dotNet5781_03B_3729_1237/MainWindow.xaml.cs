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
                var DRA = new DateTime(MyRandom.r.Next(2015, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                if (DRA.Year < 2018)
                    id = (uint)MyRandom.r.Next(1000000, 9999999);
                else
                    id = (uint)MyRandom.r.Next(10000000, 99999999);
                buses.Buses.Add(new Bus(DRA, id, (uint)MyRandom.r.Next(10000, 400000)
                , lastCare: new DateTime(DRA.Year + 1, DRA.Month, DRA.Day)));
            }
            // the function do mass in evrey bus. (mileage, last care, etc..)
            massBuses(buses);
            lbBuses.DataContext = buses.Buses;
        }

        // the function do mass in evrey bus. (mileage, last care, etc..)
        public void massBuses(ManageBuses buses)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) // Creates a need to do care
                {
                    buses.Buses[1].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[2].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[3].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[4].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                    buses.Buses[5].LastCare = new DateTime(MyRandom.r.Next(2018, 2019), MyRandom.r.Next(1, 11), MyRandom.r.Next(1, 28));
                }
                else if (i == 1)//Raises the mileage so that care will be needed soon
                {
                    buses.Buses[6].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[7].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[8].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[9].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                    buses.Buses[10].Mileage += (uint)MyRandom.r.Next(18500, 19995);
                }
                else if (i == 2)
                {
                    buses.Buses[7].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[8].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[9].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[10].Fuel = MyRandom.r.Next(10, 100);
                    buses.Buses[11].Fuel = MyRandom.r.Next(10, 100);
                }

            }
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

        }

        private void reful_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}

