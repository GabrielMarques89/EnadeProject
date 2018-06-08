using System.Collections.Generic;
using EnadeProject.Model.Filter.Support;

namespace EnadeProject.Interfaces
{
    public interface IFilter
    {
        List<IndividualFilter> Set { get; set; }
    }
}
