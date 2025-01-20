using System.Security.Claims;
using Dima.Api.Common;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoint.Transactions;

public abstract class GetTransactionsByPeriodEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
              .WithName("Transactions: GetByPeriod")
              .WithSummary("Get transactions by period")
              .WithDescription("Get transactions by period")
              .WithOrder(5)
              .Produces<Response<List<Transaction>>>();

    private static async Task<IResult> HandleAsync
        ( [FromServices] ITransactionHandler handler
        , [FromServices] ClaimsPrincipal user    
        , [FromQuery] DateTime? startDate = null
        , [FromQuery] DateTime? endDate = null
        , [FromQuery] int pageNumber = Configuration.DefaultPageNumber
        , [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionsByPeriodRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };
        
        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}