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
    public BO.User? GetUser(string name,int password);

    public void compare(BO.User user);


}
