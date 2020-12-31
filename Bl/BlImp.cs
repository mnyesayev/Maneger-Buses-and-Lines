﻿using System;
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

        #region BusStop
        public IEnumerable<BusStop> GetBusStops()
        {
            return from BusStop in dal.GetStops()
                   let newBusStop = new BusStop
                   {
                       Address = BusStop.Address,
                       Code = BusStop.Code,
                       Name = BusStop.Name,
                       MoreInfo = BusStop.MoreInfo,
                       LinesPassInStop = GetLinesInStop(BusStop.Code)
                   }
                   select newBusStop;
        }

        public IEnumerable<Line> GetLinesInStop(int code)
        {
            return from Line in GetLines()
                   where Line.StopsInLine.Any((StopLine) => StopLine.CodeStop == code)
                   select Line;
        }
        public string GetNameStop(int code)
        {
            var stop = dal.GetBusStop(code);
            if (stop != null)
                return stop.Name;
            else
                return null;
        }

        BusStop IBL.UpdateName(int code, string name)
        {
            var st = dal.GetBusStop(code);
            if (st == null) return null;
            dal.UpdateBusStop(code, (BusStop) => { BusStop.Name = name; });
            st = dal.GetBusStop(code);
            var busStop = new BusStop();
            return (BusStop)Bl.Cloning.CopyPropertiesToNew(st, busStop.GetType());
        }
        #endregion

        #region user
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
        public string RecoverPassword(string phone, DateTime birthday)
        {
            var user = dal.GetUser(phone, birthday);
            if (user == null)
                throw new PasswordRecoveryException(phone);
            return user.Password;
        }

        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region StopLine
        public Line ChangeStopLine(int idLine, int codeStop1, int codeStop2, int index1, int index2)
        {
            var st1 = dal.GetStopLine(idLine, codeStop1);
            var st2 = dal.GetStopLine(idLine, codeStop2);
            var l = dal.GetLine(idLine);

            if (l == null || st1 == null || st2 == null)
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
                if (l.CodeFirstStop == codeStop1 && l.CodeLastStop == codeStop2)
                {
                    l.CodeFirstStop = codeStop2;
                    l.CodeLastStop = codeStop1;
                    dal.UpdateLine(l);
                }
                if (l.CodeFirstStop == codeStop1)
                {
                    l.CodeFirstStop = codeStop2;
                    dal.UpdateLine(l);
                }
                if (l.CodeLastStop == codeStop2)
                {
                    l.CodeLastStop = codeStop1;
                    dal.UpdateLine(l);
                }
            }
            catch (DO.StopLineExceptionDO)
            {
                return null;
            }
            catch (DO.LineExceptionDO)
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

        public Line AddStopLine(int idLine, int codeStop, int index)
        {
            var st = dal.GetStopLine(idLine, codeStop);
            var stopsInLine = dal.GetStopLinesBy((StopLine) => { return StopLine.IdLine == idLine; });
            if (st != null || stopsInLine == null)
                return null;
            if (stopsInLine.Count() < index + 1)
                return null;
            var l = dal.GetLine(idLine);
            try
            {
                if (index == 1)
                {
                    var head = stopsInLine.First();
                    head.PrevStop = codeStop;
                    head.NumStopInLine++;
                    dal.UpdateStopLine(head);
                    foreach (var item in stopsInLine)
                    {
                        if (item.CodeStop == head.CodeStop)
                            continue;
                        dal.UpdateStopLine(item.IdLine, item.CodeStop, (StopLine) => { StopLine.NumStopInLine++; });
                    }
                    dal.AddStopLine(new DO.StopLine() { NextStop = head.CodeStop, IdLine = idLine, CodeStop = codeStop, NumStopInLine = 1 });
                    dal.UpdateLine(idLine, (Line) => { Line.CodeFirstStop = codeStop; });
                }
                if (index != 1)
                {
                    if (index == stopsInLine.Count())//add to end of route
                    {
                        var tail = stopsInLine.Last();
                        tail.NextStop = codeStop;
                        dal.UpdateStopLine(tail);
                        dal.AddStopLine(new DO.StopLine()
                        {
                            PrevStop = tail.CodeStop,
                            IdLine = idLine,
                            CodeStop = codeStop,
                            NumStopInLine = index
                        });
                        dal.UpdateLine(idLine, (Line) => { Line.CodeLastStop = codeStop; });
                        return new Line()
                        {
                            Area = (Areas)l.Area,
                            IdLine = l.IdLine,
                            CodeAgency = (Agency)l.CodeAgency,
                            MoreInfo = l.MoreInfo,
                            NumLine = l.NumLine,
                            StopsInLine = GetStopsInLine(idLine)
                        };
                    }
                    var prev = stopsInLine.ElementAt(index - 1);
                    dal.AddStopLine(new DO.StopLine()
                    {
                        PrevStop = prev.CodeStop,
                        IdLine = idLine,
                        CodeStop = codeStop,
                        NumStopInLine = index,
                        NextStop = prev.NextStop
                    });
                    prev.NextStop = codeStop;
                    dal.UpdateStopLine(prev);
                    for (int i = index; i < stopsInLine.Count(); i++)
                    {
                        var t = stopsInLine.ElementAt(i);
                        if (i == index)
                        {
                            dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                            {
                                StopLine.NumStopInLine++;
                                StopLine.PrevStop = codeStop;
                            });
                            continue;
                        }
                        dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                        {
                            StopLine.NumStopInLine++;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return new Line()
            {
                Area = (Areas)l.Area,
                IdLine = l.IdLine,
                CodeAgency = (Agency)l.CodeAgency,
                MoreInfo = l.MoreInfo,
                NumLine = l.NumLine,
                StopsInLine = GetStopsInLine(idLine)
            };
        }

        public Line DeleteStopLine(int idLine, int codeStop, int index)
        {
            var st = dal.GetStopLine(idLine, codeStop);
            var stopsInLine = dal.GetStopLinesBy((StopLine) =>
            { return StopLine.IdLine == idLine; });
            if (st == null || stopsInLine == null)
                return null;
            var l = dal.GetLine(idLine);
            if (stopsInLine.Count() == 2)
                return null;
            try
            {
                if (index == 1)
                {
                    dal.UpdateLine(idLine, (Line) =>
                    { Line.CodeFirstStop = stopsInLine.ElementAt(1).CodeStop; });
                    dal.DeleteStopLine(idLine, codeStop);
                    for (int i = 1; i < stopsInLine.Count(); i++)
                    {
                        var t = stopsInLine.ElementAt(i);
                        if (i == 1)
                        {
                            dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                            {
                                StopLine.NumStopInLine--;
                                StopLine.PrevStop = 0;
                            });
                            continue;
                        }
                        dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                        {
                            StopLine.NumStopInLine--;
                        });
                    }

                }
                if (index == stopsInLine.Count())
                {
                    dal.UpdateLine(idLine, (Line) =>
                    { Line.CodeFirstStop = stopsInLine.ElementAt(stopsInLine.Count() - 2).CodeStop; });
                    dal.UpdateStopLine(idLine, stopsInLine.ElementAt(stopsInLine.Count() - 2).CodeStop,
                        (StopLine) => { st.NextStop = 0; });
                    dal.DeleteStopLine(idLine, codeStop);
                }
                if (index != 1 && index != stopsInLine.Count())
                {
                    var prev = stopsInLine.ElementAt(index - 2);
                    prev.NextStop = st.NextStop;
                    dal.DeleteStopLine(idLine, codeStop);
                    for (int i = index + 1; i < stopsInLine.Count(); i++)
                    {
                        var t = stopsInLine.ElementAt(i);
                        if (i == index + 1)
                        {
                            dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                            {
                                StopLine.NumStopInLine--;
                                StopLine.PrevStop = prev.CodeStop;
                            });
                            continue;
                        }
                        dal.UpdateStopLine(t.IdLine, t.CodeStop, (StopLine) =>
                        {
                            StopLine.NumStopInLine--;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return new Line()
            {
                Area = (Areas)l.Area,
                IdLine = l.IdLine,
                CodeAgency = (Agency)l.CodeAgency,
                MoreInfo = l.MoreInfo,
                NumLine = l.NumLine,
                StopsInLine = GetStopsInLine(idLine)
            };
        }

        public IEnumerable<StopLine> GetStopsInLine(int id)
        {
            return from StopLine in dal.GetStopLinesBy((StopLine) =>
            { return StopLine.IdLine == id; })
                   let newStopLine = new BO.StopLine()
                   {
                       CodeStop = StopLine.CodeStop,
                       IdLine = StopLine.IdLine,
                       Name = GetNameStop(StopLine.CodeStop),
                       NumStopInLine = StopLine.NumStopInLine,
                       NextStop = StopLine.NextStop,
                       PrevStop = StopLine.PrevStop
                   }
                   orderby newStopLine.NumStopInLine
                   select newStopLine;
        }

        #endregion

        public IEnumerable<Line> GetLines()
        {
            return from Line in dal.GetLines()
                   let newLine = new Line()
                   {
                       IdLine = Line.IdLine,
                       NumLine = Line.NumLine,
                       Area = (BO.Areas)Line.Area,
                       CodeAgency = (BO.Agency)Line.CodeAgency,
                       StopsInLine = GetStopsInLine(Line.IdLine),
                       MoreInfo = Line.MoreInfo
                   }
                   select newLine;
        }

        public void InsertDistanceAndTime(int code1, int code2, double distance, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        #region Bus
        public IEnumerable<Bus> GetBuses()
        {
            return from Bus in dal.GetBuses()
                   let newBus = new Bus
                   {
                       DateRoadAscent = Bus.DateRoadAscent,
                       Fuel = Bus.Fuel,
                       Id = Bus.Id,
                       State = (States)Bus.State,
                       LastCare = Bus.LastCare,
                       LastCareMileage = Bus.LastCareMileage,
                       Mileage = Bus.Mileage
                   }
                   orderby newBus.Id
                   select newBus;
        }

        public Bus Care()
        {
            throw new NotImplementedException();
        }

        public Bus Fuel()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
