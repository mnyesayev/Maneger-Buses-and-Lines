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

        private void bReful_Click(object sender, RoutedEventArgs e)
        {
            Bus tmp = (Bus)this.DataContext;
            new Thread(() =>
            {
                tmp.State = States.refueling;
                Thread.Sleep(12000);
                var st = tmp.Refueling();
                MessageBox.Show(st, "Refuel", MessageBoxButton.OK, MessageBoxImage.Information);
                if (tmp.CheckCare())
                    tmp.State = States.mustCare;
                else
                    tmp.State = States.ready;
            }).Start();
            // refrash the fuel
            tb2fuel.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

        }

        private void bCare_Click(object sender, RoutedEventArgs e)
        {
            Bus tmp = (Bus)this.DataContext;
            new Thread(() =>
            {

                tmp.State = States.care;
                this.Dispatcher.Invoke(() => { tb2status.Text = tmp.State.ToString(); });
                Thread.Sleep(12000);
                var str = tmp.Care();
                this.Dispatcher.Invoke(() => 
                { tb2DateLastCare.Text = tmp.LastCare.ToString(@"dd/MM/yyyy");
                  tb2MileageLastCare.Text=tmp.LastCareMileage.ToString();
                  tmp.State = States.ready;
                  tb2status.Text = tmp.State.ToString();
                });
                
                MessageBox.Show(str, "Care", MessageBoxButton.OK, MessageBoxImage.Information);   
            }).Start();
        }
    }
}
