﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class DeleteException : Exception
    {
        public string NameClass;
        public string Id;
        public DeleteException(string nameClass, string id) : base() 
        { NameClass = nameClass; Id = id; }
        public DeleteException(string nameClass, string id, string message) :
            base(message)
        { NameClass = nameClass;Id = id; }
        public DeleteException(string nameClass, string id, string message, Exception innerException) :
            base(message, innerException)
        { NameClass = nameClass; Id = id; }
        public override string ToString() => base.ToString() + $", have problem with Delete {NameClass}";
    }
    [Serializable]
    public class IdException : Exception
    {
        public string NameClass;
        public string Id;
        public IdException(string nameClass, string id) : base()
        { NameClass = nameClass; Id = id; }
        public IdException(string nameClass, string id, string message) :
            base(message)
        { NameClass = nameClass; Id = id; }
        public IdException(string nameClass, string id, string message, Exception innerException) :
            base(message, innerException)
        { NameClass = nameClass; Id = id; }
        public override string ToString() => base.ToString() + $", have problem with Identification of {NameClass}";
    }
    [Serializable]
    public class AddException : Exception
    {
        public string NameClass;
        public string Id;
        public AddException(string nameClass, string id) : base() { NameClass = nameClass; Id = id; }
        public AddException(string nameClass, string id, string message) :
            base(message)
        { NameClass = nameClass; Id = id; }
        public AddException(string nameClass, string id, string message, Exception innerException) :
            base(message, innerException)
        { NameClass = nameClass; Id = id; }
        public override string ToString() => base.ToString() + $", have problem with Add {NameClass}";
    }
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
    [Serializable]
    public class ConsecutiveStopsException : Exception
    {
        public int Code1;
        public int Code2;
        public ConsecutiveStopsException(int code1, int code2) : base() { Code1 = code1; Code2 = code2; }
        public ConsecutiveStopsException(int code1, int code2, string message) :
              base(message)
        { Code1 = code1; Code2 = code2; }
        public ConsecutiveStopsException(int code1, int code2, string message, Exception innerException) :
              base(message, innerException)
        { Code1 = code1; Code2 = code2; }
        public override string ToString() => base.ToString() + $", have problem with ConsecutiveStops: {Code1},{Code2}";
    }
}
