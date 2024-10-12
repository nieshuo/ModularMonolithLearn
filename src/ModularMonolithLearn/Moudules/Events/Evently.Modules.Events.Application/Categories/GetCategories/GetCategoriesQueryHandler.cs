using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Application.Categories.GetCategory;
using Evently.Common.Domain;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Categories.GetCategories
{
    internal sealed class GetCategoriesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCategoriesQuery, IReadOnlyCollection<CategoryResponse>>
    {
        public async Task<Result<IReadOnlyCollection<CategoryResponse>>> Handle(
            GetCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
             SELECT
                 "Id" AS {nameof(CategoryResponse.Id)},
                 "Name" AS {nameof(CategoryResponse.Name)},
                 "IsArchived" AS {nameof(CategoryResponse.IsArchived)}
             FROM events."Categories"
             """;

            List<CategoryResponse> categories = (await connection.QueryAsync<CategoryResponse>(sql, request)).AsList();

            return categories;
        }
    }
}
