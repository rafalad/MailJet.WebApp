using MailJet.Core.Common.Email.Model;

namespace MailJet.Core.Interfaces
{
  public interface IEmailSender
  {
    Task SendEmail(string address, string subject, string body, List<EmailAttachment>? emailAttachment = null);
    Task SendEmail(EmailModel emailModel);
  }
}
