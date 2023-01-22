using BlApi;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (user.UserName==null) throw new BO.BlInvalidInputException("user name");
          IEnumerable<DO.User> use=from DO.User u in dal.User.GetAll()
                  where (u.UserName==user.UserName)&&(u.Password==user.Password)
                  select u;
            if (use.Count()==0)//if empty-there was nothing equals to the given user
                throw new BO.BlIdDoNotExistException("user doesnt exist");//user doesnt exist
            user.Log = (BO.LogIn)(use.First(u => (u.UserName == user.UserName) && (u.Password == user.Password)).Log);
           //exists
        }
        public void addUser(BO.User user)
        {

            if (user.UserName == null||user.UserName=="") 
                throw new BO.BlInvalidInputException("user name");
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (!re.IsMatch(user.Email) || user.Email == null)
                throw new BO.BlInvalidInputException("user email");
            if (user?.Password == null)
                throw new BO.BlInvalidInputException("user password");
            try
            {
                dal.User.Add(new DO.User() { UserName=user.UserName,Password=user.Password, Email=user.Email});
            }//func return password...
            catch (DO.DalIdAlreadyExistException ex)
            {
                throw new BO.BlIdAlreadyExistException(" this user already exists", ex);
            }
        }

        public BO.User? GetUser(string name, int password)
        {
            if( (name== null)||(password<=0) )throw new BO.BlInvalidInputException("user name");
            IEnumerable<DO.User> users = from DO.User u in dal.User.GetAll()
            where (u.UserName ==name) && (u.Password == password)
                                       select u;
            if (users.Count() == 0)//if empty-there was nothing equals to the given user
                throw new BO.BlIdDoNotExistException("user doesnt exist");//user doesnt exist
            BO.User result = new BO.User();
            result.UserName = users.ToArray()[0].UserName;  
            result.Password = users.ToArray()[0].Password;
            result.Email = users.ToArray()[0].Email;
            result.Log= (BO.LogIn)users.ToArray()[0].Log;
            return result;
            //exists
        }
    }
}
