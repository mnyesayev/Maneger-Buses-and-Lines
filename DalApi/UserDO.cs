using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Authorizations
    {
        User, PremiumUser, Admin, MainAdmin, IT
    }
    public class UserDO
    {
        string userName;
        string password;
        Authorizations authorization;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public Authorizations Authorization { get => authorization; set => authorization = value; }

        public UserDO(string userName, string password, Authorizations authorization = Authorizations.User)
        {
            UserName = userName;
            Password = password;
            Authorization = authorization;
        }
    }
}
