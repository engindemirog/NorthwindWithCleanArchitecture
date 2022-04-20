using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mailing.MailKitImplementations
{
    public class MailkitMailService : IMailService
    {
        IConfiguration _configuration;
        MailSettings _mailSettings;

        public MailkitMailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings = _configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public void SendMail(Mail mail)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));

            email.To.Add(new MailboxAddress(mail.ToFullName, mail.ToEmail));

            email.Subject = mail.Subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = mail.TextBody,
                HtmlBody = mail.HtmlBody,

            };

            if (mail.Attachments != null)
            {
                foreach (var attachment in mail.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment);
                }
            }

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.Auto);
            //smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
