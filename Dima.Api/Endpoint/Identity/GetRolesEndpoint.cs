using System.Security.Claims;
using Dima.Api.Common;

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
            .Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}