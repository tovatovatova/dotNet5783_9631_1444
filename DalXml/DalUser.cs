using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalUser : IUser
{
    readonly string s_users = "users";

    public int Add(User user)
    {
        List<User?> lstUser = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        bool flag = lstUser.Any(use => use?.Password == user.Password && use?.UserName == user.UserName);
        if (flag)//if this password already exist-cant add this user
            throw new DalIdAlreadyExistException(user.Password, "user");
        //the new user doesnt exist
        user.Log = LogIn.Customer;
        lstUser.Add(user);
        XMLTools.SaveListToXMLSerializer<DO.User>(lstUser, s_users);
        return user.Password;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
    {
        List<User?> lstUser = XMLTools.LoadListFromXMLSerializer<DO.User>(s_users);
        if (filter != null)
            return lstUser.Where(item => filter(item));
        return lstUser.Select(item => item);
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(User item)
    {
        throw new NotImplementedException();
    }
}
