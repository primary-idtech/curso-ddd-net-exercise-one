using Microsoft.EntityFrameworkCore;
using ROFE.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROFE.Infrastructure.ORM.Repositories;

public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

    public Repository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetPagedAsync(int offset, int limit)
    {
        return await _dbSet.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }
}