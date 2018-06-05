using Abp.Web.Mvc.Controllers;

namespace EnadeProject.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class EnadeProjectControllerBase : AbpController
    {
        protected EnadeProjectControllerBase()
        {
            LocalizationSourceName = EnadeProjectConsts.LocalizationSourceName;
        }
    }
}