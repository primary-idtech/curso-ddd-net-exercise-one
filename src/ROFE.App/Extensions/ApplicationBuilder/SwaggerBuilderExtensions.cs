using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ROFE.App.Extensions.ApplicationBuilder;

public static class SwaggerBuilderExtensions
{
    public static void UseSwaggerExtension(this IApplicationBuilder app)
    {            
        app.UseSwagger(c =>
        {
            //Fill the server url based on each request (host) 
            c.PreSerializeFilters.Add(ReplaceWithHostedServerUrl);
        });
        app.UseSwaggerUI(c =>
        {
            //Build a swagger endpoint
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
            c.SupportedSubmitMethods(SubmitMethod.Head, SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Patch, SubmitMethod.Delete);
            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.EnableValidator();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            c.RoutePrefix = "swagger";
        });

    }
    
    private static void ReplaceWithHostedServerUrl(OpenApiDocument swagger, HttpRequest httpReq)
    {
        swagger.Servers = [new() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" }];
    }
}
