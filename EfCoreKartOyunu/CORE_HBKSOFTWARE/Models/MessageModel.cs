using Microsoft.AspNetCore.Http;

namespace CORE_HBKSOFTWARE.Models
{
    public class MessageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string MailType { get; set; } //forgot , verify
        public IFormFileCollection Attachments { get; set; }

        public MessageModel(string firstname, string lastname, string to, string subject, string content, string mailtype, IFormFileCollection attachments)
        {
            FirstName = firstname;
            LastName = lastname;
            To = to;
            Subject = subject;
            Content = content;
            MailType = mailtype;
            Attachments = attachments;
        }
    }
}