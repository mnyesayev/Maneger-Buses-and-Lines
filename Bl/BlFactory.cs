using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bl;

namespace BlApi
{
    public static class BlFactory
    {

        public static IBL GetBL()
        {
            IBL bl = BlImp.Instance;
            return bl;
        }

    }
}
