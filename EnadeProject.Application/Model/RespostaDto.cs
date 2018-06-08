using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.Model
{
    [AutoMapTo(typeof(Resposta))]
    public class RespostaDto : BaseEntityDto
    {
        public string Conteudo { get; set; }

    }
}
