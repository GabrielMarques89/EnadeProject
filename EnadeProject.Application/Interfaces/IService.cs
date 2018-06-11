#region Região de Imports

using System.Linq;
using Abp.Application.Services.Dto;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities.Interfaces;
using IEntityDto = EnadeProject.Model.IEntityDto;

#endregion

namespace EnadeProject.Interfaces
{
    public interface IService<T, TDto, TFilter> : IServiceSimple<TDto>
        where T : IEntidadeBase
        where TDto : IEntityDto
        where TFilter : IFilter
    {
        /// <summary>
        /// Método apenas para simplificar a busca pelo Id diretamente.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TDto Get(long input);

        /// <summary>
        /// Método apenas para simplificar a exclusão pelo Id diretamente.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Delete(long input);


        IQueryable<T> ApplyFilter(TFilter filtro);
        PagedResultDto<TDto> GetAndFilter(TFilter filtro);
        
        PagedResultDto<TDto> ApplyPagination(IQueryable<IEntidadeBase> set,PagedAndSortedResultRequestDto input);
    }
}