using Dima.Api.Common;
using Dima.Api.Endpoint.Categories;
using Dima.Api.Endpoint.Transactions;

namespace Dima.Api.Endpoint;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/health")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK"});

        endpoints.MapGroup("/v1/categories")
            .WithTags("Categories")
            .MapEndpoints<CreateCategoryEndpoint>()
            .MapEndpoints<UpdateCategoryEndpoint>()
            .MapEndpoints<DeleteCategoryEndpoint>()
            .MapEndpoints<GetCategoryByIdEndpoint>()
            .MapEndpoints<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("/v1/transactions")
            .WithTags("Transactions")
            .MapEndpoints<CreateTransactionEndpoint>()
            .MapEndpoints<UpdateTransactionEndpoint>()
            .MapEndpoints<DeleteTransactionEndpoint>()
            .MapEndpoints<GetTransactionByIdEndpoint>()
            .MapEndpoints<GetTransactionsByPeriodEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoints<TEndpoint>
        (this IEndpointRouteBuilder app) where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}