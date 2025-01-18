using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Categories;

public abstract class DeleteCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/", HandleAsync)
          .WithName("Categories: Delete")
          .WithSummary("Delete a category")
          .WithDescription("Delete a category")
          .WithOrder(3)
          .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync
    ( [FromServices] ICategoryHandler handler
    , [FromBody] DeleteCategoryRequest request)
    {
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(result);
    }
}