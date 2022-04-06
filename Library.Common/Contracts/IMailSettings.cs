using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Contracts
{
    public interface IMailSettings
    {
        string DisplayName { get; }
        string Host { get; }
        string Mail { get; }
        string Password { get; }
        int Port { get; }
    }
}
