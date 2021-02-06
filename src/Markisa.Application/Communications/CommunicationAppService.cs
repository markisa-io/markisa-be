using System.Text;
using System.Threading.Tasks;
using Markisa.Communications;
using Markisa.Communications.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Volo.Abp.Identity;
using Volo.Abp.TextTemplating;
using Volo.Abp.Users;

namespace Markisa
{
    public class CommunicationAppService : MarkisaAppService, ICommunicationAppService
    {
        private readonly ICurrentUser _currentUser;
        private readonly ICommunicationSender _sender;
        private readonly IdentityUserManager _userManager;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly ITemplateRenderer _templateRenderer;

        public CommunicationAppService(IdentityUserManager userManager, ICurrentUser currentUser, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory, ITemplateRenderer templateRenderer, ICommunicationSender sender)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _actionContextAccessor = actionContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _templateRenderer = templateRenderer;
            _sender = sender;
        }

        public virtual async Task SendRegistrationConfirmationEmail()
        {
            if (_currentUser.Id.HasValue)
            {
                var identityUser = await _userManager.FindByIdAsync(_currentUser.Id.ToString());
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                byte[] encbuff = Encoding.UTF8.GetBytes(token);
                var code = WebEncoders.Base64UrlEncode(encbuff);

                var confirmationLink = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).ActionLink(
                    "ConfirmEmail", "Account",
                    new { userId = _currentUser.Id, token = code }, protocol: _actionContextAccessor.ActionContext.HttpContext.Request.Scheme);

                var content = await _templateRenderer.RenderAsync(
                    EmailTemplates.EmailConfirmation,
                    new { link = confirmationLink }
                );

                await SendEmailToUser(new EmailUserParamDto()
                { Body = content, Subject = L["EmailConfirmationSubject"] });
            }
        }

        public virtual async Task SendEmailToUser(EmailUserParamDto emailUserParam)
        {
            if (_currentUser.Id.HasValue)
            {
                await _sender.SendEmail(_currentUser.Email, emailUserParam.Subject, emailUserParam.Body);
            }
        }
    }
}
