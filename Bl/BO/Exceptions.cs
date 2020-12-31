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
    [Serializable]
    public class PasswordRecoveryException : Exception
    {
        public string Phone;
        public PasswordRecoveryException(string phone) : base() => Phone = phone;
        public PasswordRecoveryException(string phone, string message) :
            base(message) => Phone = phone;
        public PasswordRecoveryException(string phone, string message, Exception innerException) :
            base(message, innerException) => Phone = phone;
        public override string ToString() => base.ToString() + $", have problem with user: {Phone}";
    }
}
