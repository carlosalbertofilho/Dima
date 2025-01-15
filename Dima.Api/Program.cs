using Dima.Api.Data;
using Dima.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Db 
builder.Services.AddDbContext<AppDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName);
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dima.Api", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dima.Api v1");
    options.RoutePrefix = string.Empty;
});

// Endpoints
app.MapPost
    ("/v1/transactions"
    , (Resquest request) =>  new Handler().Handle(request))
    .WithName("Transaction: Create")
    .WithSummary("Cria uma nova transação")
    .Produces<Response>();

app.Run();

// Request
public record Resquest
    ( long Id
    , string Title
    , string UserId
    , ETransactionType Type
    , decimal Amount
    , long CategoryId)
{ }

// response
public record Response
    ( long Id
    , string Title
    , string UserId
    , ETransactionType Type
    , decimal Amount
    , long CategoryId
    , DateTime PaidOrReceivedAt
    , DateTime CreatedAt)
{}
    
// handle
public class Handler()
{
    public Response Handle(Resquest request)
    => new ( request.Id
        , request.Title
        , request.UserId
        , request.Type
        , request.Amount
        , request.CategoryId
        , DateTime.Now
        , DateTime.Now);
}