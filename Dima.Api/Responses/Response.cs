using System.Text.Json.Serialization;

namespace Dima.Api.Responses;

public record Response<T>
    ( T? Data
    , int Code = Configuration.DefaultStatusCode
    , string? Message = null)
{
    [JsonIgnore] public bool IsSuccess => Code is >= 200 and < 300;

    [JsonConstructor] public Response() : 
        this (default, 200) {}
}