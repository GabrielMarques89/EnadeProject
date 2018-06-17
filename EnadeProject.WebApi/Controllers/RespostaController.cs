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
    [RoutePrefix("resposta")]
    public class RespostaApiController : BaseCrudApiController<Resposta,RespostaDto,RespostaFilter,RespostaService>, IEntityRestController<RespostaDto,RespostaFilter>
    {
        public RespostaApiController(RespostaService service) : base(service) { }

        [HttpPost]
        [Route("search")]
        public HttpResponseMessage FiltrarPaginarOrdernar(RespostaFilter request)
        {
            return InnerFiltrarPaginarOrdernar(request);
        }

        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Salvar(RespostaDto model)
        {
            return InnerSalvar(model);
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Atualizar(RespostaDto model)
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