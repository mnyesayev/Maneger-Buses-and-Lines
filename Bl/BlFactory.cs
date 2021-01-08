using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bl;

namespace BlApi
{
    public class BlFactory
    {

        public static IBL GetBL(string type)
        {
            switch (type)
            {
                case "1":
                    return new BlImp();
                default:
                    return new BlImp();
            }
        }

    }
}
