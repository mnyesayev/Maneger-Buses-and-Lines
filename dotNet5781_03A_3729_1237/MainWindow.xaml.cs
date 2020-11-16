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

namespace dotNet5781_03A_3729_1237
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
        private Lines currentDisplayBusLine;
        Lines busLines = new Lines();
        List<BusStation> listStations = new List<BusStation>();
        public MainWindow()
        {
            InitializeComponent();
            // create rendom busStation
            for (int i = 1; i < 41; i++)
                listStations.Add(new BusStation(i));
            // create 10 lines
            for (int x = 0, y = 10; x < 10; ++x, ++y)
                busLines.AddLine(new Line(listStations[x], listStations[y], MyRandom.r.Next(1, 999)));
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine(busLines.AllLines[cbBusLines.SelectedIndex].NumLine);


        }
        private void ShowBusLine(int index)
        {

            currentDisplayBusLine = busLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.AllLines[0];
        }
        private void cbBusLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as Line).NumLine);
        }
    }
}
