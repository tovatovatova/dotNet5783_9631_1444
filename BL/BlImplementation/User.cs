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
        public void compare(BO.User user)
        {
          IEnumerable<DO.User> use=from DO.User u in dal.User.GetAll()
                  where (u.UserName==user.UserName)&&(u.Password==user.Password)/*&&((BO.LogIn)(u.Log)==user.Log)*//*&&(u.Email==user.Email)*/
                  select u;
            if (use.Count()==0)//if empty-there was nothing equals to the given user
                throw new BO.BlIdDoNotExistException("user doesnt exist");//user doesnt exist
            user.Log = (BO.LogIn)(use.First(u => (u.UserName == user.UserName) && (u.Password == user.Password)).Log);
           //exists
        }
        public void addUser(BO.User user)
        {
            if (user.UserName==" ")
                throw new BO.BlInvalidInputException("user name");
            try
            {
                dal.User.Add(new DO.User() { UserName=user.UserName,Password=user.Password, Email=user.Email});
            }//func return password...
            catch (DO.DalIdAlreadyExistException ex)
            {
                throw new BO.BlIdAlreadyExistException(" this user already exists", ex);
            }
        }

        public void deleteUser(int id)
        {
            try
            {
                dal.User.Delete(id);//try to delete user with this id-password
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("this user doesnt exist", ex);
            }
        }

        public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null)
        {
            return from BO.User u in GetAllUsers()
                   where filter(u)
                   select u;
        }

        public void updateUser(BO.User user)
        {
            if (user.UserName == " ")
                throw new BO.BlInvalidInputException("user name");
            
            try
            {
                dal.User.Update(new DO.User() { UserName = user.UserName, Password = user.Password, Log = (DO.LogIn)(user.Log),Email=user.Email });
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("user doesnt exist", ex);
            }
            
        }
        public IEnumerable<BO.User?> GetAllUsers()
        {
            return dal.User.GetAll().Select(user => new BO.User()
            {
                UserName = user?.UserName ?? throw new BO.BlNullPropertyException("missing user name"),
                Password = user?.Password ?? throw new BO.BlNullPropertyException("missing password"),
                Log = (BO.LogIn)(user?.Log)
            });
        }
    }
}
