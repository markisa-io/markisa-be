using Volo.Abp.Settings;

namespace Markisa.Settings
{
    public class MarkisaSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(MarkisaSettings.MySetting1));
        }
    }
}
