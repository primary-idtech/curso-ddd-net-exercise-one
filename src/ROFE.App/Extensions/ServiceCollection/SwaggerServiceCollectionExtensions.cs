using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;

namespace ROFE.App.Extensions.ServiceCollection;

public static class SwaggerServiceCollectionExtensions
{
    public static void AddSwaggerGenExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var appName = GetApplicationName();
            var solutionName = appName?.Split(".").FirstOrDefault();
            var xmlFile = $"{solutionName}.Presentation.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = "v1" });

            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });
    }

    private static string GetApplicationName()
    {
        return (System.Reflection.Assembly.GetEntryAssembly() ?? System.Reflection.Assembly.GetExecutingAssembly()).GetName().Name;
    }
}
