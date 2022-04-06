using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class InboxService : IInboxService
    {
        private readonly LibraryDbContext _db;
        private readonly IUserService _ap;

        public InboxService(LibraryDbContext db, IUserService ap)
        {
            _db = db;
            _ap = ap;
        }

        public async Task SendMessageAsync(string sender, string reciever, string message)
        {
            var senderUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == sender || x.Id.ToString() == sender);
            var recieverUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == reciever || x.Id.ToString() == reciever);

            if (senderUser != null && recieverUser != null)
            {
                await _db.Inboxes.AddAsync(new Inbox { FromUserId = senderUser.Id, UserId = recieverUser.Id, Message = message });
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InboxDTO>> GetUserMessages(string userIdOrEmail)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == userIdOrEmail || x.Id.ToString() == userIdOrEmail);
            var messages = await _db.Inboxes.Where(x => x.UserId == user.Id).Select(x => x.GetDTO()).ToListAsync();
            await _db.Inboxes.Where(x => x.UserId == user.Id).ForEachAsync(x => x.Seen = true);
            await _db.SaveChangesAsync();

            for (int i = 0; i < messages.Count; i++)
            {
                var author = await _ap.GetUserByEmailOrIdAsync(messages[0].Sender);
                messages[i].Sender = $"{author.FirstName} {author.LastName}";
            }
            return messages.OrderByDescending(x => x.SendOnDate);
        }

        public async Task<bool> HasUnreadMessages(string userIdOrEmail)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == userIdOrEmail || x.Id.ToString() == userIdOrEmail);
            return await _db.Inboxes.AnyAsync(x => x.UserId == user.Id && x.Seen == false);
        }
    }
}
