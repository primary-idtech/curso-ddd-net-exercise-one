using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ROFE.App.Extensions.ServiceCollection;
using ROFE.Infrastructure.Configurations;
using ROFE.Infrastructure.ORM;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ROFE.App.Extensions;

public static class ServiceCollectionExtension
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddCorsExtension();
        services.AddHealthChecksExtension();
        services.AddInjections();
        services.AddSwaggerGenExtension();
        services.AddHttpContextAccessor();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

        services.AddControllers(o =>
        {
            o.Filters.Add(new ProducesResponseTypeAttribute(400));
        }).AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddDbContext<MyDbContext>(opt =>
        {
            opt.UseSqlServer(AppSettingConfig.DataBaseConnectionString);
        });
    }
}
