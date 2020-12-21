using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class UserExceptionDO : Exception
    {
        public string UserName;
        public UserExceptionDO(string userName) : base() => UserName = userName;
        public UserExceptionDO(string userName, string message) :
            base(message) => UserName = userName;
        public UserExceptionDO(string userName, string message, Exception innerException) :
            base(message, innerException) => UserName = userName;
        public override string ToString() => base.ToString() + $", have problem with user: {UserName}";
    }

    [Serializable]
    public class BusExceptionDO : Exception
    {
        public int Id;
        public BusExceptionDO(int id) : base() => Id = id;
        public BusExceptionDO(int id, string message) :
            base(message) => Id = id;
        public BusExceptionDO(int id, string message, Exception innerException) :
            base(message, innerException) => Id = id;
        public override string ToString() => base.ToString() + $", have problem with Bus: {Id}";
    }
}
