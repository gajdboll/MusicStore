using Microsoft.AspNetCore.Identity.UI.Services;

namespace MusicUtilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email goes in here
            return Task.CompletedTask;
        }
    }
}
