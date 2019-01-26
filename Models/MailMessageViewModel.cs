using System;

namespace Models
{
    public class MailMessageViewModel
    {
        public int Index { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
