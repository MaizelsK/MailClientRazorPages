using MailKit.Net.Pop3;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Models;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services
{
    public class GmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;

        private readonly string _popHost;
        private readonly int _popPort;

        private readonly string _username;
        private readonly string _password;

        public GmailService(IConfigurationSection mailSection)
        {
            _smtpHost = mailSection["SmtpSettings:Host"];
            _smtpPort = int.Parse(mailSection["SmtpSettings:Port"]);

            _popHost = mailSection["PopSettings:Host"];
            _popPort = int.Parse(mailSection["PopSettings:Port"]);

            _username = mailSection["MailCredentials:Username"];
            _password = mailSection["MailCredentials:Password"];
        }

        public async Task<IEnumerable<MailMessageViewModel>> GetRecentAsync(int index = -1)
        {
            IEnumerable<MimeMessage> mimeMessages;

            using (var client = new Pop3Client())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_popHost, _popPort, true);

                await client.AuthenticateAsync("recent:" + _username, _password);

                if (index == -1)
                    mimeMessages = await client.GetMessagesAsync(0, client.Count);
                else
                {
                    mimeMessages = new List<MimeMessage>();
                    mimeMessages.Append(await client.GetMessageAsync((int)index));
                }

                client.Disconnect(true);
            }

            int i = 0;
            var mailMessages = mimeMessages.Select(mime => new MailMessageViewModel
            {
                Index = ++i,
                From = mime.From[0].Name,
                To = mime.To.Mailboxes.First().Name,
                Subject = mime.Subject,
                Body = mime.TextBody
            });

            return mailMessages;
        }

        public async Task SendMailAsync(string to, string title, string body)
        {
            MailMessage mailMessage = new MailMessage(_username, to)
            {
                Body = body,
                Subject = title
            };

            using (SmtpClient client = new SmtpClient())
            {
                client.Host = _smtpHost;
                client.Port = _smtpPort;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_username, _password);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
