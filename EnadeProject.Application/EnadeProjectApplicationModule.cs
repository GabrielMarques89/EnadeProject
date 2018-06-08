using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace EnadeProject
{
    [DependsOn(typeof(EnadeProjectCoreModule), typeof(AbpAutoMapperModule))]
    public class EnadeProjectApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}