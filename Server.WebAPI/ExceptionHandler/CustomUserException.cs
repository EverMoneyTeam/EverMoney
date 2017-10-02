using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.WebApi.ExceptionHandler
{
    public class CustomUserException : Exception
    {
        public CustomUserException()
        { }

        public CustomUserException(string message)
            : base(message)
        { }

        public CustomUserException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
