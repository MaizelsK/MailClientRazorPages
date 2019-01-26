using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Abstract;

namespace MailClientRazorPages.Pages
{
    public class RecentMessagesModel : PageModel
    {
        private readonly IEmailService _emailService;

        public IEnumerable<MailMessageViewModel> Messages { get; set; }

        public RecentMessagesModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnGetAsync()
        {
            Messages = await _emailService.GetRecentAsync();
        }
    }
}