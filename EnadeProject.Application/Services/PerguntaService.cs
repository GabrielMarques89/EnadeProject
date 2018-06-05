#region Imports

using Abp.Domain.Repositories;
using EnadeProject.NHibernate.EntityMappings.Entidades;

#endregion

namespace EnadeProject.Services
{
    public class PerguntaService : EnadeProjectAppServiceBase<Pergunta>
    {
        /// <summary>
        ///     <para>Construtor default</para>
        ///     <para>Os parâmetros do construtor default são injetados por dependência pelo framework</para>
        /// </summary>
        /// <param name="repository"></param>
        public PerguntaService(IRepository<Pergunta> repository) : base(repository)
        {
        }
    }
}