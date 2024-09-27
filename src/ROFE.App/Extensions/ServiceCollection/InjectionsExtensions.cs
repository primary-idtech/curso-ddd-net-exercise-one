using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Portfolio;
using ROFE.Infrastructure.ORM;
using ROFE.Infrastructure.ORM.Repositories;

namespace ROFE.App.Extensions.ServiceCollection;

public static class InjectionsExtensions
{
    public static IServiceCollection AddInjections(this IServiceCollection services)
    {
        services.AddScoped<DbContext, MyDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //TODO: Agregar Repositories específicos si es necesario
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();

        return services;
    }
}
