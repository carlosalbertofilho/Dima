using Dima.Api.Common;
using Dima.Api.Endpoint.Categories;
using Dima.Api.Endpoint.Identity;
using Dima.Api.Endpoint.Reports;
using Dima.Api.Endpoint.Transactions;
using Dima.Api.Models;

namespace Dima.Api.Endpoint;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/health")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK"});

        endpoints.MapGroup("/v1/users")
            .WithTags("Identity")
            .MapIdentityApi<ApplicationUser>();

        endpoints.MapGroup("/v1/users")
            .WithTags("Identity")
            .MapEndpoints<LogoutEndpoint>()
            .MapEndpoints<GetRolesEndpoint>();

        endpoints.MapGroup("/v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoints<CreateCategoryEndpoint>()
            .MapEndpoints<UpdateCategoryEndpoint>()
            .MapEndpoints<DeleteCategoryEndpoint>()
            .MapEndpoints<GetCategoryByIdEndpoint>()
            .MapEndpoints<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("/v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapEndpoints<CreateTransactionEndpoint>()
            .MapEndpoints<UpdateTransactionEndpoint>()
            .MapEndpoints<DeleteTransactionEndpoint>()
            .MapEndpoints<GetTransactionByIdEndpoint>()
            .MapEndpoints<GetTransactionsByPeriodEndpoint>();

        endpoints.MapGroup("/v1/reports")
            .WithTags("Reports")
            .RequireAuthorization()
            .MapEndpoints<GetIncomesAndExpensesEndpoint>()
            .MapEndpoints<GetIncomesByCategoryEndpoint>()
            .MapEndpoints<GetExpensesByCategoryEndpoint>()
            .MapEndpoints<GetFinancialSummaryEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoints<TEndpoint>
        (this IEndpointRouteBuilder app) where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}