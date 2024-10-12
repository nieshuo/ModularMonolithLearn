using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Categories.GetCategory
{
    internal sealed class GetCategoryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCategoryQuery, CategoryResponse>
    {
        public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
             SELECT
                 "Id" AS {nameof(CategoryResponse.Id)},
                 "Name" AS {nameof(CategoryResponse.Name)},
                 "IsArchived" AS {nameof(CategoryResponse.IsArchived)}
             FROM events."Categories"
             WHERE "Id" = @CategoryId
             """;

            CategoryResponse? category = await connection.QuerySingleOrDefaultAsync<CategoryResponse>(sql, request);

            if (category is null)
            {
                return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(request.CategoryId));
            }

            return category;
        }
    }

}
