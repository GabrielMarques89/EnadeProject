using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Abp.Application.Services.Dto;
using EnadeProject.Model;
using EnadeProject.Model.Filter;
using EnadeProject.Model.Filter.Support;
using EnadeProject.Services;

namespace EnadeProject.Controllers
{
    [RoutePrefix("test")]
    public class TesteController : BaseRestController
    {
        private readonly PerguntaService _service;

        public TesteController(PerguntaService appService)
        {
            _service = appService;
        }

        [HttpPost]
        [Route("SalvarPergunta")]
        public HttpResponseMessage SalvarPergunta(string conteudo, decimal dificuldade=0.5m)
        {
            return ResponseWrapper(_service.Create(new PerguntaDto
            {
                Conteudo = conteudo,
                Dificuldade = dificuldade
            }));
        }

        [HttpPost]
        [Route("Listar")]
        public HttpResponseMessage ListarPerguntas()
        {
            PagedAndSortedResultRequestDto pagedAndSortedResultRequest = null;
            if (pagedAndSortedResultRequest == null)
            {
                pagedAndSortedResultRequest = DefaultPagedAndSortedRequest;
            }

            var filtro = HardCodedFiltro();
            return ResponseWrapper(_service.GetAndFilter(filtro,pagedAndSortedResultRequest));
        }

        private static FiltroEstaticoPergunta HardCodedFiltro()
        {
            return new FiltroEstaticoPergunta
            {
                Set = new List<IndividualFilter>
                {
                    //new IndividualFilter
                    //{
                    //    Campo = "Conteudo",
                    //    Value = "Isso foi uma pergunta de teste ?",
                    //    Criteria = Criterio.Igual,
                    //}
                    new IndividualFilter
                    {
                        Campo = "Id",
                        Value = "1",
                        Criteria = Criterio.Igual,
                    }
                }
            };
        }

        [HttpGet]
        [Route("buscarPergunta")]
        public HttpResponseMessage GetPergunta(int id)
        {
            return ResponseWrapper(_service.Get(new PerguntaDto {Id = id}));
        }
    }
}