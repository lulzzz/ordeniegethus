using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Arkitektum.Orden.Services
{
    /// <summary>
    ///     All emails is sent through this class. Emails are sent with Sendgrid.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        //private readonly AppSettings _appSettings;

        //public EmailSender(AppSettings appSettings)
        //{
        //    _appSettings = appSettings;
        //}

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //var client = new SendGridClient(_appSettings.EmailSettings.SendgridApiKey);
            //var from = new EmailAddress(_appSettings.EmailSettings.FromAddress);
            //var to = new EmailAddress(email);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            //await client.SendEmailAsync(msg);
        }
    }
}