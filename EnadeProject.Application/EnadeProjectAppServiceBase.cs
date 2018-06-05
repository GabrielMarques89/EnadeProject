#region Imports

using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EnadeProject.Interfaces;
using EnadeProject.Model;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

#endregion

namespace EnadeProject
{
    /// <inheritdoc cref="ApplicationService" />
    /// <summary>
    ///     Derive your application services from this class.
    /// </summary>
    public abstract class EnadeProjectAppServiceBase<THerdaEntidadeBase> : ApplicationService, IService,
        ICrudAppService<BaseEntityDto> where THerdaEntidadeBase : EntidadeBase
    {
        protected readonly IRepository<THerdaEntidadeBase> Repository;

        protected EnadeProjectAppServiceBase(IRepository<THerdaEntidadeBase> repository)
        {
            Repository = repository;
            LocalizationSourceName = EnadeProjectConsts.LocalizationSourceName;
        }

        public BaseEntityDto Get(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        public BaseEntityDto Get()
        {
            throw new NotImplementedException();
        }

        public PagedResultDto<BaseEntityDto> GetAll(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public BaseEntityDto Create(BaseEntityDto input)
        {
            throw new NotImplementedException();
        }

        public BaseEntityDto Update(BaseEntityDto input)
        {
            throw new NotImplementedException();
        }

        public void Delete(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        public virtual void BusinessRulesValidation()
        {
            throw new NotImplementedException();
        }

        public virtual void EntityValidation()
        {
            throw new NotImplementedException();
        }
    }
}