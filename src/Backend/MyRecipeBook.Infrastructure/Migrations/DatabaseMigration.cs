using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {
        public static void Migrate(string connectionString) {
            EnsureDatabaseCreated_SQLServer(connectionString);
        }

        private static void EnsureDatabaseCreated_SQLServer(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);
            if (records.Any() == false) 
                dbConnection.Execute($"CREATE DATABASE {databaseName}");

        }
    }
}
