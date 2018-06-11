using Abp.Application.Services;
using IEntityDto = EnadeProject.Model.IEntityDto;

namespace EnadeProject.Interfaces
{
    public interface IServiceSimple<TDto> : ICrudAppService<TDto,long> where TDto : IEntityDto
    {}
}