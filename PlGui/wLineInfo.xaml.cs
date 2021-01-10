using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var StopLine = (PO.StopLine)(sender as Button).DataContext;
            BO.Line upline;
            try
            {
                upline = bl.DeleteStopLine(StopLine.IdLine, StopLine.CodeStop, StopLine.NumStopInLine);
                if (upline == null)
                    return;
            }
            catch (BO.DeleteException ex)
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int indexLine = Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
            var temp = new PO.Line();
            Cloning.DeepCopyTo(upline, temp);
            Lines[indexLine].StopsInLine = temp.StopsInLine;
            Lines[indexLine].NameFirstLineStop = temp.NameFirstLineStop;
            Lines[indexLine].NameLastLineStop = temp.NameLastLineStop;
            //for old stop
            var indexdeleteStop = Stops.ToList().FindIndex((BusStop) => BusStop.Code == StopLine.CodeStop);
            var upstop = bl.GetStop(StopLine.CodeStop);
            var tempStop1 = new PO.BusStop();
            upstop.DeepCopyTo(tempStop1);
            Stops[indexdeleteStop].LinesPassInStop = tempStop1.LinesPassInStop;
            //for other stops           
            new Thread(() =>
            {
                var tempLST = from stopLine in upline.StopsInLine
                              select stopLine.CodeStop;
                foreach (var item in tempLST)
                {
                    var indexStop = Stops.ToList().FindIndex((BusStop) => BusStop.Code == item);
                    var upStop = bl.GetStop(item);
                    var tempBusStop = new PO.BusStop();
                    Cloning.DeepCopyTo(upStop, tempBusStop);
                    Stops[indexStop].LinesPassInStop = tempBusStop.LinesPassInStop;
                }
            }).Start();
        }

        private void ChangeStopLine_Click(object sender, RoutedEventArgs e)
        {
            PO.StopLine sl = (PO.StopLine)(sender as Button).DataContext;
            wEditSuccessiveStations wEdit = new wEditSuccessiveStations(bl);
            wEdit.tbcode1.Text = sl.CodeStop.ToString();
            wEdit.tbcode2.Text = sl.NextStop.ToString();
            wEdit.TimePicker.Text = sl.AvregeDriveTimeToNext.ToString(@"hh\:mm\:ss");
            wEdit.TBKmDis.Text = sl.DistanceToNext.ToString();
            wEdit.ShowDialog();
            if (wEdit.IsSave)
            {
                foreach (var item in Lines)
                {
                    var index = item.StopsInLine.ToList().FindIndex((StopLine) =>
                      { return StopLine.CodeStop == sl.CodeStop && StopLine.NextStop == sl.NextStop; });
                    if (index != -1)
                    {
                        var upstopLine = bl.GetStopInLine(sl.CodeStop, item.IdLine);
                        var temp = new PO.StopLine();
                        upstopLine.DeepCopyTo(temp);
                        item.StopsInLine[index].DistanceToNext = temp.DistanceToNext;
                        item.StopsInLine[index].AvregeDriveTimeToNext = temp.AvregeDriveTimeToNext;
                    }
                }
            }
        }

        private void addStopToLineInInfo_CLick(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lines, Stops);
            addStopLine.DataContext = this.DataContext;
            addStopLine.ShowDialog();
        }

        private void ComboBoxLineInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxLineInfo.SelectedItem is PO.Line)
            {
                this.DataContext = ComboBoxLineInfo.SelectedItem;
            }
        }
    }
}
