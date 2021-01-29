using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PO
{
    public class Bus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        uint mileage;
        int fuel;
        uint lastCareMileage;
        DateTime lastCare;
        States state;
        public bool Active { get; set; }
        /// <summary>
        /// Represents the bus license number
        ///  by years as provided by law
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// Represents the bus license number in string
        ///  by years as provided by law
        /// </summary>
        public string PrintId
        {
            get
            {
                string temp = this.Id.ToString();
                if (this.DateRoadAscent.Year > 2017)
                {
                    temp = temp.Insert(3, "-");
                    temp = temp.Insert(6, "-");
                }
                if (this.DateRoadAscent.Year < 2018)
                {
                    temp = temp.Insert(2, "-");
                    temp = temp.Insert(6, "-");
                }
                return temp;
            }
            set { }
        }
        /// <summary>
        ///  Represents the date of ascent to the road.
        /// </summary>
        public DateTime DateRoadAscent { get; set; }
        /// <summary>
        ///  Represents the amount of mileage that the bus passed from entering the Road
        /// </summary>
        public uint Mileage { get => mileage; set { if (mileage != value) { mileage = value; OnPropertyChanged(); } } }
        /// <summary>
        ///  Represents how many miles the bus can travel further
        /// </summary>
        public int Fuel { get => fuel; set{ if (fuel!=value) { fuel = value; OnPropertyChanged(); } } }
        /// <summary>
        ///  Represents the date of the last treatment in the garage
        /// </summary>
        public DateTime LastCare { get => lastCare; set { if (lastCare != value) { lastCare = value; OnPropertyChanged(); } } }
        /// <summary>
        ///  Represents the amount of bus mileage in the last care
        /// </summary>
        public uint LastCareMileage { get => lastCareMileage; set { if (lastCareMileage != value) { lastCareMileage = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Represents the state of the bus
        /// </summary>
        public States State { get { return state; } set { if (state != value) { state = value; OnPropertyChanged(); } } }
        public string Image { get; set; }
    }
}
