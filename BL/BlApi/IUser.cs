using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IUser
{
    public void addUser(BO.User user);
    public void deleteUser(int id);
    public void updateUser(BO.User user);
    public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null);
    public IEnumerable<BO.User?> GetAllUsers();
    public void compare(BO.User user);


}
