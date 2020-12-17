using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DS;
using DO;

namespace Dal
{
    sealed class DalObject : IDal
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion

        public void createBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetBuses()
        {
            return from Bus in DataSource.Buses
                   select Bus.Clone();
        }

        public IEnumerable<Bus> GetBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
