using System.Security.Claims;
using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Categories;

public abstract class GetAllCategoriesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
          .WithName("Categories: GetAll")
          .WithSummary("Get all categories")
          .WithDescription("Get all categories")
          .WithOrder(5)
          .Produces<Response<List<Category>>>();

    private static async Task<IResult> HandleAsync
    ( [FromServices] ICategoryHandler handler
    , [FromServices] ClaimsPrincipal user    
    , [FromQuery] int pageNumber = Configuration.DefaultPageNumber
    , [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = user.Identity?.Name ?? string.Empty
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}