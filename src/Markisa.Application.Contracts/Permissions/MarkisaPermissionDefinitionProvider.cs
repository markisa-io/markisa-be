using Markisa.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Markisa.Permissions
{
    public class MarkisaPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(MarkisaPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(MarkisaPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MarkisaResource>(name);
        }
    }
}
