#region Região de Imports

using System.Net.Http;
using Abp.Application.Services.Dto;
using EnadeProject.Interfaces;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;
using IEntityDto = EnadeProject.Model.IEntityDto;

#endregion

namespace EnadeProject.Controllers.BaseControllers
{
    public abstract class BaseCrudApiController<T, TDto, TFilter, TService> : BaseRestController
        where TService : EnadeProjectAppServiceBase<T, TDto, TFilter>
        where T : EntidadeBase
        where TDto : IEntityDto
        where TFilter : IFilter
    {
        protected TService Service;

        protected BaseCrudApiController(TService service)
        {
            Service = service;
        }

        protected virtual HttpResponseMessage InnerFiltrarPaginarOrdernar(TFilter request)
        {
            var result = Service.GetAndFilter(request);
            return ResponseWrapper(result);
        }

        protected virtual HttpResponseMessage InnerSalvar(TDto model)
        {
            return ResponseWrapper(Service.Create(model));
        }

        protected virtual HttpResponseMessage InnerAtualizar(TDto model)
        {
            return ResponseWrapper(Service.Create(model));
        }

        protected virtual HttpResponseMessage InnerRemover(long id)
        {
            Service.Delete(new EntityDto<long>(id));
            return Success();
        }

        protected virtual HttpResponseMessage InnerBuscarPorId(long id)
        {
            return ResponseWrapper(Service.Get(id));
        }
    }
}