#region Imports

using System;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

#endregion

namespace EnadeProject.NHibernate.EntityMappings.Entidades
{
    public class Pergunta : EntidadeBase
    {
        public virtual string Conteudo { get; set; }
        public virtual decimal Dificuldade { get; set; }

        public override string ChaveCandidata()
        {
            throw new NotImplementedException();
        }

        public override bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}