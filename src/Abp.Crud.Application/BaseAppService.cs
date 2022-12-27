using Abp.Crud.Localization;
using Volo.Abp.Application.Services;

namespace Abp.Crud;

public abstract class BaseAppService : ApplicationService
{
    protected BaseAppService()
    {
        LocalizationResource = typeof(Resource);
    }
}
