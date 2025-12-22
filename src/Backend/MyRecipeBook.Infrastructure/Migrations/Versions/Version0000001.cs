using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_USER, "Create TABLE to save the User Information.")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Users")
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Email").AsString(150).NotNullable()
                .WithColumn("Password").AsString(256).NotNullable();
        }
    }
}
