#region Imports

using System.Linq;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using EnadeProject.Model;
using EnadeProject.Model.Filter.BaseFilter;
using EnadeProject.NHibernate.EntityMappings.Entidades;

#endregion

namespace EnadeProject.Services
{
    public class RespostaService : EnadeProjectAppServiceBase<Resposta,RespostaDto,BaseStaticFilter>
    {
        public RespostaService(IRepository<Resposta,long> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
        {}

        public override Del<IQueryable<Resposta>, BaseStaticFilter> ApplyExtraFilter { get; set; }
    }
}