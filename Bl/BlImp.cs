using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;

namespace BlApi
{
    public class BlImp : IBL
    {
        IDal dal = DalFactory.GetDal();

        public string getName(int code)
        {
            var stop = dal.GetBusStop(code);
            if (stop != null)
                return stop.Name;
            else
                return null;
        }

        public User GetUser(string userName, string password)
        {
            var user = dal.GetUser(userName);
            if (user != null && user.Password == password)
            {
                var newUserBO = new User();
                Bl.Cloning.CopyPropertiesTo(user, newUserBO);
                return newUserBO;
            }
            return null;
        }

        bool IBL.addStopLine(int idLine, int codeStop, int index)
        {
            throw new NotImplementedException();
        }

        void IBL.addUser(User user)
        {
            throw new NotImplementedException();
        }

        bool IBL.changeStopLine(int idLine, int codeStop, int index)
        {
            throw new NotImplementedException();
        }

        bool IBL.deleteStopLine(int idLine, int codeStop, int index)
        {
            throw new NotImplementedException();
        }

        void IBL.deleteUser(string userName)
        {
            throw new NotImplementedException();
        }

       

        IEnumerable<Line> IBL.GetLines()
        {
            var lines = from Line in dal.GetLines()
                        let newLine=new BO.Line()
                        { IdLine=Line.IdLine,NumLine=Line.NumLine
                        ,Area=(BO.Areas)Line.Area,CodeAgency=(BO.Agency)Line.CodeAgency
                        }
                        select newLine;

            for (int i = 0; i < lines.Count(); i++)
            {
                var stopLines = from StopLine in dal.GetStopLinesBy((StopLine)
                               => { return StopLine.IdLine == lines.ElementAt(i).IdLine; })
                                let newStopLine = new BO.StopLine()
                                {
                                    CodeStop = StopLine.CodeStop
                                    ,
                                    IdLine = StopLine.IdLine
                                    ,
                                    Name = getName(StopLine.CodeStop)
                                    ,
                                    NumStopInLine = StopLine.NumStopInLine
                                    ,
                                    NextStop = StopLine.NextStop
                                    ,
                                    PrevStop = StopLine.PrevStop
                                }
                                select newStopLine;
                Bl.Cloning.CopyPropertiesTo(stopLines, lines.ElementAt(i).StopsInLine);
 
            } 
            return lines;
        }

        void IBL.insertDistanceAndTime(int code1, int code2, double distance, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        string IBL.recoverPassword(string phone, DateTime birthday)
        {
            throw new NotImplementedException();
        }

        void IBL.updateName(int code, string name)
        {
            throw new NotImplementedException();
        }
    }
}
