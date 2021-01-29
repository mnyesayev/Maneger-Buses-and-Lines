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
using BlApi;
using PO;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for wBusInfo.xaml
    /// </summary>
    public partial class wBusInfo : Window
    {
        IBL bl;
        
        public wBusInfo(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            
        }
        Thread thCare;
        Thread thFuel;
        public Thread ThCare { get => thCare; private set => thCare = value; }
        public Thread ThFuel { get => thFuel; private set => thFuel = value; }


        /// <summary>
        /// start Thread for refuel the bus
        /// </summary>
        private void bRefuel_Click(object sender, RoutedEventArgs e)
        {
            var bus= (Bus)DataContext;
            var temp = new BO.Bus();
            bus.DeepCopyTo(temp); 
            var updateBus=bl.Fuel(temp);
            bus.Fuel = updateBus.Fuel;
        }

        /// <summary>
        /// start Thread for start care the bus
        /// </summary>
        private void bCare_Click(object sender, RoutedEventArgs e)
        {
            var bus = (Bus)DataContext;
            var temp = new BO.Bus();
            bus.DeepCopyTo(temp);
            var updateBus = bl.Care(temp);
            updateBus.DeepCopyTo(bus);
        }
    }
}
