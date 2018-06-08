#region Imports

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
using EnadeProject.Model;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

#endregion

namespace EnadeProject
{
    /// <inheritdoc cref="ApplicationService" />
    /// <summary>
    ///     Contém métodos genéricos para comportamentos recorrentes. Além disso, força a implementação de métodos de customização.
    /// </summary>
    public abstract class EnadeProjectAppServiceBase<THerdaEntidadeBase, TEntityDto, TFilter>
        : ApplicationService, IService<THerdaEntidadeBase, TEntityDto, TFilter>
        where THerdaEntidadeBase : EntidadeBase
        where TEntityDto : BaseEntityDto
        where TFilter : IFilter

    {
        public delegate IQueryable<THerdaEntidadeBase> Del<in TIqueryble, in T>(TIqueryble set, T item)
            where TIqueryble : IQueryable<THerdaEntidadeBase> where T : TFilter;

        // ReSharper disable once NotAccessedField.Local -- Classe base não usa diretamente
        private ExpressionHelper ExpressionHelper { get; set; } = new ExpressionHelper();
        private readonly IObjectMapper _objectMapper;
        protected readonly IRepository<THerdaEntidadeBase,long> Repository;

        protected EnadeProjectAppServiceBase(IRepository<THerdaEntidadeBase,long> repository,
            IObjectMapper objectMapper)
        {
            Repository = repository;
            _objectMapper = objectMapper;
            LocalizationSourceName = EnadeProjectConsts.LocalizationSourceName;
        }

        public abstract Del<IQueryable<THerdaEntidadeBase>, TFilter> ApplyExtraFilter { get; set; }

        public TEntityDto Get(EntityDto<long> input)
        {
            var result = ObjectMapper.Map(Repository.Single(x => x.Id == input.Id), input);
            return (TEntityDto) result;
        }

        public PagedResultDto<TEntityDto> GetAll(PagedAndSortedResultRequestDto input)
        {
            return CreatePagedResult(Repository.GetAll(), input);
        }

        public TEntityDto Create(TEntityDto input)
        {
            var model = ObjectMapper.Map(input, Activator.CreateInstance<THerdaEntidadeBase>());
            return ObjectMapper.Map(Repository.Insert(model), input);
        }

        public TEntityDto Update(TEntityDto input)
        {
            var model = ObjectMapper.Map(input, Activator.CreateInstance<THerdaEntidadeBase>());
            return ObjectMapper.Map(Repository.Update(model), input);
        }

        public void Delete(EntityDto<long> input)
        {
            Repository.Delete(input.Id);
        }

        /// <summary>
        ///     Implementação particular de um service sobre a aplicação de filtros.
        /// </summary>
        /// <typeparam name="TFiltro"></typeparam>
        /// <param name="filtro"></param>
        /// <returns></returns>
        //public abstract IQueryable<THerdaEntidadeBase> Filter<T>(T filtro) where T : BaseStaticFilter<TEntityDto>;
        public IQueryable<THerdaEntidadeBase> Filter<TFiltro>(TFiltro filtro) where TFiltro : TFilter
        {
            var set = Repository.GetAll();
            if (filtro.Set.Count >= 1)
                foreach (var filter in filtro.Set)
                {
                    var property = typeof(TEntityDto).GetProperties().Single(x => x.Name == filter.Campo);
                    var convertedValue = Convert.ChangeType(filter.Value, property.PropertyType);
                    var lambda = ExpressionHelper.GenerateLambdaOperationExpression<THerdaEntidadeBase>(property, convertedValue, filter.Criteria);
                    set = set.Where(lambda);
                }

            if (ApplyExtraFilter != null)
            {
                set = ApplyExtraFilter(set, filtro);
            }

            return set;
        }

        /// <summary>
        ///     Implementação de filtros genérica. Utiliza a implementação particular do método <see cref="Filter{TFiltro}" />>
        ///     pelo serviço que implementa uma entidade.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filtro"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<TEntityDto> GetAndFilter<T>(T filtro, PagedAndSortedResultRequestDto input)
            where T : TFilter
        {
            return CreatePagedResult(Filter(filtro), input);
        }

        public PagedResultDto<TEntityDto> CreatePagedResult(IQueryable<THerdaEntidadeBase> set,
            PagedAndSortedResultRequestDto input)
        {
            var result = set.PageBy(input).ToList();

            var secondResult = result.AsQueryable().Select(x =>
                ObjectMapper.Map(x, Activator.CreateInstance<TEntityDto>())).ToList();

            return new PagedResultDto<TEntityDto>
            {
                TotalCount = DynamicQueryableExtensions.Count(set),
                Items = secondResult
            };
        }
    }
}