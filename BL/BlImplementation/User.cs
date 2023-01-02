using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class User : IUser
    {
        DalApi.IDal dal = DalApi.Factory.Get();
        private delegate BO.User? sc<in T>(T obj);
        public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
        public bool compare(BO.User user)
        {
          var use=from DO.User u in dal.User.GetAll()
                  where (u.UserName==user.UserName)&&(u.Password==user.Password)/*&&(u.Email==user.Email)*/
                  select u;
            if (use==null)//if empty-there was nothing equals to the given user
                return false;
            return true;
        }
        public void addUser(BO.User user)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public void updateUser(BO.User user)
        {
            throw new NotImplementedException();
        }
    }
}
