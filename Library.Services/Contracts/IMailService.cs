using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailDTO mailRequest);
    }
}
