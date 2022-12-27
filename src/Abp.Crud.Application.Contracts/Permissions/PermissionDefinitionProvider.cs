using Abp.Crud.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.Crud.Permissions;

public class PermissionDefinitionProvider : Volo.Abp.Authorization.Permissions.PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Permission.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CrudPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Resource>(name);
    }
}
