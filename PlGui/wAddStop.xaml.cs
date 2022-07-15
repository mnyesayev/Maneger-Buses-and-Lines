﻿using System;
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
namespace PLGui
{
    /// <summary>
    /// Interaction logic for wAddStop.xaml
    /// </summary>
    public partial class wAddStop : Window
    {
        readonly IBL bl;
        PO.Lists Lists;
        public wAddStop(IBL bl, PO.Lists lists)
        {
            InitializeComponent();
            this.bl = bl;
            Lists = lists;
        }

        private void bAddStop_Click(object sender, RoutedEventArgs e)
        {
            tbNewStopCode.BorderBrush = default;
            tbNewStoplatitude.BorderBrush = default;
            tbNewStoplongitude.BorderBrush = default;
            tbNewStopName.BorderBrush = default;
            if (!int.TryParse(tbNewStopCode.Text, out int code))
                return;
            if (!double.TryParse(tbNewStoplatitude.Text, out double lat))
                return;
            if (!double.TryParse(tbNewStoplongitude.Text, out double lon))
                return;
            BO.BusStop busStop = default;
            try
            {
                busStop = new BO.BusStop() { Code = code, Longitude = lon, Latitude = lat, Name = tbNewStopName.Text };
                var upStop= bl.AddStop(busStop);
                if (upStop == null)
                {
                    MessageBox.Show($"the stop {code} already exits in system", "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (BO.AddException ex)
            {
                if (ex.Id == tbNewStopCode.Text)
                    tbNewStopCode.BorderBrush = Brushes.Red;
                if (ex.Id == tbNewStoplatitude.Text)
                    tbNewStoplatitude.BorderBrush = Brushes.Red;
                if (ex.Id == tbNewStoplongitude.Text)
                    tbNewStoplongitude.BorderBrush = Brushes.Red;
                if (ex.Id == tbNewStopName.Text)
                    tbNewStopName.BorderBrush = Brushes.Red;
                return;
            }
            var newStop = new PO.BusStop();
            busStop.DeepCopyTo(newStop);
            Lists.Stops.Add(newStop);
            Lists.Stops = new ObservableCollection<PO.BusStop>(Lists.Stops.OrderBy(stop => stop.Code));

            this.Close();
        }
    }
}
