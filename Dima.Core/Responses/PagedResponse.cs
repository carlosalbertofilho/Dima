using System.Text.Json.Serialization;
using Dima.Api;

namespace Dima.Core.Responses;

[method: JsonConstructor] public record PagedResponse<T>
    ( int TotalCount
    , int PageSize = Configuration.DefaultPageSize    
    , int CurrentPage = Configuration.DefaultPageNumber
    , T? Data = default
    , int Code = Configuration.DefaultStatusCode
    , string? Message = null)
    : Response<T>( Data, Code, Message)
{
    public int TotalPages => (int) Math.Ceiling( TotalCount / (double) PageSize );

    public PagedResponse() : this ( 1 ) { }
}