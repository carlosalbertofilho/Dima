using System.Security.Claims;
using Azure;
using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Categories;

public abstract class CreateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Create a new category")
            .WithDescription("Create a new category")
            .WithOrder(1)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync
    ( ClaimsPrincipal user
    , [FromServices] ICategoryHandler handler    
    , [FromBody] CreateCategoryRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
    
}