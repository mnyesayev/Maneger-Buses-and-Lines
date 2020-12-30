using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IBL
    {
        #region User
        User GetUser(string userName, string password);
        void AddUser(User user);
        string RecoverPassword(string phone, DateTime birthday);
        void DeleteUser(string userName);
        #endregion

        void InsertDistanceAndTime(int code1,int code2,double distance,TimeSpan time);
        #region Line
        IEnumerable<Line> GetLines();
        IEnumerable<StopLine> GetStopsInLine(int id);
        #endregion
        #region StopLine
        Line ChangeStopLine(int idLine, int codeStop1, int codeStop2, int index1, int index2);
        Line AddStopLine(int idLine ,int codeStop,int index);
        Line DeleteStopLine(int idLine ,int codeStop,int index);
        #endregion
        #region BusStop
        string GetNameStop(int code);
        void UpdateName(int code, string name);
        #endregion
    }
}
