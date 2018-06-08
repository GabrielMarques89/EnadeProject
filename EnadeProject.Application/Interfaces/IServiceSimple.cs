using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EnadeProject.Model;

namespace EnadeProject.Interfaces
{
    public interface IServiceSimple<T,TDto> : ICrudAppService<TDto,long> where TDto : BaseEntityDto
    {}
}