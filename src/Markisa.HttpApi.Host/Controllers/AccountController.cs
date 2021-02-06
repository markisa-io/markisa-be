using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Markisa.Controllers
{
    public class AccountController: AbpController
    {
        private IdentityUserManager UserManager { get; }

        public AccountController(IdentityUserManager userManager)
        {
            UserManager = userManager;
        }

        [HttpGet]
        [Route("confirmEmail")]
        [AllowAnonymous]
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

            if (result.Succeeded)
            {
                return Content(L.GetString(("EmailConfirmed")));
            }

            return Content(L.GetString("EmailConfirmationFailed"));
        }
    }
}
