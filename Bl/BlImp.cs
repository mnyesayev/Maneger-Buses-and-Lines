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
        IDal dal = DalFactory.GetDal();
        public User GetUser(string userName, string password)
        {
            try
            {
                var user = dal.GetUser(userName);
                if (user.Password == password)
                {
                    var newUserBO = new User();
                    Bl.Clone.CopyPropertiesTo(user, newUserBO);
                    return newUserBO;
                }
                else
                    throw new UserException(userName, $"the password: {password} incorrect");
            }
            catch (DO.UserExceptionDO ex)
            {
                throw new UserException(ex.UserName, ex.Message, ex);
            }
        }
    }
}
