#region Imports

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

#endregion

namespace EnadeProject.NHibernate.EntityMappings.FrameWork_Entities.Interfaces
{
    public interface IEntidadeBase : IEntity<long>,IFullAudited

    {
        List<ValidationResult> Validate();
        string ChaveCandidata();
    }
}