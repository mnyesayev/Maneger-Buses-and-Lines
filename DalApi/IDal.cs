using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        void Create<T>(T t);
        void update<T>(T t);
        void del<T>(T t);
    }
}
