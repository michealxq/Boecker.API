
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Constants;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;



namespace Boecker.Infrastructure.Services;

public class EmailService(IOptions<SmtpSettings> options) : IEmailService
{
    private readonly SmtpSettings _settings = options.Value;

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(toEmail);

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = string.IsNullOrWhiteSpace(_settings.User)
                ? CredentialCache.DefaultNetworkCredentials
                : new NetworkCredential(_settings.User, _settings.Password)
        };

        await client.SendMailAsync(message);
    }

    public async Task SendEmailWithAttachmentAsync(string toEmail, string subject, string body, byte[] attachment, string attachmentName)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(toEmail);
        message.Attachments.Add(new Attachment(new MemoryStream(attachment), attachmentName));

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = string.IsNullOrWhiteSpace(_settings.User)
                ? CredentialCache.DefaultNetworkCredentials
                : new NetworkCredential(_settings.User, _settings.Password)
        };

        await client.SendMailAsync(message);
    }
}

