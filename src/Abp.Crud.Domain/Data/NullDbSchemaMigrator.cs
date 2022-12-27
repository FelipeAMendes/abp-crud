using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.Crud.Data;

public class NullDbSchemaMigrator : IDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
