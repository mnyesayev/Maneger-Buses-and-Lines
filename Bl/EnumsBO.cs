using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum States
    {
        ready, drive, refueling, care, mustCare, mustRefuel
    }
    public enum Authorizations
    {
        User, PremiumUser, Admin, MainAdmin, IT
    }
}
