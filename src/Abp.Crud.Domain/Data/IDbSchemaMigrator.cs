using System.Threading.Tasks;

namespace Abp.Crud.Data;

public interface IDbSchemaMigrator
{
    Task MigrateAsync();
}
