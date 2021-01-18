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
    public class LineTrip : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// Represents the unique  number of the "LineTrip"
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the unique inner number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        TimeSpan start;
        TimeSpan end;
        ObservableCollection<TripOnLine> departureSchedule = new ObservableCollection<TripOnLine>();
        /// <summary>
        /// Represents the frequency of line per hour
        /// </summary>
        public int Frequency { get; set; }
        public ObservableCollection<TripOnLine> DepartureSchedule
        {
            get { return departureSchedule; }
            set
            {
                if (departureSchedule != value)
                {
                    departureSchedule = new ObservableCollection<TripOnLine>(value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Represents the time of start line
        /// </summary>
        public TimeSpan StartTime
        {
            get { return DepartureSchedule[0].Time; }
            set { if (start != value) { start = value; OnPropertyChanged(); } }
        }
        /// <summary>
        /// Represents the time of end line
        /// </summary>
        public TimeSpan EndTime
        {
            get { return DepartureSchedule[departureSchedule.Count-1].Time; }
            set { { if (end != value) { end = value; OnPropertyChanged(); } } }
        }
    }
}
