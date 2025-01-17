using Azure;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
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

// Add CategoryHandler
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

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
    , ( [FromBody] CreateCategoryRequest request
      , [FromServices] ICategoryHandler handler) =>  handler.CreateAsync(request))
    .WithName("Category: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();

app.MapPut
    ("/v1/transactions"
    , ( [FromBody] UpdateCategoryRequest request
      , [FromServices] ICategoryHandler handler) => handler.UpdateAsync(request))
    .WithName("Category: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category>>();

app.MapDelete
    ("/v1/transactions"
    , ( [FromBody] DeleteCategoryRequest request
      , [FromServices] ICategoryHandler handler) => handler.DeleteAsync(request))
    .WithName("Category: Delete")
    .WithSummary("Deleta uma categoria")
    .Produces<Response<Category>>();

app.MapGet
    ("/v1/transactions/"
    , ( [FromBody] GetCategoryByIdRequest request
      , [FromServices] ICategoryHandler handler) => handler.GetByIdAsync(request))
    .WithName("Category: GetById")
    .WithSummary("Busca uma categoria por id")
    .Produces<Response<Category>>();
    

app.Run();