namespace Dima.Api.Common;

public interface IEndPoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}