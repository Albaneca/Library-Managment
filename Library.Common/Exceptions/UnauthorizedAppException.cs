using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Library.Common.Exceptions
{
    public class UnauthorizedAppException : ApplicationException
    {
        public UnauthorizedAppException() : base() { }

        public UnauthorizedAppException(string message) : base(message) { }

        public UnauthorizedAppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
