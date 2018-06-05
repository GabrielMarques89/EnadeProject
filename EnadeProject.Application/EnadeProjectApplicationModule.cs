using System.Reflection;
using Abp.Modules;

namespace EnadeProject
{
    [DependsOn(typeof(EnadeProjectCoreModule))]
    public class EnadeProjectApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
