using System.Security.Claims;
using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Transactions;

public abstract class GetTransactionByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
              .WithName("Transactions: GetById")
              .WithSummary("Get a transaction by id")
              .WithDescription("Get a transaction by id")
              .WithOrder(4)
              .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync
    ( [FromRoute] long id
    , [FromServices] ClaimsPrincipal user    
    , [FromServices] ITransactionHandler handler)
    {
        var request = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };    
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);
    }
}