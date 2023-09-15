using Mailjet.Client;
using MailJet.Core.Common.Email.Model;
using MailJet.Core.Interfaces;
using Newtonsoft.Json.Linq;

namespace MailJet.Core.Common.Email.EmailProvider
{
    public class MailJetProvider : EmailSender.EmailSender, IEmailSender
  {
    protected override async Task Send(EmailModel email)
    {
      try
      {
        JArray jArray = new JArray();
        JArray attachments = new JArray();
        if (email.Attachments != null && email.Attachments.Count() > 0)
        {
          email.Attachments.ToList().ForEach(attachment => attachments.Add(
            new JObject
            {
            new JProperty("Content-Type", attachment.ContentType),
            new JProperty("Filename", attachment.ContentType),
            new JProperty("Content", Convert.ToBase64String(attachment.Data))
            }));
        }
        jArray.Add(new JObject
          {
           new JProperty("FromEmail","rafal@summitstories.blog"),
           new JProperty("FromName","Rafal"),
           new JProperty("Recipients", new JArray
           {
              new JObject
              {
                new JProperty("Email", email.EmailAddress),
                new JProperty("Name", email.EmailAddress)
              }
            }),
                new JProperty("Subject", email.Subject),
                new JProperty("Text-part", email.Body),
                new JProperty("Html-part", email.Body),
                new JProperty("Attachments", attachments)
        });
        var client = CreateMailJetClient();
        var request = new MailjetRequest
        {
          Resource = Mailjet.Client.Resources.Send.Resource
        }
        .Property(Mailjet.Client.Resources.Send.Messages, jArray);

        var response = await client.PostAsync(request);

        Console.WriteLine($"Send result {response.StatusCode} with message: {response.Content}");
      } catch (Exception ex)
      {
        Console.WriteLine(ex.Message.ToString());
      }
      

    }
  }
}
