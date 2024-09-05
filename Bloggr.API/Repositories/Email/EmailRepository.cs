using System.Net;
using System.Net.Mail;
using Bloggr.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Bloggr.API.Repositories.Email
{

    public class EmailRepository : IEmailSender<User>
    {

        public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            var smtpClient = new SmtpClient(Environment.GetEnvironmentVariable("SMTP_HOST"))
            {
                Port = 587,
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USERNAME"), Environment.GetEnvironmentVariable("SMTP_PASSWORD")),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@agrimarket.com"),
                Subject = "Your Confirmation email",
                Body = $"<p>Dear {user.FirstName},</p><p>Please confirm your email address by clicking the following link:</p><p><a href='{confirmationLink}'>Confirm Email</a></p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            return smtpClient.SendMailAsync(mailMessage);
        }

        public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            throw new NotImplementedException();
        }

        public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            throw new NotImplementedException();
        }
    }
}