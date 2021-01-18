using BlApi;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for wEditTrip.xaml
    /// </summary>
    public partial class wEditTrip : Window
    {
        IBL bl;
        PO.Lists Lists;
        public wEditTrip(IBL bL, PO.Lists lists)
        {
            InitializeComponent();
            bl = bL;
            Lists = lists;
        }

        private void bAddNewfrequency_Click(object sender, RoutedEventArgs e)
        {
            var line = this.DataContext as PO.Line;
            if (TPstartTime == null || TBfrequency.Text.Length == 0)
                return;
            if (!TimeSpan.TryParse(TPstartTime.Text, out TimeSpan ss))
                return;
            if (TBfrequency.Text != "0")
            {
                if (TPendTime == null) return;
                if (!TimeSpan.TryParse(TPendTime.Text, out TimeSpan ee)) return;
            }
            try
            {
                BO.LineTrip lineTrip = bl.AddLineTrip(line.IdLine,
                                                      TimeSpan.Parse(TPstartTime.Text),
                                                      (TBfrequency.Text == "0") ? default : TimeSpan.Parse(TPendTime.Text),
                                                      int.Parse(TBfrequency.Text));
                PO.LineTrip newLIneTrip = new PO.LineTrip();
                lineTrip.DeepCopyTo(newLIneTrip);
                int index = Lists.Lines.ToList().FindIndex(l => l.IdLine == line.IdLine);
                var temp = Lists.Lines[index].Trips.ToList();
                temp.AddRange(newLIneTrip.DepartureSchedule);
                Lists.Lines[index].Trips = new ObservableCollection<PO.TripOnLine>(temp.OrderBy(lt => lt.Time));
            }
            catch (BO.AddException ex)
            {
                if (ex.Id == TBfrequency.Text)
                    MessageBox.Show(ex.Message, "Add ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show($"This range of times: {ex.Id} invalid!", "Add ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void bEditFrequency_Click(object sender, RoutedEventArgs e)
        {
            if (TPstartTime == null || TBfrequency.Text.Length == 0)
                return;
            if (!TimeSpan.TryParse(TPstartTime.Text, out TimeSpan ss))
                return;
            if (TBfrequency.Text != "0")
            {
                if (TPendTime == null) return;
                if (!TimeSpan.TryParse(TPendTime.Text, out TimeSpan ee)) return;
            }
            if (ListViewFrequency.SelectedItem is PO.TripOnLine)
            {
                var tripOnLine = ListViewFrequency.SelectedItem as PO.TripOnLine;
                try
                {
                    var line = this.DataContext as PO.Line;
                    var lineTrip = bl.UpdateLineSchedule(tripOnLine.Id,
                                                         TimeSpan.Parse(TPstartTime.Text),
                                                         (TBfrequency.Text == "0") ? default : TimeSpan.Parse(TPendTime.Text),
                                                         int.Parse(TBfrequency.Text));
                    PO.LineTrip newLIneTrip = new PO.LineTrip();
                    lineTrip.DeepCopyTo(newLIneTrip);
                    int index = Lists.Lines.ToList().FindIndex(l => l.IdLine == line.IdLine);
                    var temp = Lists.Lines[index].Trips.ToList();
                    temp.RemoveAll(tOL => tOL.Id == tripOnLine.Id);//delete the old range trips
                    temp.AddRange(newLIneTrip.DepartureSchedule);//add the range trips update
                    Lists.Lines[index].Trips = new ObservableCollection<PO.TripOnLine>(temp.OrderBy(lt => lt.Time));
                }
                catch (BO.IdException ex)
                {
                    if (ex.Id == TBfrequency.Text || ex.Id == tripOnLine.Id.ToString())
                        MessageBox.Show(ex.Message, "Edit ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        MessageBox.Show($"This range of times: {ex.Id} invalid!", "Edit ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void bDeleteRange_click(object sender, RoutedEventArgs e)
        {
            if (ListViewFrequency.SelectedItem is PO.TripOnLine)
            {
                var tripOnLine = ListViewFrequency.SelectedItem as PO.TripOnLine;
                try
                {
                    bl.DeleteRangeTrips(tripOnLine.Id);
                    var idLine = (this.DataContext as PO.Line).IdLine;
                    PO.LineTrip newLIneTrip = new PO.LineTrip();
                    int index = Lists.Lines.ToList().FindIndex(l => l.IdLine == idLine);
                    var temp = Lists.Lines[index].Trips.ToList();
                    temp.RemoveAll(tOL => tOL.Id == tripOnLine.Id);//delete the old range trips
                    Lists.Lines[index].Trips = new ObservableCollection<PO.TripOnLine>(temp);
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong here", "Delete ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TBfrequency_preKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }

        private void ListViewFrequency_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewFrequency.SelectedItem is PO.TripOnLine)
            {
                var tripOnLine = ListViewFrequency.SelectedItem as PO.TripOnLine;
                var times = tripOnLine.StartAneEnd.Split('-');
                TPstartTime.Text = times[0];
                TPendTime.Text = times[1];
                TBfrequency.Text = tripOnLine.Frequency.ToString();
            }
        }
    }
}
