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
    /// Interaction logic for wAddLine.xaml
    /// </summary>
    public partial class wAddLine : Window
    {
        IBL bl;
        ObservableCollection<PO.Line> Lines = new ObservableCollection<PO.Line>();
        ObservableCollection<PO.BusStop> Stops = new ObservableCollection<PO.BusStop>();
        public bool IsSuccssed { get; private set; }
        public wAddLine(IBL bL, ObservableCollection<PO.Line> lines,ObservableCollection<PO.BusStop> stops)
        {
            InitializeComponent();
            bl = bL;
            Lines = lines;
            Stops = stops;
            List<BO.Areas> areas = new List<BO.Areas>();
            for (int i = 0; i < 7; i++)
                areas.Add((BO.Areas)i);
            cbAddLineArea.ItemsSource = areas;

        }

        private void SaveAddLine_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(FirstStopCode.Text, out int code1) || code1 <= 0)
            {
                MessageBox.Show("The first stop input Invalid!", "First Stop ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(LastStopCode.Text, out int code2) || code2 <= 0)
            {
                MessageBox.Show("The last stop input Invalid!", "Last Stop ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(tbAddLineNumber.Text, out int numLine) || numLine <= 0)
            {
                MessageBox.Show("The number of line Invalid!", "Line Number ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(cbAddLineArea.SelectedItem is BO.Areas))
                return;

            if (bl.GetStop(code1) == null)
            {
                MessageBox.Show($"stop {code1} not exits in system", "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (bl.GetStop(code2) == null)
            {
                MessageBox.Show($"stop {code2} not exits in system", "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double dis = 0;
            TimeSpan time = default;
            try
            {
                dis = bl.GetDistance(code1, code2);
                time = bl.GetTime(code1, code2);
                IsSuccssed = true;
            }
            catch (BO.ConsecutiveStopsException ex)
            {
                wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                wEditSuccessive.tbcode1.Text = FirstStopCode.Text;
                wEditSuccessive.tbcode2.Text = LastStopCode.Text;
                wEditSuccessive.ShowDialog();
                if (wEditSuccessive.IsSave)
                {
                    dis = double.Parse(wEditSuccessive.TBKmDis.Text);
                    time = TimeSpan.Parse(wEditSuccessive.TimePicker.Text);
                    IsSuccssed = true;
                }
            }

            if (IsSuccssed == false)
                return;

            List<BO.StopLine> stops = new List<BO.StopLine>();
            stops.Add(new BO.StopLine { CodeStop = code1, NextStop = code2, DistanceToNext = dis, 
                AvregeDriveTimeToNext= time, NumStopInLine =1 });
            stops.Add(new BO.StopLine { CodeStop = code2, PrevStop = code1, NumStopInLine =2 });
            var area = (BO.Areas)cbAddLineArea.SelectedItem;
            try
            {
                PO.Line newLine = new PO.Line();
                var upLine = bl.AddLine(tbAddLineNumber.Text, area, stops, "");
                upLine.DeepCopyTo(newLine);
                Lines.Add(newLine);
                var index1=Stops.ToList().FindIndex((BusStop) => BusStop.Code == code1);
                var index2=Stops.ToList().FindIndex((BusStop) => BusStop.Code == code2);
                Stops[index1].LinesPassInStop.Add(newLine);
                Stops[index2].LinesPassInStop.Add(newLine);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Something is wrong here");
            }

        }

        private void CencelAddLine_Click(object sender, RoutedEventArgs e)
        {
            IsSuccssed = false;
            this.Close();
        }
    }
}
