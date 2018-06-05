#region Imports

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

#endregion

namespace EnadeProject.NHibernate.EntityMappings.FrameWork_Entities.Interfaces
{
    public interface IEntidadeBase<TKeyType> : IEntity<TKeyType>

    {
        List<ValidationResult> Validate();
        string ChaveCandidata();
    }
}