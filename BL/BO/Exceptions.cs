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
        public string? Entity;
        public BlIdDoNotExistException(string ent) : base() { Entity = ent; }
        public BlIdDoNotExistException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => base.ToString() + $" does not exist";

    }
    [Serializable]
    public class BlIdAlreadyExistException : Exception//if already exist
    {
        public BlIdAlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
        public BlIdAlreadyExistException(string message) : base(message) { }

        public override string ToString() => base.ToString() ;
    }
    [Serializable]
    public class BlNullPropertyException:Exception
    {
        public string? message;

        public BlNullPropertyException(string mess):base() { message = mess; }
        public BlNullPropertyException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"{message}";
        
    }
    [Serializable]
    public class BlWrongCategoryException:Exception
    {
        public string? message;
        public BlWrongCategoryException(string mess) : base() { message = mess; }
        public BlWrongCategoryException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"{message}";
    }
    [Serializable]
    public class BlIncorrectDateException:Exception
    {
        public string ?message;
        public BlIncorrectDateException(string mess) : base() { message = mess; }
        public BlIncorrectDateException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString() => $"{message}";
    }
    [Serializable]
    public class BlInvalidInputException : Exception
    {
        public string? Entity;
        public BlInvalidInputException(string ent) : base() { Entity = ent; }
        public BlInvalidInputException(string Entity, Exception innerException) : base(Entity, innerException) { }
        public override string ToString() => $"invalid {Entity}";
    }
    [Serializable]
    public class BlOutOfStockException : Exception
    {
        public string? Entity;
        public BlOutOfStockException(string ent) : base() { Entity = ent; }
        public BlOutOfStockException(string Entity, Exception innerException) : base(Entity, innerException) { }
        public override string ToString() => $" {Entity}";
    }
}
