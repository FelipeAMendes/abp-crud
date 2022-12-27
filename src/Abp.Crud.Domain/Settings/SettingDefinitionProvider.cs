using Volo.Abp.Settings;

namespace Abp.Crud.Settings;

public class SettingDefinitionProvider : Volo.Abp.Settings.SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CrudSettings.MySetting1));
    }
}
