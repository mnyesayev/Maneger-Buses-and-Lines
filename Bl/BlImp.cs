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

        #region BusStop
        public IEnumerable<BusStop> GetBusStops()
        {
            return from BusStop in dal.GetStops().AsParallel()
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

        public Line AddStopLine(int idLine, int codeStop, StopLine stopLine, int index)
        {
            var st = dal.GetStopLine(idLine, codeStop);
            var stopsInLine = dal.GetStopLinesBy((StopLine) => { return StopLine.IdLine == idLine; });
            if (st != null || stopsInLine == null)
                return null;
            try
            {
                if (index != 1)
                {
                    var d1 = GetDistance(stopLine.PrevStop, codeStop);
                }
            }
            catch (ConsecutiveStopsException ex)
            {
                throw new ConsecutiveStopsException(stopLine.PrevStop, codeStop, "No time and distance available", ex);
            }
            try
            {
                var d2 = GetDistance(codeStop, stopLine.CodeStop);
            }
            catch (ConsecutiveStopsException ex)
            {
                throw new ConsecutiveStopsException(codeStop, stopLine.CodeStop, "No time and distance available", ex);
            }

            var l = dal.GetLine(idLine);
            try
            {
                //add to head of route
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
                        dal.UpdateStopLine(item.IdLine, item.CodeStop, (StopLine) => StopLine.NumStopInLine++);
                    }
                    dal.AddStopLine(new DO.StopLine() { NextStop = head.CodeStop, IdLine = idLine, CodeStop = codeStop, NumStopInLine = 1 });
                    dal.UpdateLine(idLine, (Line) => Line.CodeFirstStop = codeStop);
                }
                if (index != 1)
                {
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
                    if (index - 1 == stopsInLine.Count())
                    {
                        dal.UpdateLine(idLine, (Line) => Line.CodeLastStop = codeStop);
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
            if (stopsInLine.Count() == 2)
                return null;
            try
            {
                if (index != 1)
                {
                    var d1 = GetDistance(st.PrevStop, st.NextStop);
                }
            }
            catch (ConsecutiveStopsException ex)
            {
                throw new ConsecutiveStopsException(st.PrevStop, st.CodeStop, "No time and distance available", ex);
            }
            var l = dal.GetLine(idLine);
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
                        (StopLine) =>  st.NextStop = 0);
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
                       PrevStop = StopLine.PrevStop,
                       AvregeDriveTimeToNext = GetTime(StopLine.CodeStop, StopLine.NextStop),
                       DistanceToNext = GetDistance(StopLine.CodeStop, StopLine.NextStop)
                   }
                   orderby newStopLine.NumStopInLine
                   select newStopLine;
        }
        public double GetDistance(int code1, int code2)
        {
            if (code2 == 0) return default;
            var cs = dal.GetConsecutiveStops(code1, code2);
            if (cs == null)
            {
                throw new ConsecutiveStopsException(code1, code2, "No information is available on these pair of stations");
            }
            return cs.Distance;
        }

        public TimeSpan GetTime(int code1, int code2)
        {
            if (code2 == 0) return default;
            var cs = dal.GetConsecutiveStops(code1, code2);
            if (cs == null)
            {
                throw new ConsecutiveStopsException(code1, code2, "No information is available on these pair of stations");
            }
            return cs.AvregeDriveTime;
        }
        #endregion

        #region Line
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
        public Line AddLine(string numLine, Areas area, IEnumerable<StopLine> stops, string moreInfo)
        {
            int idLine = dal.CreateLine(new DO.Line()
            {
                NumLine = numLine,
                Active = true,
                Area = (DO.Areas)area,
                CodeFirstStop = stops.First().CodeStop,
                CodeLastStop = stops.Last().CodeStop,
                MoreInfo = moreInfo
            });
            var stopsLine = from StopLine in stops
                            let sL = new DO.StopLine()
                            {
                                IdLine = idLine,
                                PrevStop = (StopLine.NumStopInLine == 1) ? 0 : stops.ElementAt(StopLine.NumStopInLine - 2).CodeStop,
                                NextStop = (StopLine.NumStopInLine == stops.Count()) ? 0 : stops.ElementAt(StopLine.NumStopInLine).CodeStop,
                                CodeStop = StopLine.CodeStop,
                                NumStopInLine = StopLine.NumStopInLine
                            }
                            select sL;
            dal.AddRouteStops(stopsLine);
            return new Line()
            {
                IdLine = idLine,
                NumLine = numLine,
                Area = area,
                StopsInLine = GetStopsInLine(idLine),
                MoreInfo = moreInfo
            };
        }
        public bool UpdateLine(int idLine, string numLine, Areas area, Agency agency)
        {
            var l = dal.GetLine(idLine);
            if (l == null) return false;
            try
            {
                dal.UpdateLine(idLine, (Line) =>
                {
                    Line.NumLine = numLine;
                    Line.Area = (DO.Areas)area;
                    Line.CodeAgency = (DO.Agency)agency;
                });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool DeleteLine(int idLine)
        {
            var l = dal.GetLine(idLine);
            if (l == null) return false;
            try
            {
                dal.DeleteLine(idLine);
                dal.DeleteAllStopsInLine(idLine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public void InsertDistanceAndTime(int code1, int code2, double distance, TimeSpan time)
        {
            var cs= dal.GetConsecutiveStops(code1, code2);
            if(cs!=null)
            {
                dal.UpdateConsecutiveStops(code1, code2, (cstops)=> { cstops.Distance = distance; cstops.AvregeDriveTime = time; });
                return;
            }
            if(cs==null)
            {
                dal.AddConsecutiveStops(new DO.ConsecutiveStops()
                {
                    CodeBusStop1=code1,
                    CodeBusStop2=code2,
                    AvregeDriveTime=time,
                    Distance=distance
                });
            }
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

        public bool CheckFuel(int id, int distance)
        {
            var b = dal.GetBus(id);
            if (b == null)
                return false;//should be "throw"
            if (b.Fuel - distance > 0)
                return false;
            return true;
        }

        public bool CheckCare(int id, int distance)
        {
            var b = dal.GetBus(id);
            if (b == null)
                return false;//should be "throw"
            if (DateTime.Compare(b.LastCare, DateTime.Now.AddYears(-1)) <= 0)
                return true;
            if (b.Mileage + distance - b.LastCareMileage >= 20000)
                return true;
            return false;
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

        #region Driver
        public IEnumerable<Driver> GetDrivers()
        {
            return from Driver in dal.GetDrivers()
                   let newDriver = new Driver()
                   {
                       Id = Driver.Id,
                       Name = Driver.Name,
                       Seniority = Driver.Seniority
                   }
                   orderby newDriver.Name
                   select newDriver;
        }



        #endregion
    }
}
