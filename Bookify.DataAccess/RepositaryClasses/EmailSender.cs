using Microsoft.AspNetCore.Identity.UI.Services;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    //This Class was made to implement interface IEmailSender, to solve "Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UI.Services.IEmailSender' while attempting to activate 'BikeKinnus.Areas.Identity.Pages.Account.RegisterModel'."This issue.
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
