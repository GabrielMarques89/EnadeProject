using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.Model
{
    [AutoMapTo(typeof(Resposta))]
    public class RespostaDto : EntityDto
    {
        public string Conteudo { get; set; }

    }
}
