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

        private void bReful_Click(object sender, RoutedEventArgs e)
        {
            Bus tmp = (Bus)this.DataContext;
            var st = tmp.Refueling();
            MessageBox.Show(st, "Refuel", MessageBoxButton.OK, MessageBoxImage.Information);
            // need to refrash the window
        }

        private void bCare_Click(object sender, RoutedEventArgs e)
        {
            Bus tmp = (Bus)this.DataContext;
            var st = tmp.Care();
            MessageBox.Show(st, "Care", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
