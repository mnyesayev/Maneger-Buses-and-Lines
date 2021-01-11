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

    [Serializable]
    public class DriverExceptionDO : Exception
    {
        public int Id;
        public DriverExceptionDO(int id) : base() => Id = id;
        public DriverExceptionDO(int id, string message) :
            base(message) => Id = id;
        public DriverExceptionDO(int id, string message, Exception innerException) :
            base(message, innerException) => Id = id;
        public override string ToString() => base.ToString() + $", have problem with Driver: {Id}";
    }

    [Serializable]
    public class BusStopExceptionDO : Exception
    {
        public int Code;
        public BusStopExceptionDO(int code) : base() => Code = code;
        public BusStopExceptionDO(int code, string message) :
            base(message) => Code = code;
        public BusStopExceptionDO(int code, string message, Exception innerException) :
            base(message, innerException) => Code = code;
        public override string ToString() => base.ToString() + $", have problem with BusStop: {Code}";
    }

    [Serializable]
    public class LineExceptionDO : Exception
    {
        public int IdLine;
        public LineExceptionDO(int idLine) : base() => IdLine = idLine;
        public LineExceptionDO(int idLine, string message) :
            base(message) => IdLine = idLine;
        public LineExceptionDO(int idLine, string message, Exception innerException) :
            base(message, innerException) => IdLine = idLine;
        public override string ToString() => base.ToString() + $", have problem with Line: {IdLine}";
    }

    [Serializable]
    public class StopLineExceptionDO : Exception
    {
        public int IdLine;
        public int Code;
        public StopLineExceptionDO(int idLine, int code) : base() { IdLine = idLine; Code = code; }
        public StopLineExceptionDO(int idLine, int code,string message) :
              base(message) { IdLine = idLine; Code = code; }
        public StopLineExceptionDO(int idLine, int code,string message, Exception innerException) :
              base(message,innerException){ IdLine = idLine; Code = code; }
        public override string ToString() => base.ToString() + $", have problem with stopLine:{Code} in {IdLine} line ";
    }

    [Serializable]
    public class ConsecutiveStopsExceptionDO : Exception
    {
        public int Code1;
        public int Code2;
        public ConsecutiveStopsExceptionDO(int code1, int code2) : base() { Code1 = code1; Code2 = code2; }
        public ConsecutiveStopsExceptionDO(int code1, int code2, string message) :
              base(message)
        { Code1 = code1; Code2 = code2; }
        public ConsecutiveStopsExceptionDO(int code1, int code2, string message, Exception innerException) :
              base(message, innerException)
        { Code1 = code1; Code2 = code2; }
        public override string ToString() => base.ToString() + $", have problem with ConsecutiveStops: {Code1},{Code2}";
    }

    [Serializable]
    public class LineTripExceptionDO : Exception
    {
        public int IdLine;
        public LineTripExceptionDO(int idLine) : base() => IdLine = idLine;
        public LineTripExceptionDO(int idLine, string message) :
            base(message) => IdLine = idLine;
        public LineTripExceptionDO(int idLine, string message, Exception innerException) :
            base(message, innerException) => IdLine = idLine;
        public override string ToString() => base.ToString() + $", have problem with LineTrip: {IdLine}";
    }
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
}
