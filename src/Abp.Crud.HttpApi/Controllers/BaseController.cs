using Abp.Crud.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.Crud.Controllers;

public abstract class BaseController : AbpControllerBase
{
    protected BaseController()
    {
        LocalizationResource = typeof(Resource);
    }
}
