using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Categories;

public class UpdateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/", HandleAsync)
          .WithName("Categories: Update")
          .WithSummary("Update a category")
          .WithDescription("Update a category")
          .WithOrder(2)
          .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync
    ( [FromServices] ICategoryHandler handler
    , [FromBody] UpdateCategoryRequest request)
    {
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(result);
    }
}