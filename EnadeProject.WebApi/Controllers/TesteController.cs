using System;
using System.Net.Http;
using System.Web.Http;

namespace EnadeProject.Controllers
{
    [RoutePrefix("banana")]
    public class TesteController : BaseRestController
    {
        [HttpPost]
        [Route("action1")]
        public HttpResponseMessage Action11()
        {
            return ResponseWrapper("1");
        }

        [HttpPost]
        [Route("action2")]
        public HttpResponseMessage Action22()
        {
            throw new NotImplementedException();
        }
    }
}