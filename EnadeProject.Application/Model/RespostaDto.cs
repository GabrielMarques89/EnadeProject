using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.Model
{
    [AutoMapTo(typeof(Resposta))]
    public class RespostaDto : EntityDto
    {
        public string Conteudo { get; set; }
        public virtual bool Correta { get; set; }
        public PerguntaDto Pergunta { get; set; }

    }
}
