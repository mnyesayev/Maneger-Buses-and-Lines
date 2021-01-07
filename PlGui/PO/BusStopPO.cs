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
    public class BusStop:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        int code;
        string name;
        string moreInfo;
        ObservableCollection<Line> lines = new ObservableCollection<Line>();
        /// <summary>
        /// Represents the unique number of the "BusStop"
        /// </summary>
        public int Code { get { return code; } set { if (code!=value) { code = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the name of Bus stop/station
        /// </summary>
        public string Name { get { return name; } set { if (name != value) { name = value;OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the longitude location
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Represents the latitude location
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Represents more info about the stop/station 
        /// such as: Roof, disabled access, digital panel, etc.
        /// </summary>
        public string MoreInfo { get { return moreInfo; } set{ if (moreInfo != value) { moreInfo = value; OnPropertyChanged(); } }}

        /// <summary>
        /// Represents the lines that pass in the stop/station 
        /// </summary>
        public ObservableCollection<Line> LinesPassInStop { get { return lines; }set { lines = new ObservableCollection<Line>(value); } }
    }

}
