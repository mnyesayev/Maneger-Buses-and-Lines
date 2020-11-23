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

        public MainWindow()
        {
            InitializeComponent();

            // create list of buses
            ManageBuses buses = new ManageBuses();
            // randem id to the bus
            for (int i = 0; i < NumBuses; i++)
                buses.AddBus(new DateTime(2018, 11, 23), (uint)MyRandom.r.Next(10000000, 99999999));
            // the function do mass in evrey bus. (mileage, last care, etc..)
            massBuses(buses);
        }

        // the function do mass in evrey bus. (mileage, last care, etc..)
        public void massBuses(ManageBuses buses)
        {
            
            for (int i = 0; i < 4; i++)
            {

                if (i == 1) // Creates a need to do care
                { 
                    buses.Buses[1].LastCare = new DateTime(2019, 10, 23);
                    buses.Buses[2].LastCare = new DateTime(2019, 8, 8);
                    buses.Buses[3].LastCare = new DateTime(2019, 6, 17);
                }
                  
            }
        }
    }
}

