#region Região de Imports

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using EnadeProject.Common.Helpers;
using EnadeProject.Interfaces;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities.Interfaces;
using IEntityDto = EnadeProject.Model.IEntityDto;

#endregion

namespace EnadeProject
{
    /// <inheritdoc cref="ApplicationService" />
    /// <summary>
    ///     Contém métodos genéricos para comportamentos recorrentes. Além disso, força a implementação de métodos de
    ///     customização.
    /// </summary>
    public abstract class EnadeProjectAppServiceBase<TEntity, TEntityDto, TFilter>
        : ApplicationService,
          IService<TEntity, TEntityDto, TFilter>
        where TEntity : EntidadeBase
        where TEntityDto : IEntityDto
        where TFilter : IFilter
    {
        public delegate IQueryable<TEntity> Del<in TIqueryble, in T>(TIqueryble set, T item)
            where TIqueryble : IQueryable<TEntity> where T : TFilter;

        // ReSharper disable once NotAccessedField.Local
        private readonly   IObjectMapper              _objectMapper;
        protected readonly IRepository<TEntity, long> Repository;

        protected EnadeProjectAppServiceBase(IRepository<TEntity, long> repository,
                                             IObjectMapper              objectMapper)
        {
            Repository             = repository;
            _objectMapper          = objectMapper;
            LocalizationSourceName = EnadeProjectConsts.LocalizationSourceName;
        }

        // ReSharper disable once NotAccessedField.Local -- Classe base não usa diretamente
        protected ExpressionHelper ExpressionHelper { get; } = new ExpressionHelper();

        public abstract Del<IQueryable<TEntity>, TFilter> ApplyExtraFilter { get; set; }

        public TEntityDto Get(EntityDto<long> input)
        {
            return Get(input.Id);
        }

        public PagedResultDto<TEntityDto> GetAll(PagedAndSortedResultRequestDto input)
        {
            return ApplyPagination(Repository.GetAll(), input);
        }

        public TEntityDto Get(long input)
        {
            var result = ObjectMapper.Map(Repository.Single(x => x.Id == input),
                                          Activator.CreateInstance<TEntityDto>());
            return result;
        }

        /// <summary>
        ///     Implementação particular de um service sobre a aplicação de filtros.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>

        //public abstract IQueryable<THerdaEntidadeBase> ApplyFilter<T>(T filtro) where T : BaseStaticFilter<TEntityDto>;
        public IQueryable<TEntity> ApplyFilter(TFilter filtro)
        {
            var set = Repository.GetAll();
            if (filtro.Set.Count >= 1)
                foreach (var filter in filtro.Set)
                {
                    var property       = typeof(TEntity).GetProperties().Single(x => x.Name == filter.Campo);
                    var convertedValue = Convert.ChangeType(filter.Value, property.PropertyType);
                    var lambda =
                        ExpressionHelper.GenerateLambdaOperationExpression<TEntity>(property, convertedValue,
                                                                                    filter.Criteria);
                    set = set.Where(lambda);
                }

            if (ApplyExtraFilter != null) set = ApplyExtraFilter(set, filtro);

            return set;
        }

        public TEntityDto Create(TEntityDto input)
        {
            var model = ObjectMapper.Map(input, Activator.CreateInstance<TEntity>());
            return ObjectMapper.Map(Repository.Insert(model), input);
        }

        public TEntityDto Update(TEntityDto input)
        {
            var model = ObjectMapper.Map(input, Activator.CreateInstance<TEntity>());
            return ObjectMapper.Map(Repository.Update(model), input);
        }

        public void Delete(EntityDto<long> input)
        {
            Delete(input.Id);
        }

        /// <summary>
        ///     Implementação de filtros genérica. Utiliza a implementação particular do método <see cref="ApplyExtraFilter" />>
        ///     pelo serviço que implementa uma entidade.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public PagedResultDto<TEntityDto> GetAndFilter(TFilter filtro)
        {
            var filteredQuery = ApplyFilter(filtro);
            return ApplyPagination(filteredQuery, filtro.PageAndSort);
        }

        public void Delete(long input)
        {
            Repository.Delete(input);
        }

        public PagedResultDto<TEntityDto> ApplyPagination(IQueryable<IEntidadeBase>      set,
                                                          PagedAndSortedResultRequestDto input)
        {
            var result = set.PageBy(input).ToList();

            var secondResult = result.AsQueryable().Select(x =>
                                                               ObjectMapper
                                                                   .Map(x, Activator.CreateInstance<TEntityDto>()))
                                     .ToList();

            return new PagedResultDto<TEntityDto>
                   {
                       TotalCount = DynamicQueryableExtensions.Count(set),
                       Items      = secondResult
                   };
        }
    }
}