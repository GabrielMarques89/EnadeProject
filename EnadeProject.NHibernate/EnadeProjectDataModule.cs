﻿using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace EnadeProject
{
    [DependsOn(typeof(AbpNHibernateModule), typeof(EnadeProjectCoreModule))]
    public class EnadeProjectDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString =
                ConfigurationManager.ConnectionStrings["CS_API_ENADE"].ConnectionString;
            Configuration.Modules.AbpNHibernate()
                .FluentConfiguration
                .Database(MySQLConfiguration.Standard.ConnectionString(Configuration.DefaultNameOrConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(c => new SchemaUpdate(c).Execute(Generate_Data_Definition_Sql(), false));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        private static Action<string> Generate_Data_Definition_Sql()
        {
            var pasta = new DirectoryInfo(@"\scripts_projeto_enade");
            if (!pasta.Exists)
            {
                pasta.Create();
            }

            string path = pasta.ToString() + @"\script_ddl_gerado_nhibernate.sql";
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo.Create();
            }

            Action<string> updateExport = x =>
            {
                using (var file = new FileStream(path, FileMode.Append, FileAccess.Write))
                using (var sw = new StreamWriter(file))
                {
                    sw.Write($@"{x};\n");
                }
            };

            return updateExport;
        }
    }
}