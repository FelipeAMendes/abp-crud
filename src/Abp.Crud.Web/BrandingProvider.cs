﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Abp.Crud.Web;

[Dependency(ReplaceServices = true)]
public class BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Crud";
}
