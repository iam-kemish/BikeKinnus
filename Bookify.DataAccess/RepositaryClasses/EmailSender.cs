using Microsoft.AspNetCore.Identity.UI.Services;

namespace BikeKinnus.DataAccess.RepositaryClasses
{

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
