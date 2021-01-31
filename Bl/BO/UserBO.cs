using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
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
        
    }
}
