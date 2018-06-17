#region Região de Imports

using System.Net.Http;
using EnadeProject.Interfaces;
using EnadeProject.Model;

#endregion

namespace EnadeProject.Controllers.Interface
{
    public interface IEntityRestController<TDto, TFilter>
        where TDto : IEntityDto
        where TFilter : IFilter
    {
        HttpResponseMessage FiltrarPaginarOrdernar(TFilter request);

        HttpResponseMessage Salvar(TDto model);

        HttpResponseMessage Atualizar(TDto model);

        HttpResponseMessage Remover(long id);

        HttpResponseMessage BuscarPorId(long id);
    }
}