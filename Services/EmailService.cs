using MimeKit.Text;
using MimeKit;
using SimpleEmailApp.Models;
using MailKit.Net.Smtp;

namespace SimpleEmailApp.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            //email.To.Add(MailboxAddress.Parse("janis.tremblay44@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };

            try
            {
                using var smtp = new SmtpClient();
                var EmailHost = _configuration.GetSection("EmailHost").Value;
                smtp.Connect(EmailHost, 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
