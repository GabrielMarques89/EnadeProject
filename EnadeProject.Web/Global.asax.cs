﻿using System;
using System.Web.Http;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;

namespace EnadeProject.Web
{
    public class MvcApplication : AbpWebApplication<EnadeProjectWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            );

            GlobalConfiguration.Configure(WebApiConfig.Register);
            base.Application_Start(sender, e);
        }
    }
}
