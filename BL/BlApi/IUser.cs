using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

internal interface IUser
{
    public void addUser(BO.User user);
    public void deleteUser(int id);
    public void updateUser(BO.User user);
    public IEnumerable<BO.User?> GetListedListByFilter(Func<BO.User?, bool>? filter = null);


}
