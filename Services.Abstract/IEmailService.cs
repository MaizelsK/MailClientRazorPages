using Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string title, string body);
        Task<IEnumerable<MailMessageViewModel>> GetRecentAsync(int index = -1);
    }
}
