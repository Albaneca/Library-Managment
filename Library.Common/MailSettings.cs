using Library.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common
{
    public class MailSettings : IMailSettings
    {
        public string Mail => "devvpurposeemail@gmail.com";
        public string DisplayName => "Library System";
        public string Password => "somepasswordfordevvemail"; // pass for imageKit Poolacar247Poolacar
        public string Host => "smtp.gmail.com";
        public int Port => 587;
    }
}
