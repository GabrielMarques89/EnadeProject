#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.NHibernate;
using Abp.ObjectMapping;
using Abp.Runtime.Validation;
using EnadeProject.Model;
using EnadeProject.Model.Filter;
using EnadeProject.Model.Filter.Support;
using EnadeProject.NHibernate.EntityMappings.Entidades;

#endregion

namespace EnadeProject.Services
{
    public class RespostaService : EnadeProjectAppServiceBase<Resposta,RespostaDto, RespostaFilter>
    {
        public PerguntaService PerguntaService { get; set; }

        public RespostaService(PerguntaService perguntaService, IRepository<Resposta, long> repository,
                               IObjectMapper   objectMapper,    ISessionProvider            session) : base(
                                                                                                            repository,
                                                                                                            objectMapper,
                                                                                                            session)
        {
            PerguntaService = perguntaService;
        }

        public override Del<IQueryable<Resposta>, RespostaFilter> ApplyExtraFilter { get; set; } = 
        delegate (IQueryable<Resposta> set, RespostaFilter filtro)
        {
            //TODO: Implementações de qualquer filtro presente na classe RespostaFilter
            return set;
        };

        public override void ValidateLogicBusiness(RespostaDto model)
        {
            GarantirQueAListaDeErrosSempreEstaVazia();

            PerguntaPodeReceberNovaResposta(model);

            if (ErrosValidacaoLogicaNegocio.Count > 0)
            {
                throw new AbpValidationException("Erro de validação.", ErrosValidacaoLogicaNegocio);
            }
        }

        [Obsolete("Método só pra garantir a maneira como o service deveria se comportar. Remover assim que notar que a lista sempre está vazia")]
        private void GarantirQueAListaDeErrosSempreEstaVazia()
        {
            if (ErrosValidacaoLogicaNegocio.Count > 0)
            {
                throw new Exception("Checar método que não está correto. A lista deveria sempre estar vazia aqui");
            }
        }

        private void PerguntaPodeReceberNovaResposta(RespostaDto model)
        {
            var maxResposta = model.Correta ? 1 : 3;
            var quantasRespostasPerguntaTem =
                Repository.GetAll().Count(x => x.Pergunta.Id == model.Pergunta.Id && x.Correta == model.Correta);
            var perguntaPodeReceberNovaResposta = model.Id                          == 0
                                                      ? quantasRespostasPerguntaTem < maxResposta
                                                      : quantasRespostasPerguntaTem <= maxResposta;
            var stringIncorretasCorretas = model.Correta ? "corretas" : "incorretas";

            if (perguntaPodeReceberNovaResposta == false)
            {
                ErrosValidacaoLogicaNegocio.Add(new ValidationResult($"A pergunta já possui {maxResposta} respostas {stringIncorretasCorretas} cadastradas."));
            }
        }

        public List<RoundDto> GerarRound(int quantiaDePerguntas = 0)
        {
            quantiaDePerguntas = quantiaDePerguntas == 0 ? 15 : quantiaDePerguntas;
            
            var rep = Repository.GetAll();
            
            var idsPerguntas = CurrentSession.Session.CreateSQLQuery($"SELECT Id FROM PERGUNTA ORDER BY RAND() LIMIT {quantiaDePerguntas}").List<long>();

            var listaPorExtenso = new List<string>();
            
            foreach (var idsPergunta in idsPerguntas)
            {
                listaPorExtenso.Add(idsPergunta.ToString());
            }
            var perguntas = CurrentSession.Session.CreateSQLQuery($"SELECT * FROM PERGUNTA WHERE Id in ({String.Join(",", listaPorExtenso)})").AddEntity(typeof(Pergunta)).List<Pergunta>();
            


            var respostas = rep.Where(x=> idsPerguntas.Contains(x.Pergunta.Id));
            var respostasDto = new List<RespostaDto>();
            var listaFinal = new List<RoundDto>();
            foreach (var resposta in respostas)
            {
                respostasDto.Add(ObjectMapper.Map(resposta, new RespostaDto()));
            }

            foreach (var pergunta in perguntas)
            {
                var round = new RoundDto
                            {
                                Pergunta = ObjectMapper.Map(pergunta, new PerguntaDto()),
                                Resposta = respostasDto.Where(x => x.Pergunta.Id == pergunta.Id).ToList()
                            };
                listaFinal.Add(round);
            }

            return listaFinal.OrderBy(x=>x.Pergunta.Dificuldade).ToList();
        }
    }
}