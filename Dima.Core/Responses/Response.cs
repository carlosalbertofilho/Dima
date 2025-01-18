using System.Text.Json.Serialization;
using Dima.Api;

namespace Dima.Core.Responses;

[method: JsonConstructor] public record Response<T>
    ( T? Data
    , int Code = Configuration.DefaultStatusCode
    , string? Message = null)
{
    [JsonIgnore] public bool IsSuccess => Code is >= 200 and < 300;

    public Response() : this (default, 200) {}
}

public record CreatedResponse<T>(T? Data, string? message = null) 
    : Response<T>(Data, Configuration.DefaultStatusCodeCreated, message);

public record NoContentResponse<T>(string message, T? Data = default)
    : Response<T>(Data, Configuration.DefaultStatusNoContent, message);

public record InternalServerErrorResponse<T>(string message, T? Data = default)
    : Response<T>(Data, Configuration.DefaultStatusCodeError, message);
public record NotFoundResponse<T>(string message)
: Response<T>(default, Configuration.DefaultStatusCodeNotFound, message);