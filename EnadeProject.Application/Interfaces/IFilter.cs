using System.Collections.Generic;
using Abp.Application.Services.Dto;
using EnadeProject.Model.Filter.Support;

namespace EnadeProject.Interfaces
{
    public interface IFilter
    {
        List<IndividualFilter> Set { get; set; }
        PagedAndSortedResultRequestDto PageAndSort { get; set; }

    }
}
