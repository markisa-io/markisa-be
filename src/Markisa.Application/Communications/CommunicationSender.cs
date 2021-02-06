using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Volo.Abp.DependencyInjection;

namespace Markisa.Communications
{
    public class CommunicationSender : ICommunicationSender, ITransientDependency
    {
        private readonly IConfiguration _configuration;

        public CommunicationSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string to, string subject, string body, string @from = null)
        {
            var configurationSection = _configuration.GetSection("Email");
            var client = new SendGridClient(configurationSection["ApiKey"]);
            var emailFrom = new EmailAddress(string.IsNullOrEmpty(from) ? configurationSection["SenderEmail"] : from);
            var emailTo = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(emailFrom, emailTo, subject, body, body);

           var response =  await client.SendEmailAsync(msg);

        }
    }
}
