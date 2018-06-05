#region Imports

using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

#endregion

namespace EnadeProject.Model
{
    [AutoMapTo(typeof(EntidadeBase))]
    public abstract class BaseEntityDto : EntityDto<int>, IFullAudited
    {
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
}