using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Transactions;

public abstract class DeleteTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/", HandleAsync)
              .WithName("Transactions: Delete")
              .WithSummary("Delete a transaction")
              .WithDescription("Delete a transaction")
              .WithOrder(3)
              .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync
    ( [FromBody] DeleteTransactionRequest request
    , [FromServices] ITransactionHandler handler)
    {
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(result);
    }
}