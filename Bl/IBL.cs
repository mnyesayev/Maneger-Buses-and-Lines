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
        void addUser(User user);
        string recoverPassword(string phone, DateTime birthday);
        void deleteUser(string userName);
        #endregion

        void insertDistanceAndTime(int code1,int code2,double distance,TimeSpan time);
        #region Line
        IEnumerable<Line> GetLines();
        IEnumerable<StopLine> GetStopsInLine(int id);
        #endregion
        bool changeStopLine(int idLine, int codeStop, int index);
        bool addStopLine(int idLine ,int codeStop,int index);
        bool deleteStopLine(int idLine ,int codeStop,int index);
        
        void updateName(int code,string name);

        #region BusStop
        string getName(int code);

        #endregion
    }
}
