using System;

namespace BLL.Interface.Exceptions
{
    public class UserException: Exception
    {
        public UserException(string message) : base(message) { }
    }
}
