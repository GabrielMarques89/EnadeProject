using System.Collections.Generic;
using EnadeProject.Model.Filter.Support;

namespace EnadeProject.Model.Filter.BaseFilter
{
    public abstract class BaseDynamicFilter<T> where T : EntityDto
    {
        public List<IndividualFilter> Filtros { get; set; }
    }
}
