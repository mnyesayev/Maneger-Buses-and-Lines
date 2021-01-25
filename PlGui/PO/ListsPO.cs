using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Lists : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        ObservableCollection<PO.BusStop> stops = new ObservableCollection<PO.BusStop>();
        ObservableCollection<PO.Bus> buses = new ObservableCollection<PO.Bus>();
        ObservableCollection<PO.Line> lines = new ObservableCollection<PO.Line>();
        ObservableCollection<BO.Driver> drivers = new ObservableCollection<BO.Driver>();
        List<BO.LineTiming> panelStation;
        public List<BO.LineTiming> PanelStation
        {
            get => panelStation; set { panelStation = value; }
        }
        public ObservableCollection<PO.BusStop> Stops
        {
            get { return stops; }
            set { if (value != stops) { stops = value; OnPropertyChanged(); } }
        }
        public ObservableCollection<PO.Bus> Buses
        {
            get { return buses; }
            set { if (value != buses) { buses = value; OnPropertyChanged(); } }
        }
        public ObservableCollection<PO.Line> Lines
        {
            get { return lines; }
            set { if (value != lines) { lines = value; OnPropertyChanged(); } }
        }
        public ObservableCollection<BO.Driver> Drivers
        {
            get { return drivers; }
            set { if (value != drivers) { drivers = value; OnPropertyChanged(); } }
        }
    }
}
