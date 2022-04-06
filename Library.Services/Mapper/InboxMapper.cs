using Library.Data.Models;
using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Mapper
{
    public static class InboxMapper
    {
        public static InboxDTO GetDTO(this Inbox model)
        {
            return new InboxDTO
            {
                Sender = model.FromUserId.ToString(),
                Receiver = model.UserId.ToString(),
                Message = model.Message,
                SendOnDate = model.CreatedOn
            };
        }
    }
}
