using System.Net.Http;
using System.Web.Http;
using EnadeProject.Controllers.BaseControllers;
using EnadeProject.Model;
using EnadeProject.Model.Filter;
using EnadeProject.NHibernate.EntityMappings.Entidades;
using EnadeProject.Services;

namespace EnadeProject.Controllers
{
    [RoutePrefix("pergunta")]
    public class PerguntaApiController : BaseCrudApiController<Pergunta,PerguntaDto,PerguntaFilter,PerguntaService>
    {
        public PerguntaApiController(PerguntaService service) : base(service) { }

        [HttpPost]
        [Route("search")]
        public HttpResponseMessage Search(PerguntaFilter request)
        {
            return FiltrarPaginarOrdernar(request);
        }
    }
}