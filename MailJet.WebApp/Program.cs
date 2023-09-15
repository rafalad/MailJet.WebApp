using MailJet.Core.HostedServices;
using MailJet.Core.Common.Email.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EmailHostedService>();
builder.Services.AddHostedService(provider => provider.GetService<EmailHostedService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test-email", async (EmailHostedService hostedService) => {
  await hostedService.SendEmailAsync(new EmailModel
  {
    EmailAddress = "rafal.adamczyk01@gmail.com",
    Subject = "Test Message",
    Body = "<strong> Test Message </strong>",
    Attachments = null
  }) ;
}).WithName("TestEmail");

app.Run();