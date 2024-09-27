using System.Collections.Generic;
using System.Threading.Tasks;

namespace ROFE.Domain.Abstractions;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetPagedAsync(int offset, int limit);
    Task<int> CountAsync();
}
