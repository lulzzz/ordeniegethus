using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            var subject = UIResource.EmailConfirmationSubject;
            var body = string.Format(UIResource.EmailConfirmationBody, HtmlEncoder.Default.Encode(link));

            return emailSender.SendEmailAsync(email, subject, body);
        }
    }
}