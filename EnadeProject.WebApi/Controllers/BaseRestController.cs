using System.Net;
using System.Net.Http;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Newtonsoft.Json;

namespace EnadeProject.Controllers
{
    [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
    public class BaseRestController : AbpApiController
    {
        #region Response Wrappers

        protected string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private HttpResponseMessage Response(object content)
        {
            return Response(HttpStatusCode.OK, ToJson(content));
        }

        protected HttpResponseMessage Success()
        {
            return Response(HttpStatusCode.OK,"");
        }

        protected HttpResponseMessage ResponseWrapper(object result)
        {
            return MultipleResponse(HttpStatusCode.OK, result);
        }

        protected HttpResponseMessage SingleResponse(HttpStatusCode status, object errors)
        {
            return MultipleResponse(status, new {errors});
        }

        private HttpResponseMessage MultipleResponse(HttpStatusCode status, object errors)
        {
            return Response(status, ToJson(errors));
        }

        protected HttpResponseMessage Response(HttpStatusCode status, string content)
        {
            var response = new HttpResponseMessage(status)
            {
                Content = new StringContent(content)
            };
            return response;
        }

        #endregion
    }
}