using System;
using System.Reflection;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

namespace Arkitektum.Orden.Services
{
    /// <summary>
    ///     All emails is sent through this class. Emails are sent with Sendgrid. Using appsettings.json for configuration details.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly AppSettings _appSettings;

        public EmailSender(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SendGridClient(_appSettings.EmailSettings.SendgridApiKey);
                
                var from = new EmailAddress(_appSettings.EmailSettings.FromAddress);
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

                Log.Information("Sending email to: {email} with subject: {subject}", email, subject);

                await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while sending email: " + e.Message);
            }
        }
    }
}