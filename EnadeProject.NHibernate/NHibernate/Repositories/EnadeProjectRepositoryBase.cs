using Abp.Domain.Entities;
using Abp.NHibernate;
using Abp.NHibernate.Repositories;

namespace EnadeProject.NHibernate.Repositories
{
    /// <summary>
    /// Base class for all repositories in this application
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    public class EnadeProjectRepositoryBase<TEntity, TPrimaryKey> : NhRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public EnadeProjectRepositoryBase(ISessionProvider sessionProvider) : base(sessionProvider){}

        //add common methods for all repositories
    }

    /// <summary>
    /// A shortcut of EnadeProjectRepositoryBase for entities with integer Id.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EnadeProjectRepositoryBase<TEntity> : EnadeProjectRepositoryBase<TEntity, long>
        where TEntity : class, IEntity<long>
    {
        protected EnadeProjectRepositoryBase(ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }
    }
}
