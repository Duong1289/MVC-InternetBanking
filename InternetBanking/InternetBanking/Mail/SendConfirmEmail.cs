﻿using InternetBanking.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;


namespace InternetBanking.Mail
{
    public class MailSettings
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
    }
    public class SendMailService : IEmailSender
    {
        readonly MailSettings mailSettings;
        public SendMailService(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress("NexBank", "nexbank.online.banking@gmail.com");
            message.From.Add(new MailboxAddress("NexBank", "nexbank.online.banking@gmail.com"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect("smtp.gmail.com", 587);
                smtp.Authenticate("nexbank.online.banking@gmail.com", "vopnfioavbaolrxn");
                await smtp.SendAsync(message);

            }
            catch (Exception ex)
            {
                if (!Directory.Exists("mailssave"))
                {
                    Directory.CreateDirectory("mailssave");
                    var mailFile = $"mailssave/{Guid.NewGuid()}.eml";
                    await message.WriteToAsync(mailFile);
                }
            }
            smtp.Disconnect(true);
        }
    }
}