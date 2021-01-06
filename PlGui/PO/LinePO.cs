using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace PO
{
    public class Line : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        ObservableCollection<StopLine> stopsInLine = new ObservableCollection<StopLine>();

        string numLine;
        Agency codeAgency;
        Areas area;
        string moreInfo;
        string first;
        string last;
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the number of the Line
        /// </summary>
        public string NumLine { get { return numLine; } set { if (numLine != value) { numLine = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the agency of the Line
        /// </summary>
        public Agency CodeAgency { get { return codeAgency; } set { if (codeAgency != value) { codeAgency = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the area of the Line
        /// </summary>
        public Areas Area { get { return area; } set { if (area != value) { area = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents all stops in the Line
        /// </summary>
        public ObservableCollection<StopLine> StopsInLine { get { return stopsInLine; } set { stopsInLine = new ObservableCollection<StopLine>(value); } }
        /// <summary>
        /// Represents the more info of about the Line
        /// </summary>
        public string MoreInfo { get { return moreInfo; } set { if (moreInfo != value) { moreInfo = value; OnPropertyChanged(); } } }

        public string NameFirstLineStop
        {
            get { return StopsInLine[0].Name; }
            set
            {
                if (value != first)
                {
                    first = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NameLastLineStop
        {
            get { return StopsInLine[StopsInLine.Count - 1].Name; }
            set
            {
                if (value != last)
                {
                    last = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
