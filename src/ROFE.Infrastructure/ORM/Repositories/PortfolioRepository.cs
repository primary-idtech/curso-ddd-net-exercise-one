using Microsoft.EntityFrameworkCore;
using ROFE.Domain.Models.Portfolio;
using System.Threading.Tasks;

namespace ROFE.Infrastructure.ORM.Repositories;

public class PortfolioRepository(DbContext context) : Repository<Portfolio>(context), IPortfolioRepository
{
    public Task<Portfolio> GetByIdWithIncludesAsync(int id)
    {
        return _dbSet
            .Include(p => p.Instruments)
            .ThenInclude(o => o.Histories)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
