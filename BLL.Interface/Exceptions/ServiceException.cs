using System;

namespace BLL.Interface.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException() { }
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
