using FluentMigrator.Runner.VersionTableInfo;

namespace EnadeProject.DbMigrations
{
    [VersionTableMetaData]
    public class VersionTable : DefaultVersionTableMetaData
    {
        public override string TableName
        {
            get
            {
                return "MyAppVersionInfo";
            }
        }
    }
}
