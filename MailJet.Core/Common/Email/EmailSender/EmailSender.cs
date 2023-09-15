using Mailjet.Client;
using MailJet.Core.Common.Email.Model;
using MailJet.Core.Interfaces;

namespace MailJet.Core.Common.Email.EmailSender
{
    public abstract class EmailSender : IEmailSender
  {
    public static MailjetClient CreateMailJetClient()
    {
      return new MailjetClient("xxx", "xxx");
    }
    protected abstract Task Send(EmailModel email);
    public async Task SendEmail(EmailModel emailModel)
    {
      await Send(emailModel);
    }


    public async Task SendEmail(string address, string subject, string body, List<EmailAttachment>? emailAttachment = null)
    {
      await Send(new EmailModel
      {
        Attachments = emailAttachment!,
        Body = body,
        EmailAddress = address,
        Subject = subject,
      });
    }
  }
}
