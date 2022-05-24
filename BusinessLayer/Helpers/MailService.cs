using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public class MailService
    {
        public static void SendMail(int ProjectId,string body,string subject,string email)
        {
            var fromAddress = new MailAddress("koubpm@gmail.com");
            var toAddress = new MailAddress(email);
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Sabsay123321")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
