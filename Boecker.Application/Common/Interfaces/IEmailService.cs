
namespace Boecker.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailWithAttachmentAsync(string toEmail, string subject, string body, byte[] attachment, string attachmentName);
    Task SendEmailAsync(string toEmail, string subject, string body);
}

