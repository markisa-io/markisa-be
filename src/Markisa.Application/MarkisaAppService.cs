using System;
using System.Collections.Generic;
using System.Text;
using Markisa.Localization;
using Volo.Abp.Application.Services;

namespace Markisa
{
    /* Inherit your application services from this class.
     */
    public abstract class MarkisaAppService : ApplicationService
    {
        protected MarkisaAppService()
        {
            LocalizationResource = typeof(MarkisaResource);
        }
    }
}
