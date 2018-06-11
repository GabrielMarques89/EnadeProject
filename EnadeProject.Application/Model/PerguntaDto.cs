using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.Model
{
    [AutoMapTo(typeof(Pergunta))]
    public class PerguntaDto : EntityDto
    {
        public virtual string Conteudo { get; set; }
        public virtual decimal Dificuldade { get; set; }
    }
}
