using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Transactions;

public abstract class UpdateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/", HandleAsync)
              .WithName("Transactions: Update")
              .WithSummary("Update a transaction")
              .WithDescription("Update a transaction")
              .WithOrder(2)
              .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync
    ( [FromBody] UpdateTransactionRequest request
    , [FromServices] ITransactionHandler handler)
    {
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(result);
    }
}