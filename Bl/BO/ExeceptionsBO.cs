using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class UserException : Exception
    {
        public string UserName;
        public UserException(string userName) : base() => UserName = userName;
        public UserException(string userName, string message) :
            base(message) => UserName = userName;
        public UserException(string userName, string message, Exception innerException) :
            base(message, innerException) => UserName = userName;
        public override string ToString() => base.ToString() + $", have problem with user: {UserName}";
    }
}
