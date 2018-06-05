using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.NHibernate.EntityMappings.Maps
{
    public class PerguntaMap : BaseClassMap<Pergunta>
    {
        public PerguntaMap()
        {
            Table("Pergunta");
            Map(x => x.Conteudo).Column("Conteudo");
            Map(x => x.Dificuldade).Column("Dificuldade");
        }
    }
}