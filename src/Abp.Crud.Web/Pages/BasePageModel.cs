using Abp.Crud.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.Crud.Web.Pages;

public abstract class BasePageModel : AbpPageModel
{
    protected BasePageModel()
    {
        LocalizationResourceType = typeof(Resource);
    }
}
