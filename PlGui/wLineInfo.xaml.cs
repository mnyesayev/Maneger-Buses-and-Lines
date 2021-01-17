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
        PO.Lists Lists;
        public wLineInfo(IBL bl, PO.Lists lists)
        {
            InitializeComponent();
            this.bl = bl;
            Lists = lists;
        }

        private void DeleteStopLine_Click(object sender, RoutedEventArgs e)
        {
            PO.StopLine StopLine = (PO.StopLine)listViewLineInfo.SelectedItem;
            var idLine = (listViewLineInfo.DataContext as PO.Line).IdLine;
            BO.Line upline;
            try
            {
                upline = bl.DeleteStopLine(idLine, StopLine.CodeStop, StopLine.NumStopInLine);
                if (upline == null)
                    return;
            }
            catch (BO.DeleteException ex)
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int indexLine = Lists.Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
            var linePO = new PO.Line();
            Cloning.DeepCopyTo(upline, linePO);
            Lists.Lines[indexLine].StopsInLine = linePO.StopsInLine;
            Lists.Lines[indexLine].NameFirstLineStop = linePO.NameFirstLineStop;
            Lists.Lines[indexLine].NameLastLineStop = linePO.NameLastLineStop;
            //for old stop
            var indexdeleteStop = Lists.Stops.ToList().FindIndex((BusStop) => BusStop.Code == StopLine.CodeStop);
            var i = Lists.Stops[indexdeleteStop].LinesPassInStop.ToList().FindIndex(l => l.IdLine == idLine);
            Lists.Stops[indexdeleteStop].LinesPassInStop.RemoveAt(i);
        }

        private void ChangeStopLine_Click(object sender, RoutedEventArgs e)
        {
            PO.StopLine sl = (PO.StopLine)listViewLineInfo.SelectedItem;
            wEditSuccessiveStations wEdit = new wEditSuccessiveStations(bl);
            wEdit.tbcode1.Text = sl.CodeStop.ToString();
            wEdit.tbcode2.Text = sl.NextStop.ToString();
            wEdit.TimePicker.Text = sl.AvregeDriveTimeToNext.ToString(@"hh\:mm\:ss");
            wEdit.TBKmDis.Text = sl.DistanceToNext.ToString();
            wEdit.ShowDialog();
            if (wEdit.IsSave)
            {
                foreach (var item in Lists.Lines)
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

        private void addStopToLineInfo_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl,Lists);
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

        private void ShowDistanceAndTime_Click(object sender, RoutedEventArgs e)
        {
            DriveTime.Width = 100;
            Distance.Width = 100;
        }

        private void HideDistanceAndTime_Click(object sender, RoutedEventArgs e)
        {
            DriveTime.Width = 0;
            Distance.Width = 0;
        }

        private void addAfterStopToLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lists);
            addStopLine.DataContext = this.DataContext;
            addStopLine.tBNewIndex.Text = (listViewLineInfo.SelectedIndex + 2).ToString();
            addStopLine.ShowDialog();
        }

        private void addBeforeStopToLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lists);
            addStopLine.DataContext = this.DataContext;
            addStopLine.tBNewIndex.Text = (listViewLineInfo.SelectedIndex + 1).ToString();
            addStopLine.ShowDialog();
        }

        private void ExpandFrequency_Click(object sender, RoutedEventArgs e)
        {
            wEditTrip editTrip = new wEditTrip(bl, Lists);
            editTrip.line = ComboBoxLineInfo.SelectedItem as PO.Line;
            editTrip.ShowDialog();
        }
    }
}
