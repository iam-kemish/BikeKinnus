using BikeKinnus.DataAccess.Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class EmailSenderr: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
