#region Região de Imports

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.NHibernate;
using Abp.ObjectMapping;
using EnadeProject.Model;
using EnadeProject.Model.Filter;
using EnadeProject.NHibernate.EntityMappings.Entidades;
using EnadeProject.NHibernate.Repositories;
using NHibernate;
using NHibernate.Util;

#endregion

namespace EnadeProject.Services
{
    public class PerguntaService : EnadeProjectAppServiceBase<Pergunta, PerguntaDto, PerguntaFilter>
    {

        public PerguntaService(IRepository<Pergunta, long> repository,
                               IObjectMapper         objectMapper,ISessionProvider session
            ) : base(
                     repository, objectMapper,session)
        {
            
        }


        public ISessionProvider Sess { get; set; }
        public override Del<IQueryable<Pergunta>, PerguntaFilter> ApplyExtraFilter { get; set; } =
            delegate(IQueryable<Pergunta> set, PerguntaFilter filtro)
            {
                //TODO: Implementações de qualquer filtro presente na classe PerguntaFilter
                return set;
            };

        public override void ValidateLogicBusiness(PerguntaDto model) { }
    }
}