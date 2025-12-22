using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MyRecipeBook.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableWithColumnOrSchemaOrDescriptionSyntax CreateTable(string table)
        {
            return (ICreateTableWithColumnOrSchemaOrDescriptionSyntax) Create.Table(table)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable();
        }
    }
}
