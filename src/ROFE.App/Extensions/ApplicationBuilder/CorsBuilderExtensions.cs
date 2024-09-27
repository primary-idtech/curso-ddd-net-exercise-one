using Microsoft.AspNetCore.Builder;

namespace ROFE.App.Extensions.ApplicationBuilder;

public static class CorsBuilderExtensions
{
    public static void UseCorsExtension(this IApplicationBuilder app)
    {
        app.UseCors("CorsPolicy");
    }
}
