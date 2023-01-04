
using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

public class DalUser : IUser
{
    /// <summary>
    /// add new user into the system and return his password(?)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Add(User user)
    {
        bool flag=DataSource.UserList.Any(use=>use?.Password==user.Password);
        if (flag)//if this password already exist-cant add this user
            throw new DalIdAlreadyExistException(user.Password, "user");
        //the new user doesnt exist
        DataSource.UserList.Add(user);
        return user.Password;
    }

    public void Delete(int id)//for now-dont delete any user...
    {
        User? adduser = DataSource.UserList.FirstOrDefault(user => user?.Password == id) ?? throw new DalIdDoNotExistException(id, "user");
        int UserIndex = DataSource.UserList.FindIndex(user => user?.Password == id );
        DataSource.UserList.RemoveAt(UserIndex);
    }

    public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.UserList.Where(item => filter(item));
        return DataSource.UserList.Select(item => item);
    }

    public User GetById(int id)
    {
        return DataSource.UserList.FirstOrDefault(user => user?.Password == id) ?? throw new DalIdDoNotExistException(id, "user");

    }

    public void Update(User user)
    {
        User? addUser = DataSource.UserList.FirstOrDefault(user => user?.Password == user?.Password) ?? throw new DalIdDoNotExistException(user.Password, "user");
        int UserIndex = DataSource.UserList.FindIndex(user => user?.Password == user?.Password);
        DataSource.UserList[UserIndex] = user;
    }
}
