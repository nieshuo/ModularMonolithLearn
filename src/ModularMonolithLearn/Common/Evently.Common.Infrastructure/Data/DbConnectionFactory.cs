using Evently.Common.Application.Data;
using Npgsql;
using System.Data.Common;

namespace Evently.Common.Infrastructure.Data
{
    public sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
    {
        public async ValueTask<DbConnection> OpenConnectionAsync()
        {
            return await dataSource.OpenConnectionAsync();
        }
    }
}
