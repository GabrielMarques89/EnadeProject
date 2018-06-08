using Abp.Domain.Repositories;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

namespace EnadeProject.NHibernate.Repositories.Interface
{
    public interface IRepository<T> : IRepository<T,long> where T : EntidadeBase
    {}
}
