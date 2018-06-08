using System;
using System.Collections.Generic;
using EnadeProject.Interfaces;
using EnadeProject.Model.Filter.Support;

namespace EnadeProject.Model.Filter.BaseFilter
{
    public abstract class BaseStaticFilter : IFilter
    {
        public List<IndividualFilter> Set { get; set; }
    }
}
