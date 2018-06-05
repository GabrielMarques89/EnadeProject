#region Imports

using Abp.Domain.Repositories;
using EnadeProject.NHibernate.EntityMappings.Entidades;

#endregion

namespace EnadeProject.Services
{
    public class RespostaService : EnadeProjectAppServiceBase<Resposta>
    {
        /// <summary>
        ///     <para>Construtor default</para>
        ///     <para>Os parâmetros do construtor default são injetados por dependência pelo framework</para>
        /// </summary>
        /// <param name="repository"></param>
        public RespostaService(IRepository<Resposta> repository) : base(repository)
        {
        }
    }
}