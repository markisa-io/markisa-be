using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markisa.Localization;
using Volo.Abp.Account.Emailing.Templates;
using Volo.Abp.Account.Localization;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace Markisa.Communications.Templates
{
    class EmailTemplatesProvider: TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition(
                    EmailTemplates.EmailConfirmation,
                    displayName: LocalizableString.Create<MarkisaResource>($"TextTemplate:{EmailTemplates.EmailConfirmation}"),
                    layout: StandardEmailTemplates.Layout,
                    localizationResource: typeof(MarkisaResource)
                ).WithVirtualFilePath("/Communications/Templates/EmailConfirmation.tpl", true)
            );
        }
    }
}
