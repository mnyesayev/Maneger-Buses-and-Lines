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
    public class User:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        /// <summary>
        /// Represents the unique user name of the "User"
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Represents the unique password of the "User"
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Represents the authorization of the "User"
        /// </summary>
        public Authorizations Authorization { get; set; }

        /// <summary>
        /// Represents the phone  of the "User"
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Represents the birthday  of the "User"
        /// </summary>
        public DateTime Birthday { get; set; }

        public DateTime LogIn { get; set; }
    }
}
