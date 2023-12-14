using Microsoft.AspNetCore.Identity.UI.Services;

namespace InternetBanking.Service.MailService
{
    public class SendConfirmationMail : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
