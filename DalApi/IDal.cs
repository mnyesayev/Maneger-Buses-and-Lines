using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi
{
    public interface IDal
    {

        #region Bus
        DO.Bus GetBus(int id);
        /// <summary>
        /// return all buses in Data Source
        /// </summary>
        /// <returns>a all buses in Data Source</returns>
        IEnumerable<DO.Bus> GetBuses();
        IEnumerable<DO.Bus> GetBusesBy(Predicate<DO.Bus> predicate);
        void createBus(DO.Bus bus);

        #endregion
        
        
    }
}
