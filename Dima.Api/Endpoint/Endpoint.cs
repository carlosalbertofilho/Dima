using Dima.Api.Common;
using Dima.Api.Endpoint.Categories;

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
    }

    private static IEndpointRouteBuilder MapEndpoints<TEndpoint>
        (this IEndpointRouteBuilder app) where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}