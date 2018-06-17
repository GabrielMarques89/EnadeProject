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
    [RoutePrefix("round")]
    public class RoundController : BaseRestController
    {
        public RespostaService RespostaService;
        public PerguntaService PerguntaService;

        public RoundController(RespostaService respostaService,PerguntaService perguntaService)
        {
            RespostaService = respostaService;
            PerguntaService = perguntaService;
        }

        [HttpPost]
        [Route("get")]
        public HttpResponseMessage BuscarSetDePerguntasERespostas()
        {
            return ResponseWrapper(RespostaService.GerarRound(15));
        }
    }
}