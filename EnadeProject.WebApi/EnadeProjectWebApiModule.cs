using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace EnadeProject
{
    [DependsOn(typeof(AbpWebApiModule), typeof(EnadeProjectApplicationModule))]
    public class EnadeProjectWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Modules.AbpWebApi().IsValidationEnabledForControllers = false;
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(EnadeProjectApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
