using InternetBanking.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InternetBanking.Service.MailService
{
    public class MailSetting
    {
        public string? Mail { get; set; }
        public string? DisplayName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
    }
    public class SendMailService
    {
        readonly MailSetting MailSetting;

        public SendMailService(IOptions<MailSetting> MailSetting)
        {
            this.MailSetting = MailSetting.Value;
        }
        public string GetEmailBody(Transaction transac)
        {
            string templatePath = "Service/MailService/mailTemplate.html";
            string template = File.ReadAllText(templatePath);

            // Replace placeholders with actual content
            template = template.Replace("{{Id}}", transac.Id)
                                .Replace("{{Name}}", transac.SenderAccountNumber)
                                .Replace("{{Receiver}}", transac.ReceiverAccountNumber)
                                .Replace("{{Amount}}", transac.Amount.ToString())
                                .Replace("{{Content}}", transac.Content)
                                .Replace("{{Date}}", transac.TransactionDate.ToString("mm/dd/yyyy"))
                                .Replace("{{Time}}", transac.TransactionDate.ToString("H:mm:ss"));
            return template;
        }
        
        

        public async Task SendEmail(Transaction transac)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(MailSetting.DisplayName, MailSetting.Mail));
            message.To.Add(new MailboxAddress("Tuan", "anhtuan200745@gmail.com"));
            message.Subject = "NexBank's Transaction Bill! Thank you for using our service";

            string htmlBody = GetEmailBody(transac);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody;

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(MailSetting.Host, MailSetting.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(MailSetting.Mail, MailSetting.Password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error sending email: {ex.Message}");

                // Save the email to a file for manual inspection (if needed)
                if (!Directory.Exists("sentMail"))
                {
                    Directory.CreateDirectory("sentMail");
                }

                var mailFile = $"sentMail/{Guid.NewGuid()}.eml";
                await message.WriteToAsync(mailFile);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
        
        public string GetEmailHelpBody(HelpRequest helpRequest)
        {
            string templatePath = "Service/MailService/MailProcessRequest.html";
            string template = File.ReadAllText(templatePath);

            // Replace placeholders with actual content
            template = template.Replace("{{Id}}", helpRequest.Id.ToString())
                .Replace("{{AccountId}}", helpRequest.AccountId)
                .Replace("{{CustomerPersonalId}}", helpRequest.CustomerPersonalId)
                .Replace("{{EmployeeId}}", helpRequest.EmployeeId)
                .Replace("{{RequestTypeId}}", helpRequest.RequestTypeId.ToString())
                .Replace("{{Content}}", helpRequest.Content)
                .Replace("{{CreatedDate}}", helpRequest.CreatedDate.ToString())
                .Replace("{{Answer}}", helpRequest.Answer)
                .Replace("{{Status}}", helpRequest.Status.ToString());
            return template;
        }
        
        public async Task SendEmailHelpRequest(HelpRequest helpRequest) {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(MailSetting.DisplayName, MailSetting.Mail));
            message.To.Add(new MailboxAddress("Thieu", "thieu.dv156@gmail.com"));
            message.Subject = "NexBank's Process HelpRequest! Thank you for using our service";

            string htmlBody = GetEmailHelpBody(helpRequest);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody;

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(MailSetting.Host, MailSetting.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(MailSetting.Mail, MailSetting.Password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error sending email: {ex.Message}");

                // Save the email to a file for manual inspection (if needed)
                if (!Directory.Exists("sentMail"))
                {
                    Directory.CreateDirectory("sentMail");
                }

                var mailFile = $"sentMail/{Guid.NewGuid()}.eml";
                await message.WriteToAsync(mailFile);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

    }
}
