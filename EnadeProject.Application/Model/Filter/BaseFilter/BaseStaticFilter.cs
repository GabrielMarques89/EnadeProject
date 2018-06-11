using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using EnadeProject.Interfaces;
using EnadeProject.Model.Filter.Support;

namespace EnadeProject.Model.Filter.BaseFilter
{
    [DisableValidation]
    public abstract class BaseStaticFilter : IFilter
    {
        public List<IndividualFilter> Set { get; set; }
        public PagedAndSortedResultRequestDto PageAndSort { get; set; }
    }
}
