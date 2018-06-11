#region Imports

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

#endregion

namespace EnadeProject.Model
{
    public interface IEntityDto : IEntityDto<long>{}

    [AutoMapTo(typeof(EntidadeBase))]
    public abstract class EntityDto : IEntityDto
    {
        public long Id { get; set; }
        //public DateTime CreationTime { get; set; }
        //public long? CreatorUserId { get; set; }
        //public DateTime? LastModificationTime { get; set; }
        //public long? LastModifierUserId { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime? DeletionTime { get; set; }
        //public long? DeleterUserId { get; set; }
    }
}