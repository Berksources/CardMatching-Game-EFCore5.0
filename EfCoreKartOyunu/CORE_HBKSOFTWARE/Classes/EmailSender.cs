using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using CORE_HBKSOFTWARE.Interfaces;
using CORE_HBKSOFTWARE.Models;
using Microsoft.Extensions.Configuration;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CORE_HBKSOFTWARE.Classes
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(EmailConfigModel emailConfig)
        {
            _configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .Build();
        }

        public async Task EmailSendAsync(MessageModel messageModel)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_configuration.GetSection("EmailConfiguration").GetSection("FromEmail").Value.ToString(), _configuration.GetSection("EmailConfiguration").GetSection("FromName").Value.ToString(), Encoding.UTF8);
                mailMessage.Subject = messageModel.Subject;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = messageModel.Content;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyTransferEncoding = TransferEncoding.Base64;
                mailMessage.To.Add(new MailAddress(messageModel.To));
                NetworkCredential networkCredential = new NetworkCredential(_configuration.GetSection("EmailConfiguration").GetSection("Username").Value.ToString(), _configuration.GetSection("EmailConfiguration").GetSection("Password").Value.ToString());
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = _configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value.ToString();
                smtpClient.EnableSsl = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("SSL").Value);
                smtpClient.UseDefaultCredentials = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("UseDefaultCredentials").Value);
                smtpClient.Port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("Port").Value);
                smtpClient.Credentials = networkCredential;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtpClient.SendMailAsync(mailMessage);
                smtpClient.Dispose();
            }
        }

        public async Task EmailSendWithMimeAsync(MessageModel messageModel)
        {
            string themePath = "";
            if (messageModel.MailType == "forgot-password")
            {
                themePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplate\\forgot-password.html";
            }
            else if (messageModel.MailType == "verify-account")
            {
                themePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplate\\verify-account.html";
            }
            StreamReader str = new StreamReader(themePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", messageModel.FirstName + " " + messageModel.LastName).Replace("[email]", messageModel.To).Replace("[link]", messageModel.Content);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration.GetSection("EmailConfiguration").GetSection("FromEmail").Value);
            email.From.Add(new MailboxAddress(_configuration.GetSection("EmailConfiguration").GetSection("FromName").Value, _configuration.GetSection("EmailConfiguration").GetSection("FromEmail").Value));
            email.To.Add(MailboxAddress.Parse(messageModel.To));
            email.Subject = messageModel.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value.ToString(), Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("Port").Value), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration.GetSection("EmailConfiguration").GetSection("Username").Value.ToString(), _configuration.GetSection("EmailConfiguration").GetSection("Password").Value.ToString());
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}