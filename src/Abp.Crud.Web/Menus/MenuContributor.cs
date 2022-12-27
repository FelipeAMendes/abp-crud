using Abp.Crud.Localization;
using Abp.Crud.MultiTenancy;
using System.Threading.Tasks;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Abp.Crud.Web.Menus;

public class MenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();

        AddMenuItems(context);

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }

    private void AddMenuItems(MenuConfigurationContext context)
    {
        var localization = context.GetLocalizer<Resource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                Menu.Home,
                localization["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                Menu.TaskList,
                localization["Menu:TaskList"],
                "~/tasks",
                icon: "fas fa-list-check",
                order: 1
            )
        );

        context.Menu.Items.Insert(
            2,
            new ApplicationMenuItem(
                Menu.PersonsListApi,
                localization["Menu:PersonsListApi"],
                "~/list",
                icon: "fas fa-people-group",
                order: 2
            )
        );
    }
}
