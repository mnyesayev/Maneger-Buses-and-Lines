using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class StopLine: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        string name;
        int code;
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the unique code of the "BusStop"
        /// </summary>
        public int CodeStop { get { return code; } set { if (code != value) { code = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the number Stop/station in line
        /// </summary>
        public int NumStopInLine { get; set; }
        /// <summary>
        /// Represents the name of stop in route line
        /// </summary>
        public string Name { get { return name; } set {if (name != value) { name = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the unique code of the prev stop
        /// </summary>
        public int PrevStop { get; set; }
        /// <summary>
        /// Represents the uniqu code of the next stop
        /// </summary>
        public int NextStop { get; set; }
    }
}
