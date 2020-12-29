﻿using System;
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
    public class Line:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        string numLine;
        Agency codeAgency;
        Areas area;
        /// <summary>
        /// Represents the inner unique number of the "Line"
        /// </summary>
        public int IdLine { get; set; }
        /// <summary>
        /// Represents the number of the Line
        /// </summary>
        public string NumLine { get { return numLine; } set{ numLine = value; OnPropertyChanged(); }}
        /// <summary>
        /// Represents the agency of the Line
        /// </summary>
        public Agency CodeAgency { get { return codeAgency; } set { codeAgency = value;  OnPropertyChanged(); } }
        /// <summary>
        /// Represents the area of the Line
        /// </summary>
        public Areas Area {  get { return area; } set{ area = value;  OnPropertyChanged(); } }
        /// <summary>
        /// Represents all stops in the Line
        /// </summary>
        public ObservableCollection<StopLine> StopsInLine { get; } = new ObservableCollection<StopLine>(); 
        /// <summary>
        /// Represents the more info of about the Line
        /// </summary>
        public string MoreInfo { get { return MoreInfo; } set {  OnPropertyChanged(); } }
    
        public string NameFirstLineStop { get { return StopsInLine[0].Name;} }

        public string NameLastLineStop { get { return StopsInLine[StopsInLine.Count-1].Name;} }
    }
}