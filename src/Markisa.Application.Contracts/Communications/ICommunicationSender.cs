using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markisa.Communications
{
    public interface ICommunicationSender
    {
        Task SendEmail(string to, string subject, string body, string from = null );
    }
}
