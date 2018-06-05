using System.Configuration;
using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.NHibernate;
using FluentNHibernate.Cfg.Db;

namespace EnadeProject
{
    [DependsOn(typeof(AbpNHibernateModule), typeof(EnadeProjectCoreModule))]
    public class EnadeProjectDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            Configuration.Modules.AbpNHibernate().FluentConfiguration
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(Configuration.DefaultNameOrConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}