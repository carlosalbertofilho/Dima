using Dima.Api;
using Dima.Api.Common.Configurations;
using Dima.Api.Endpoint;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.AddDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.AddSecurity();
app.MapEndpoints();
    

app.Run();