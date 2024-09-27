using ROFE.Domain.Abstractions;
using System.Threading.Tasks;

namespace ROFE.Domain.Models.Portfolio;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    Task<Portfolio> GetByIdWithIncludesAsync(int id);
}
