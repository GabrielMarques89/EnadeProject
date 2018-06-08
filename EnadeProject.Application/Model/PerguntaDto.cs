using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.Model
{
    [AutoMapTo(typeof(Pergunta))]
    public class PerguntaDto : BaseEntityDto
    {
        public virtual string Conteudo { get; set; }
        public virtual decimal Dificuldade { get; set; }
    }
}
