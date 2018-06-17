using System.Net.Http;
using System.Web.Http;
using EnadeProject.Controllers.BaseControllers;
using EnadeProject.Controllers.Interface;
using EnadeProject.Model;
using EnadeProject.Model.Filter;
using EnadeProject.NHibernate.EntityMappings.Entidades;
using EnadeProject.Services;

namespace EnadeProject.Controllers
{
    [RoutePrefix("pergunta")]
    public class PerguntaApiController : BaseCrudApiController<Pergunta,PerguntaDto,PerguntaFilter,PerguntaService>, IEntityRestController<PerguntaDto,PerguntaFilter>
    {
        public PerguntaApiController(PerguntaService service) : base(service) { }

        [HttpPost]
        [Route("search")]
        public HttpResponseMessage FiltrarPaginarOrdernar(PerguntaFilter request)
        {
            return InnerFiltrarPaginarOrdernar(request);
        }

        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Salvar(PerguntaDto model)
        {
            return InnerSalvar(model);
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Atualizar(PerguntaDto model)
        {
            return InnerAtualizar(model);
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Remover(long id)
        {
            return InnerRemover(id);
        }

        [HttpPost]
        [Route("get")]
        public HttpResponseMessage BuscarPorId(long id)
        {
            return InnerBuscarPorId(id);
        }
    }
}