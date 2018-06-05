using Abp.Application.Services;

namespace EnadeProject
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class EnadeProjectAppServiceBase : ApplicationService
    {
        protected EnadeProjectAppServiceBase()
        {
            LocalizationSourceName = EnadeProjectConsts.LocalizationSourceName;
        }
    }
}