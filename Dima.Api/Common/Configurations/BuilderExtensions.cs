using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Dima.Api.Common.Configurations;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder
            .Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        ApiConfiguration.StripeApiKey = builder.Configuration.GetValue<string>("StripeApiKey") ?? string.Empty;
        
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {   
            options.CustomSchemaIds(x => x.FullName);
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dima.Api", Version = "v1" });
        });
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        // Add Db 
        builder.Services.AddDbContext<AppDbContext>(op =>
        {
            op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Add ApplicationUser with Identity Core
        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl,
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                ));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        // Add Categories Services
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        // Add Transactions Services 
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
        
    }
    
}