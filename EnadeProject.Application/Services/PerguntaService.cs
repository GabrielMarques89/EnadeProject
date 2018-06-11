#region Imports

using System.Linq;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using EnadeProject.Model;
using EnadeProject.Model.Filter;
using EnadeProject.NHibernate.EntityMappings.Entidades;

#endregion

namespace EnadeProject.Services
{
    public class PerguntaService : EnadeProjectAppServiceBase<Pergunta,PerguntaDto, PerguntaFilter>
    {
        public PerguntaService(IRepository<Pergunta,long> repository, IObjectMapper objectMapper) : base(
            repository, objectMapper){}
        

        public override Del<IQueryable<Pergunta>, PerguntaFilter> ApplyExtraFilter { get; set; } = 
        delegate (IQueryable<Pergunta> set, PerguntaFilter filtro)
        {
            //TODO: Implementações de qualquer filtro presente na classe PerguntaFilter
            return set;
        };
    }
}