using Microsoft.Extensions.Configuration;
using ROFE.Infrastructure.Configurations;

namespace ROFE.App.Extensions;

public static class ConfigurationExtension
{
    public static AppSettingConfig AppSettings { get; set; }

    public static void Configure(this IConfiguration configuration)
    {
        AppSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettingConfig>();
    }
}
