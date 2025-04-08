
namespace Boecker.Domain.Constants;

public class SmtpSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; } = default!;
    public bool EnableSsl { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string SenderName { get; set; } = default!;
    public string SenderEmail { get; set; } = default!;
}
