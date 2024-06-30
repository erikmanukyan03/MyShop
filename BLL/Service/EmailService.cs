using BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class EmailService : IEmailService
    {
        public async Task Send(string to, string body, string subject)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new MailAddress("mr.erik.manukyan97@gmail.com");
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Subject = subject; // email subject
            mailMessage.Body = body;// email content
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("", "") // smtp Credentials
            };
            try
            {
                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {
                // somehow handle the error maybe write in log file
            }
        }
    }
}
