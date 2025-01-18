using Dima.Api.Data;
using Dima.Api.Endpoint;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
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
builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dima.Api v1");
    options.RoutePrefix = string.Empty;
});

// Endpoints
app.MapEndpoints();
    

app.Run();