using System.Security.Claims;
using Dima.Api.Common;
using Dima.Core.Models.Account;

namespace Dima.Api.Endpoint.Identity;

public class GetRolesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapGet("/roles", Handler)
            .RequireAuthorization();

    private static Task<IResult> Handler(ClaimsPrincipal user)
    {
        if (user.Identity?.IsAuthenticated is false || user.Identity is null)
            return Task.FromResult(Results.Unauthorized());
        
        var identity = user.Identity as ClaimsIdentity;
        var roles = identity!
            .FindAll(identity.RoleClaimType)
            .Select(c => new RoleClaim
            {
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType,
            });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}