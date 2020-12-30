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
        /// <summary>
        /// Represents the unique number of the "BusStop"
        /// </summary>
        public int Code { get { return code; } set { if (code!=value) { code = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the name of Bus stop/station
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents the address of Bus stop/station
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Represents more info about the stop/station 
        /// such as: Roof, disabled access, digital panel, etc.
        /// </summary>
        public string MoreInfo { get; set; }
        /// <summary>
        /// Represents the lines that pass in the stop/station 
        /// </summary>
        public ObservableCollection<Line> LinesPassInStop { get; }= new ObservableCollection<Line>();
    }

}
