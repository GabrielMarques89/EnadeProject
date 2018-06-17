using System.Collections.Generic;
using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.Model
{
    public class RoundDto : EntityDto
    {
        public PerguntaDto Pergunta { get; set; }
        public List<RespostaDto> Resposta { get; set; }
        
    }
}
