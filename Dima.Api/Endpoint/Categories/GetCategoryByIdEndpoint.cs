using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Categories;

public class GetCategoryByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
          .WithName("Categories: GetById")
          .WithSummary("Get a category by id")
          .WithDescription("Get a category by id")
          .WithOrder(4)
          .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync
        ( [FromServices] ICategoryHandler handler, [FromRoute] long id)
    {
        var request = new GetCategoryByIdRequest(id, "carlos@teste.com");
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}