using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for wLineInfo.xaml
    /// </summary>
    public partial class wLineInfo : Window
    {
        IBL bl;

        ObservableCollection<PO.Line> Lines;
        ObservableCollection<PO.BusStop> Stops;
      
       
        public wLineInfo(IBL bl, ObservableCollection<PO.Line> lines, ObservableCollection<PO.BusStop> stops)
        {
            InitializeComponent();
            this.bl = bl;
            Lines = lines;
            Stops = stops;
          
        }

        private void DeleteStopLine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeStopLine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addStopToLineInInfo_CLick(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lines, Stops, listViewLineInfo);
            
            addStopLine.DataContext = this.DataContext;
            addStopLine.ShowDialog();
        }
    }
}
