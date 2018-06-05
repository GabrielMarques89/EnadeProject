//#region Imports

//using System;
//using System.Collections.Generic;
//using Abp.Domain.Entities.Auditing;
//using TopicosEspeciaisDEV.ModelosDeNegocio.Interfaces;

//#endregion

//namespace TopicosEspeciaisDEV.ModelosDeNegocio.EntidadeBase
//{
//    /// <summary>
//    ///     Entity é a entidade base do APB. Herda de IEntity(Tipo). Usando herança do Entity, há uma criação default de
//    ///     repositório para as classes que corretamente implementam a interface.
//    /// </summary>
//    /// <typeparam name="TKeyType"></typeparam>
//    public abstract class Entity<TKeyType> : IEntidadeBase<TKeyType>, IFullAudited
//    {
//        /// <summary>
//        ///     O framework se encarrega de definir o equals baseado em Id
//        /// </summary>
//        public virtual TKeyType Id { get; set; }
//        /// <summary>
//        ///     <para>Método default que as classes devem implementar para validação de regras gerais.</para>
//        ///     <para>Validação baseada nos [Data Annotations] da classe.</para>
//        /// </summary>
//        /// <returns></returns>
//        public virtual List<ValidationResult> Validate()
//        {
//            var context = new ValidationContext(this, null, null);
//            var results = new List<ValidationResult>();

//            Validator.TryValidateObject(this, context, results, true);

//            return results;
//        }

//        public abstract string ChaveCandidata();
//        public abstract bool IsTransient();

//        /// <summary>
//        ///     O framework se encarrega de controlar o creationTime
//        /// </summary>
//        public virtual DateTime CreationTime { get; set; }

//        public virtual long? CreatorUserId { get; set; }
//        public virtual DateTime? LastModificationTime { get; set; }
//        public virtual long? LastModifierUserId { get; set; }
//        public virtual bool IsDeleted { get; set; }
//        public virtual DateTime? DeletionTime { get; set; }
//        public virtual long? DeleterUserId { get; set; }
//    }

//    /// <summary>
//    ///     É preciso criar uma entidade base sem tipo para que não ser necessário replicar o tipo de entidade base para todas
//    ///     entidades
//    /// </summary>
//    public abstract class EntidadeBase : Entity<int>
//    {
//    }
//}