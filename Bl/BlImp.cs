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
        public User GetUser(string userName, string password)
        {
            var user = dal.GetUser(userName);
            if (user != null && user.Password == password)
            {
                var newUserBO = new User();
                Bl.Clone.CopyPropertiesTo(user, newUserBO);
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

        bool IBL.addStopLine(int idLine, int codeStop, int index)
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

            foreach (var line in lines)
            {
                var stopLines = from StopLine in dal.GetStopLinesBy((StopLine)
                                =>{ return StopLine.IdLine == line.IdLine; })
                                let newStopLine=new BO.StopLine()
                                {
                                    CodeStop=StopLine.CodeStop
                                    ,IdLine=StopLine.IdLine
                                    ,NumStopInLine=StopLine.NumStopInLine
                                    ,NextStop=StopLine.NextStop
                                    ,PrevStop=StopLine.PrevStop
                                }
                                select newStopLine;
                line.StopsInLine = stopLines;
            }
            return lines;
        }

        User IBL.GetUser(string userName, string password)
        {
            throw new NotImplementedException();
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
