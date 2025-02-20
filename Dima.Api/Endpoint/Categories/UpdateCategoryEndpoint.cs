using System.Security.Claims;
using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Categories;

public abstract class UpdateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
          .WithName("Categories: Update")
          .WithSummary("Update a category")
          .WithDescription("Update a category")
          .WithOrder(2)
          .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync
    ( ClaimsPrincipal user 
    , [FromRoute] long id
    , [FromServices]  ICategoryHandler handler 
    , [FromBody] UpdateCategoryRequest request)
    {
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(result);
    }
}