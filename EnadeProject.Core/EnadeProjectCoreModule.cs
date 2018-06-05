using System.Reflection;
using Abp.Modules;

namespace EnadeProject
{
    public class EnadeProjectCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
