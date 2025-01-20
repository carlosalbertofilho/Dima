namespace Dima.Api.Common.Configurations;

public static class AppExtensions
{
    public static void AddDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dima.Api v1");
            options.RoutePrefix = string.Empty;
        });
        app.MapSwagger().RequireAuthorization();
    }
    
    public static void AddSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}