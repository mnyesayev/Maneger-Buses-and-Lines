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
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for wEditSuccessiveStations.xaml
    /// </summary>
    public partial class wEditSuccessiveStations : Window
    {
        IBL bl;
        public bool IsSave { get;private set; }
        public wEditSuccessiveStations(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void saveSucc_Click(object sender, RoutedEventArgs e)
        {
            if (TBKmDis.Text.Length == 0 || TimePicker == null)
                return;
            if (TimeSpan.TryParse(TimePicker.Text, out TimeSpan time) && double.TryParse(TBKmDis.Text, out double dis))
            {
                bl.InsertDistanceAndTime(int.Parse(tbcode1.Text), int.Parse(tbcode2.Text), dis, time);
                IsSave = true;
                this.Close();
            }
            else { MessageBox.Show("Enter only valid values!!", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void cencelSucc_Click(object sender, RoutedEventArgs e)
        {
            IsSave=false;
            this.Close();
        }
    }
}
