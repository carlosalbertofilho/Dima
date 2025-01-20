using Dima.Api.Common;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Endpoint.Identity;

public class LogoutEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost("/logout", HandlerAsync)
            .RequireAuthorization();

    private static async Task<IResult> HandlerAsync(SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}