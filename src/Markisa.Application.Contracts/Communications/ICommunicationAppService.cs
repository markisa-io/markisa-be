using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Markisa.Communications
{
    public interface ICommunicationAppService
    {
        public Task SendRegistrationConfirmationEmail();
        public Task SendEmailToUser(EmailUserParamDto emailUserParam);
    }
}
