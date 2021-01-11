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
using System.Threading;
using System.Collections.ObjectModel;
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for addStopLine.xaml
    /// </summary>
    public partial class addStopLine : Window
    {
        IBL bl;
        ObservableCollection<PO.Line> Lines;
        ObservableCollection<PO.BusStop> Stops;
        public bool IsSuccessed { get; private set; }
        public addStopLine(IBL bl,ObservableCollection<PO.Line> lines, ObservableCollection<PO.BusStop> stops)
        {
            InitializeComponent();
            this.bl = bl;
            Lines = lines;
            Stops = stops;
            
        }

        private void buttonAddStopLine_Click(object sender, RoutedEventArgs e)
        {
            if (tBCode.Text.Length == 0 || tBNewIndex.Text.Length == 0)
                return;
            tBNewIndex.Background = default;
            tBCode.Background = default;
            var idLine = (this.DataContext as PO.Line).IdLine;
            IsSuccessed = false;
            BO.Line upline = null;
            try
            {
                int code = int.Parse(tBCode.Text);
                upline = bl.AddStopLine(idLine, code, int.Parse(tBNewIndex.Text));               
                if (upline == null)
                {
                    MessageBox.Show($"stop {code} not exits in system", "Add Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                IsSuccessed = true;
            }
            catch (BO.AddException ex)
            {
                if (ex.Id == tBNewIndex.Text)
                    tBNewIndex.Background = Brushes.Red;
                if (ex.Id == tBCode.Text)
                    tBCode.Background = Brushes.Red;
                return;
            }
            catch (BO.ConsecutiveStopsException ex)
            {
                wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                wEditSuccessive.tbcode1.Text = ex.Code1.ToString();
                wEditSuccessive.tbcode2.Text = ex.Code2.ToString();
                wEditSuccessive.ShowDialog();
                if (wEditSuccessive.IsSave == false)
                {
                    this.Close();
                    return;
                }
            }
            while (IsSuccessed == false)
            {
                try
                {
                    int code =int.Parse(tBCode.Text);
                    upline=bl.AddStopLine(idLine, code, int.Parse(tBNewIndex.Text));
                    IsSuccessed = true;
                }
                catch (BO.ConsecutiveStopsException ex)
                {
                    wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                    wEditSuccessive.tbcode1.Text = ex.Code1.ToString();
                    wEditSuccessive.tbcode2.Text = ex.Code2.ToString();
                    wEditSuccessive.ShowDialog();
                    if (wEditSuccessive.IsSave == false)
                    {
                        this.Close();
                        return;
                    }
                }
            }
            int index=Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
            var temp = new PO.Line();
            Cloning.DeepCopyTo(upline, temp);
            Lines[index].StopsInLine=temp.StopsInLine;
            Lines[index].NameFirstLineStop = temp.NameFirstLineStop;
            Lines[index].NameLastLineStop = temp.NameLastLineStop;
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
            this.Close();
        }
    }
}
