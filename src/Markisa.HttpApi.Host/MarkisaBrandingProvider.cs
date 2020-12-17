using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Markisa
{
    [Dependency(ReplaceServices = true)]
    public class MarkisaBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Markisa";
    }
}
