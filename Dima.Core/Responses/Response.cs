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
