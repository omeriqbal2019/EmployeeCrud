using Employee.DBCore.Context.EFContext;
using Employee.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;

namespace Employee.DBCore.Factory
{
    public class ContextFactory : IContextFactory
    {
        private readonly IOptions<ConnectionSettings> _connectionOptions;

        public ContextFactory(IOptions<ConnectionSettings> connectionOptions)
        {
            _connectionOptions = connectionOptions;
        }

        public ContextFactory(string connectionString)
        {
            var connectionSettings = new ConnectionSettings();
            connectionSettings.DefaultConnection = connectionString;

            _connectionOptions = Options.Create<ConnectionSettings>(connectionSettings);
        }

        public IDatabaseContext DbContext => new DatabaseContext(GetDataContext().Options);



        private DbContextOptionsBuilder<DatabaseContext> GetDataContext()
        {
            ValidateDefaultConnection();

            var sqlConnectionBuilder = new SqlConnectionStringBuilder(_connectionOptions.Value.DefaultConnection);

            var contextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            contextOptionsBuilder.UseSqlServer(sqlConnectionBuilder.ConnectionString);

            return contextOptionsBuilder;
        }

        private void ValidateDefaultConnection()
        {
            if (string.IsNullOrEmpty(_connectionOptions.Value.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(_connectionOptions.Value.DefaultConnection));
            }
        }
    }
}
