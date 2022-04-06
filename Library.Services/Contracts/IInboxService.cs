using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IInboxService
    {
        Task SendMessageAsync(string sender, string reciever, string message);
        Task<bool> HasUnreadMessages(string userIdOrEmail);
        Task<IEnumerable<InboxDTO>> GetUserMessages(string userId);
    }
}
