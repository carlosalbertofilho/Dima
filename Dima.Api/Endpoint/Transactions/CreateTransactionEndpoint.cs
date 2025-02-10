using System.Security.Claims;
using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Transactions;

public abstract class CreateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
              .WithName("Transactions: Create")
              .WithSummary("Create a new transaction")
              .WithDescription("Create a new transaction")
              .WithOrder(1)
              .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync
    ( ClaimsPrincipal user
    , [FromServices] ITransactionHandler handler
    , [FromBody] CreateTransactionRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}