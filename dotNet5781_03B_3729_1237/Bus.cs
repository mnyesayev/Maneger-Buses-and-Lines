using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace dotNet5781_03B_3729_1237
{
    public enum States
    {
        ready, drive, refueling, care, mustCare, mustRefuel
    }
    /// <summary>
    /// The class represents a single bus
    /// with functionality to monitor its integrity and suitability for travel
    /// </summary>
    public class Bus : INotifyPropertyChanged
    {
        uint id;
        uint mileage;
        int fuel;
        uint lastCareMileage;
        DateTime dateRoadAscent;
        DateTime lastCare;
        States state;
        string image;
        int time;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// Represents the bus license number
        ///  by years as provided by law
        /// </summary>
        public uint Id { get => id; private set => id = value; }

        /// <summary>
        /// Displays the date of ascent to the road.
        /// </summary>
        public DateTime DateRoadAscent
        { get => dateRoadAscent; private set => dateRoadAscent = value; }

        /// <summary>
        /// Shows the amount of mileage that the bus passed from entering the Road
        /// </summary>
        public uint Mileage
        {
            get => mileage;
            set
            {
                if (!(mileage >= value))
                {
                    mileage = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Shows how many miles the bus can travel further
        /// </summary>
        public int Fuel
        {
            get => fuel;
            set
            {
                fuel = value;
                if (CheckFuel(0))
                {
                    State = States.mustRefuel;
                    Image = "images\\red.png";
                }
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Displays the date of the last treatment in the garage
        /// </summary>
        public DateTime LastCare
        {
            get => lastCare;
            set
            {
                if (value == default || value < DateRoadAscent)
                    lastCare = DateRoadAscent;
                else
                    lastCare = value;
                if (CheckCare())
                {
                    State = States.mustCare;
                    Image = "images\\red.png";
                }
                else
                {
                    state = States.ready;
                    Image = "images\\green.png";
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Displays the amount of bus mileage in the last care
        /// </summary>
        public uint LastCareMileage
        {
            get => lastCareMileage;
            set
            {
                if (value == 0 || value < Mileage)
                {
                    lastCareMileage = Mileage;
                }
                else
                    lastCareMileage = value;
                OnPropertyChanged();
            }
        }
        public States State
        {
            get => state; set
            {
                state = value; OnPropertyChanged();
            }
        }

        public string Image
        {
            get => image; set
            {
                image = value; OnPropertyChanged();
            }
        }
        public int Time
        {
            get => time; set
            {
                time = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// A Ctor who creates a bus and also serves as a default Ctor
        /// </summary>
        /// <param name="dateRoadAscent"></param>
        /// <param name="id"></param>
        /// <param name="mileage"></param>
        /// <param name="fuel"></param>
        public Bus(DateTime dateRoadAscent = default, uint id = 0, uint mileage = 0, int fuel = 1200,
            DateTime lastCare = default, uint lastCareMileage = 0)
        {
            DateRoadAscent = dateRoadAscent;
            Id = id;
            Mileage = mileage;
            Fuel = fuel;
            LastCare = lastCare;
            LastCareMileage = lastCareMileage;
            if (CheckCare())
            {
                State = States.mustCare;
                Image = "images\\red.png";
            }
            else
            {
                State = States.ready;
                Image = "images\\green.png";
            }
        }
        /// <summary>
        /// The function updates the last treatment date and saves its mileage
        /// </summary>
        public string Care()
        {
            LastCare = DateTime.Now;
            LastCareMileage = mileage;
            this.Refueling();
            return string.Format("Bus {0} can drive now another 20,000 miles safely :)",PrintId);

        }
        /// <summary>
        /// The function refuels the bus to a full tank
        /// </summary>
        public string Refueling()
        {
            Fuel = 1200;
            return string.Format("To Bus: {0} have now full tank :)",PrintId);
        }
        /// <summary>
        /// The func. checks if the bus has passed a year since the last care
        /// or drive another 20000 mileage 
        /// </summary>
        /// <returns>false if the bus not need a care</returns>
        public bool CheckCare()
        {
            if (DateTime.Compare(lastCare, DateTime.Now.AddYears(-1)) <= 0)
                return true;
            if (Mileage - LastCareMileage >= 20000)
                return true;
            return false;
        }
        /// <summary>
        /// The func. checks if the bus has passed a 20 thousand kilometers 
        /// by "addMileage"
        /// </summary>
        ///<param name="addMileage"></param>
        /// <returns>false if the bus not need a care</returns>
        public bool CheckCare(uint addMileage)
        {
            if (Mileage + addMileage - LastCareMileage >= 20000)
                return true;
            return false;
        }
        /// <summary>
        /// Shows whether a bus can travel with the fuel it has
        /// </summary>
        /// <param name="subFuel"></param>
        /// <returns>false if fuel enough to current drive</returns>
        public bool CheckFuel(uint subFuel)
        {
            if (Fuel - subFuel > 0)
                return false;
            return true;
        }
        /// <summary>
        /// Adds the "addmileage" in the "mileage" and reduces the fuel accordingly
        /// </summary>
        /// <param name="addMileage"></param>
        public void StartDrive(uint addMileage)
        {
            Mileage += addMileage;
            Fuel -= (int)addMileage;
        }

        /// <summary>
        /// returns how meny mileage the bus was driving - from the last care
        /// </summary>
        /// <returns></returns>
        public uint ReturnLastCare()
        {
            return (Mileage - LastCareMileage);
        }
        /// <summary>
        ///A function converts the vehicle license to a string
        /// </summary>
        /// <returns>string at format 12-345-67(Until 2017 inclusive),123-45-678(2018 onwards)</returns>
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
                    temp = temp.Insert(9, "  ");
                }
                return temp;
            }
        }

        public override string ToString()
        {
            return $"{PrintId}       {DateRoadAscent.ToString(@"dd/MM/yyyy")}    {Mileage}";
        }
    }

}
