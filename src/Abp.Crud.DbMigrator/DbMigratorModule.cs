using Abp.Crud.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Abp.Crud.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EntityFrameworkCoreModule),
    typeof(ContractsModule)
    )]
public class DbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = true);
    }
}
