#region Imports

using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;
using FluentNHibernate.Mapping;

#endregion

namespace EnadeProject.NHibernate.EntityMappings.Maps
{
    public class BaseClassMap<T> : ClassMap<T> where T : EntidadeBase
    {
        public BaseClassMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreationTime).Column("Data_Hora_Criacao").CustomSqlType("timestamp").Not
                .Nullable()
                .Not.Update();
            Map(x => x.CreatorUserId).Column("Id_Criador").Not.Update();
            Map(x => x.LastModificationTime).Column("Data_Hora_Alteracao").CustomSqlType("timestamp")
                .Nullable();
            Map(x => x.LastModifierUserId).Column("Id_modificador");
            Map(x => x.IsDeleted).Column("Fl_Deletado");
            Map(x => x.DeletionTime).Column("Data_Hora_Exclusao").CustomSqlType("timestamp")
                .Nullable();
            Map(x => x.DeleterUserId).Column("Id_Usr_Delete");
        }
    }
}