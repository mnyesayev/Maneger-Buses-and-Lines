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
namespace PLGui
{
    /// <summary>
    /// Interaction logic for addStopLine.xaml
    /// </summary>
    public partial class addStopLine : Window
    {
        readonly IBL bl;
        //ObservableCollection<PO.Line> Lines;
        //ObservableCollection<PO.BusStop> Stops;
        PO.Lists Lists;
        public bool IsSuccessed { get; private set; }
        public addStopLine(IBL bl, PO.Lists lists)
        {
            InitializeComponent();
            this.bl = bl;
            Lists = lists;
        }

        private void buttonAddStopLine_Click(object sender, RoutedEventArgs e)
        {
            if (tBCode.Text.Length == 0 || tBNewIndex.Text.Length == 0)
                return;
            tBNewIndex.BorderBrush = default;
            tBCode.BorderBrush = default;
            if (!int.TryParse(tBCode.Text, out int code))
            {
                tBCode.BorderBrush = Brushes.Red;
                return;
            }
            var idLine = (this.DataContext as PO.Line).IdLine;
            IsSuccessed = false;
            BO.Line upline = null;
            
            try
            {
                
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
                    tBNewIndex.BorderBrush = Brushes.Red;
                if (ex.Id == tBCode.Text)
                    tBCode.BorderBrush = Brushes.Red;
                return;
            }
            catch (BO.ConsecutiveStopsException ex)
            {
                wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                wEditSuccessive.tbcode1.Text = ex.Code1.ToString();
                wEditSuccessive.tbcode2.Text = ex.Code2.ToString();
                wEditSuccessive.tbName1.Text = bl.GetNameStop(ex.Code1);
                wEditSuccessive.tbName2.Text = bl.GetNameStop(ex.Code2);
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
                    upline = bl.AddStopLine(idLine, code, int.Parse(tBNewIndex.Text));
                    IsSuccessed = true;
                }
                catch (BO.ConsecutiveStopsException ex)
                {
                    wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                    wEditSuccessive.tbcode1.Text = ex.Code1.ToString();
                    wEditSuccessive.tbcode2.Text = ex.Code2.ToString();
                    wEditSuccessive.tbName1.Text = bl.GetNameStop(ex.Code1);
                    wEditSuccessive.tbName2.Text = bl.GetNameStop(ex.Code2);
                    wEditSuccessive.ShowDialog();
                    if (wEditSuccessive.IsSave == false)
                    {
                        this.Close();
                        return;
                    }
                }
            }
            new Thread(() => 
            {
                int index = Lists.Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
                var linePO = new PO.Line();
                Cloning.DeepCopyTo(upline, linePO);
                Lists.Lines[index].StopsInLine = linePO.StopsInLine;
                Lists.Lines[index].NameFirstLineStop = linePO.NameFirstLineStop;
                Lists.Lines[index].NameLastLineStop = linePO.NameLastLineStop;
                var indexStop = Lists.Stops.ToList().FindIndex((BusStop) => BusStop.Code == code);
                var lineInStop = new PO.LineOnStop();
                Lists.Lines[index].DeepCopyTo(lineInStop);
                Lists.Stops[indexStop].LinesPassInStop.Add(lineInStop);
            }).Start();
           
            this.Close();
        }

        private void tBNewIndex_preKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }

        private void tBCode_preKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }
    }
}
