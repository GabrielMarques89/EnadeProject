using Abp.Web.Mvc.Views;

namespace EnadeProject.Web.Views
{
    public abstract class EnadeProjectWebViewPageBase : EnadeProjectWebViewPageBase<dynamic>
    {

    }

    public abstract class EnadeProjectWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected EnadeProjectWebViewPageBase()
        {
            LocalizationSourceName = EnadeProjectConsts.LocalizationSourceName;
        }
    }
}