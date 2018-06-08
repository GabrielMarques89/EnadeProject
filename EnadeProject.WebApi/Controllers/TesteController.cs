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
    public class CompleteFucker
    {
        public FiltroEstaticoPergunta filtro { get; set;  }
        public PagedAndSortedResultRequestDto request { get; set; }
    }

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
        public HttpResponseMessage ListarPerguntas(CompleteFucker fucker)
        {
            return ResponseWrapper(_service.GetAndFilter(fucker.filtro, fucker.request ?? DefaultPagedAndSortedRequest ));
        }

        [HttpGet]
        [Route("buscarPergunta")]
        public HttpResponseMessage GetPergunta(int id)
        {
            return ResponseWrapper(_service.Get(new PerguntaDto {Id = id}));
        }
    }
}