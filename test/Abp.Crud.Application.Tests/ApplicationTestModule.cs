using Volo.Abp.Modularity;

namespace Abp.Crud;

[DependsOn(
    typeof(ApplicationModule),
    typeof(DomainTestModule)
    )]
public class ApplicationTestModule : AbpModule { }
