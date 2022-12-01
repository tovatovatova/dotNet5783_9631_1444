using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
  public class DalIdDoNotExistException:Exception//if not exist
    {
        public int EntityId;
        public string EntityName;
        public DalIdDoNotExistException(int id, string EName) : base() { EntityId = id; EntityName = EName; }
        public DalIdDoNotExistException(int id,string EName,string message) : base(message) { EntityId = id;EntityName = EName; }
        public DalIdDoNotExistException(int id, string EName, string message,Exception innerException) : base(message, innerException) { EntityId = id; EntityName = EName; }

        public override string ToString() => $"Id:{EntityId} of type {EntityName},does not exist";
    }


    [Serializable]
    public class DalIdAlreadyExistException : Exception//if already exist
    {
        public int EntityId;
        public string EntityName;
        public DalIdAlreadyExistException(int id, string EName) : base() { EntityId = id; EntityName = EName; }
        public DalIdAlreadyExistException(int id, string EName, string message) : base(message) { EntityId = id; EntityName = EName; }
        public DalIdAlreadyExistException(int id, string EName, string message, Exception innerException) : base(message, innerException) { EntityId = id; EntityName = EName; }

        public override string ToString() => $"Id:{EntityId} of type {EntityName},already exists";
    }


}
