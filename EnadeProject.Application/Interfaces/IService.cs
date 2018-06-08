using System.Linq;
using EnadeProject.Model;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

namespace EnadeProject.Interfaces
{
    public interface IService<T,TDto, TFilter> : IServiceSimple<T,TDto>
        where T : EntidadeBase
        where TDto : BaseEntityDto
        where TFilter : IFilter
    {
        //void IService(IRepository<T> repository, IObjectMapper objectMapper, string doidera);
        IQueryable<T> Filter<TFiltro>(TFiltro filtro) where TFiltro : TFilter;
    }
}