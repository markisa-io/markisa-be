using Markisa.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Markisa.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class MarkisaController : AbpController
    {
        protected MarkisaController()
        {
            LocalizationResource = typeof(MarkisaResource);
        }
    }
}