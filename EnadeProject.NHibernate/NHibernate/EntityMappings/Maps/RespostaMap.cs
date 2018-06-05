using EnadeProject.NHibernate.EntityMappings.Entidades;

namespace EnadeProject.NHibernate.EntityMappings.Maps
{
    public class RespostaMap : BaseClassMap<Resposta>
    {
        public RespostaMap()
        {
            Table("Resposta");
            Map(x => x.Conteudo).Column("Conteudo");
            Map(x => x.Correta).Column("Correta");
            References(x => x.Pergunta).Column("ID_PERGUNTA").ForeignKey("FK_PERGUNTA").Not.Nullable();
        }
    }
}