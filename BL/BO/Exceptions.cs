using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BlIdDoNotExistException : Exception//if not exist
    {
       public BlIdDoNotExistException(int id, string EName, string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => base.ToString() + $"Entity does not exist";
    }
    [Serializable]
    public class BlIdAlreadyExistException : Exception//if already exist
    {
        public BlIdAlreadyExistException(int id, string EName, string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => base.ToString() + $"Entity already exist";
    }
    [Serializable]
    public class BlNullPropertyException:Exception
    {
        string message;

        public BlNullPropertyException(string mess):base(mess) { }
        public BlNullPropertyException(string message, Exception innerException) : base(message, innerException) { }
      //  public override string ToString() => $"";
        
    }
    [Serializable]
    public class BlWrongCategoryException:Exception
    {
        string message;
        public BlWrongCategoryException(string mess) : base(mess) { }
        public BlWrongCategoryException(string message, Exception innerException) : base(message, innerException) { }
        //  public override string ToString() => $"";
    }
    [Serializable]
    public class BlIncorrectDateException:Exception
    {
        string message;
        public BlIncorrectDateException(string mess) : base(mess) { }
        public BlIncorrectDateException(string message, Exception innerException) : base(message, innerException) { }
        //  public override string ToString() => $"";
    }
    public class BlInvalidInputException : Exception
    {
        public string Entity;
        public BlInvalidInputException(string mess) : base(mess) { }
        public BlInvalidInputException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"invalid {Entity}";
    }
}
