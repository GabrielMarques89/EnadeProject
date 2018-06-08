using EnadeProject.Interfaces;
using EnadeProject.Model.Filter.BaseFilter;

namespace EnadeProject.Model.Filter
{
    public class FiltroEstaticoPergunta : BaseStaticFilter, IFilter
    {
        /// <summary>
        /// Exemplo de campo de filtro customizado.
        /// </summary>
        public string ListDeIds { get; set; }
    }
}