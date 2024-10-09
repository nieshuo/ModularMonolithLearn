using Evently.Modules.Events.Application.Abstractions.Data;
using Npgsql;
using System.Data.Common;

namespace Evently.Modules.Events.Infrastructure.Data
{
    public sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
    {
        public async ValueTask<DbConnection> OpenConnectionAsync()
        {
            return await dataSource.OpenConnectionAsync();
        }
    }
}
