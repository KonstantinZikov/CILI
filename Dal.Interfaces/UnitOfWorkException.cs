using System;

namespace Dal.Interfaces
{
    public class UnitOfWorkException : Exception
    {
        public UnitOfWorkException() { }
        public UnitOfWorkException(string message) : base(message) { }
        public UnitOfWorkException(string message, Exception innerException) : base(message, innerException) { }
    }
}
