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
        readonly IDal dal = DalFactory.GetDal();

        public string GetNameStop(int code)
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


        void IBL.AddUser(User user)
        {
            throw new NotImplementedException();
        }

        Line IBL.ChangeStopLine(int idLine,int codeStop1,int codeStop2,int index1,int index2)
        {
            var st1 = dal.GetStopLine(idLine, codeStop1);
            var st2 = dal.GetStopLine(idLine, codeStop2);
            var l = dal.GetLine(idLine);

            if (l==null|| st1 == null||st2==null)
                return null;
            
            var temp = st1;
            st1.CodeStop = codeStop2;
            st1.NextStop = st2.NextStop;
            st1.PrevStop = st2.PrevStop;
            st1.NumStopInLine = index2;
            st2.CodeStop = codeStop1;
            st2.NumStopInLine = index1;
            st2.PrevStop = temp.PrevStop;
            st2.NextStop = temp.NextStop;
            
            try
            {
                dal.UpdateStopLine(st1);
                dal.UpdateStopLine(st2);
                if (l.CodeFirstStop==codeStop1&&l.CodeLastStop==codeStop2)
                {
                    l.CodeFirstStop = codeStop2;
                    l.CodeLastStop = codeStop1;
                    dal.UpdateLine(l);
                }
                if(l.CodeFirstStop==codeStop1)
                {
                    l.CodeFirstStop = codeStop2;
                    dal.UpdateLine(l);
                }
                if(l.CodeLastStop==codeStop2)
                {
                    l.CodeLastStop = codeStop1;
                    dal.UpdateLine(l);
                }
            }
            catch (DO.StopLineExceptionDO)
            {
                return null;
            }
            catch(DO.LineExceptionDO)
            {
                return null;
            }
            var upLine = new Line()
            {
                IdLine = l.IdLine,
                NumLine = l.NumLine,
                Area = (BO.Areas)l.Area,
                CodeAgency = (BO.Agency)l.CodeAgency,
                StopsInLine = GetStopsInLine(l.IdLine)
            };
            return upLine;
        }

        Line IBL.AddStopLine(int idLine, int codeStop, int index)
        {
            throw new  NotImplementedException();
            var st = dal.GetStopLine(idLine, codeStop);
            var stopsInLine = dal.GetStopLinesBy((StopLine)=> { return StopLine.IdLine == idLine; });
            if (st!=null||stopsInLine==null)
                return null;
            if (stopsInLine.Count() < index + 1)
                return null;
            try
            {
                if (index == 1)
                {
                    var head = stopsInLine.ElementAt(0);
                    head.PrevStop = codeStop;
                    foreach (var item in stopsInLine)
                    {
                        dal.UpdateStopLine(idLine, codeStop, (StopLine) => { StopLine.NumStopInLine++; });
                    }
                    dal.AddStopLine(new DO.StopLine() { NextStop = head.CodeStop, IdLine = idLine, CodeStop = codeStop, NumStopInLine = 1 });
                    dal.UpdateLine(idLine, (Line) => { Line.CodeFirstStop = codeStop; });
                    var l = dal.GetLine(idLine);
                    return new Line() 
                    { Area = (Areas)l.Area, IdLine = l.IdLine,CodeAgency=(Agency)l.CodeAgency
                      ,
                    };
                }

            }
            catch (Exception)
            {

                throw;
            }
            var prev = stopsInLine.ElementAt(index - 1);
        }

        Line IBL.DeleteStopLine(int idLine, int codeStop, int index)
        {
            throw new NotImplementedException();

        }

        void IBL.DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StopLine> GetStopsInLine(int id)
        {
            return from StopLine in dal.GetStopLinesBy((StopLine)
                               => { return StopLine.IdLine == id; })
                            let newStopLine = new BO.StopLine()
                            {
                                CodeStop = StopLine.CodeStop
                                ,IdLine = StopLine.IdLine
                                ,Name = GetNameStop(StopLine.CodeStop)
                                ,NumStopInLine = StopLine.NumStopInLine
                                ,NextStop = StopLine.NextStop
                                ,PrevStop = StopLine.PrevStop
                            }
                            orderby newStopLine.NumStopInLine
                            select newStopLine;
        }

        IEnumerable<Line> IBL.GetLines()
        {
            return from Line in dal.GetLines()
                        let newLine=new BO.Line()
                        { IdLine=Line.IdLine,NumLine=Line.NumLine
                        ,Area=(BO.Areas)Line.Area,CodeAgency=(BO.Agency)Line.CodeAgency
                        ,StopsInLine=GetStopsInLine(Line.IdLine)
                        }
                        select newLine;
        }

        void IBL.InsertDistanceAndTime(int code1, int code2, double distance, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        string IBL.RecoverPassword(string phone, DateTime birthday)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateName(int code, string name)
        {
            throw new NotImplementedException();
        }
    }
}
