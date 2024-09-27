using Microsoft.AspNetCore.Builder;
using ROFE.App.Extensions.ApplicationBuilder;
using ROFE.Infrastructure.Configurations;

namespace ROFE.App.Extensions;

public static class ApplicationBuilderExtension
{
    public static void Configure(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseCorsExtension();
        app.UseHealthChecksExtension();
        app.UseSwaggerExtension();

        app.UseExceptionHandler(errorPipeline =>
        {
            errorPipeline.UseExceptionHandlerMiddleware(AppSettingConfig.IncludeErrorDetailInResponse);
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });
    }
}
