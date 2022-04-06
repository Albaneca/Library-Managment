using Library.Common;
using Library.Common.Contracts;
using Library.Services.Contracts;
using Library.Services.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class MailService : IMailService
    {
        private readonly IMailSettings _mailSettings;
        public MailService(IMailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(MailDTO mailRequest)
        {
            mailRequest.EmailFrom = _mailSettings.Mail;

            var builder = new BodyBuilder();
            string reciver = null;
            if (mailRequest.IsBan)
            {
                mailRequest.Subject = "[Rideshare] You have been banned!";
                reciver = mailRequest.Reciever;
                builder.TextBody = mailRequest.Message;
            }
            else if (mailRequest.isFromContact)
            {
                reciver = mailRequest.EmailFrom;
                builder.TextBody = $"From: '{mailRequest.Name}' Phone: {mailRequest.Phone} Email: '{mailRequest.Reciever}':{Environment.NewLine}{mailRequest.Message}";
            }
            else if (mailRequest.ResetPassword)
            {
                mailRequest.Subject = "[Rideshare] Reset your password";
                reciver = mailRequest.Reciever;
                var token = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes($"{mailRequest.Reciever}***{DateTime.UtcNow.AddMinutes(10)}"));
                builder.TextBody = $"Please click this link to reset your password {GlobalConstants.Domain}/Auth/UpdatePassword?token={token}";
            }
            else if (!mailRequest.isFromContact)
            {
                mailRequest.Subject = "[Rideshare] Authenticate Your Email Address";
                reciver = mailRequest.Reciever;
                var token = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(mailRequest.Reciever));
                builder.TextBody = $"Please click this link to confirm your email {GlobalConstants.Domain}/Auth/ConfirmEmail?token={token}";
            }


            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailRequest.EmailFrom);
            email.To.Add(MailboxAddress.Parse(reciver));
            email.Subject = mailRequest.Subject ?? "";


            email.Body = builder.ToMessageBody();
            SmtpClient smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
