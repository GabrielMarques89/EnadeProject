using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Runtime.Validation;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities.Interfaces;
using FluentNHibernate.Conventions;
using FluentNHibernate.Visitors;

namespace EnadeProject.NHibernate.EntityMappings.FrameWork_Entities
{
    /// <summary>
    ///     Entity é a entidade base do APB. Herda de IEntity(Tipo). Usando herança do Entity, há uma criação default de
    ///     repositório para as classes que corretamente implementam a interface.
    /// </summary>
    public abstract class Entity : IEntidadeBase
    {
        /// <summary>
        ///     O framework se encarrega de definir o equals baseado em Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        ///     <para>Método default que as classes devem implementar para validação de regras gerais.</para>
        ///     <para>Validação baseada nos [Data Annotations] da classe.</para>
        /// </summary>
        /// <returns></returns>
        public virtual List<ValidationResult> Validate()
        {
            var context = new ValidationContext(this, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(this,context,results,true);

            if (results.Count > 0)
            {
                throw new AbpValidationException("Erro de validação.", results);
            }
            return results;
        }

        public abstract string ChaveCandidata();
        public abstract bool IsTransient();

        /// <summary>
        ///     O framework se encarrega de controlar o creationTime
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? DeleterUserId { get; set; }
    }
}