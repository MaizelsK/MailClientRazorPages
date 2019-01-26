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
    public class SendMailModel : PageModel
    {
        private readonly IEmailService _emailService;

        [BindProperty]
        public SendMailViewModel MailModel { get; set; }

        public SendMailModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
                await _emailService.SendMailAsync(MailModel.To, MailModel.Subject, MailModel.Body);

            return RedirectToPage("Index");
        }
    }
}