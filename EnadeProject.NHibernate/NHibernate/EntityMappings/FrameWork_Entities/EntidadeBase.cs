#region Imports

#endregion

namespace EnadeProject.NHibernate.EntityMappings.FrameWork_Entities
{
    /// <summary>
    ///     É preciso criar uma entidade base sem tipo para que não ser necessário replicar o tipo de entidade base para todas
    ///     entidades. Desta Maneira, alinhamos os tipos de dados dos identificadores burros (PK sequencial).
    /// </summary>
    public abstract class EntidadeBase : Entity{}
}