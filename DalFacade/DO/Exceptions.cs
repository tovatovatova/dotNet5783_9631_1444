using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
  public class DalDoNotExistException:Exception
    {
        public DalDoNotExistException(string? message) : base(message) { }
    }
    public class DalAlreadyExistsException:Exception
    {
        public DalAlreadyExistsException(string? message) : base(message) { }
    }
    

}
