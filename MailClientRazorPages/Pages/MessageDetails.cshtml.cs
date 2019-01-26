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
    public class MessageDetailsModel : PageModel
    {
        private readonly IEmailService _emailService;

        [BindProperty]
        public MailMessageViewModel Message { get; set; }

        public MessageDetailsModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnGetAsync(int index)
        {
            var messages = await _emailService.GetRecentAsync(index);
            Message = messages.FirstOrDefault();
        }
    }
}