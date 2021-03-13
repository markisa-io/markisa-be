using System.Text;
using System.Threading.Tasks;
using Markisa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SendGrid;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Markisa.Controllers
{
    public class AccountController: AbpController
    {
        private IdentityUserManager UserManager { get; }
        private IConfiguration Configuration { get; }

        public AccountController(IdentityUserManager userManager, IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }

        [HttpGet]
        [Route("confirmEmail")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Content(L.GetString("CannotFindUser"));
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            var result = await UserManager.ConfirmEmailAsync(user, codeDecoded);

            var configurationSection = Configuration.GetSection("Url");
            var failedLink = configurationSection["ContactUsPage"];
            var loginLink = configurationSection["LoginPage"];

            var model = new ConfirmEmailViewModel(){Success = result.Succeeded, FailedActionLink = failedLink , SuccessActionLink = loginLink};

            return View("ConfirmEmail", model);
        }
    }
}
