using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EnadeProject.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            config.EnsureInitialized();
        }
    }
}
