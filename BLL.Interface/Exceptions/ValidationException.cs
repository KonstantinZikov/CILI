using System;

namespace BLL.Interface.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
