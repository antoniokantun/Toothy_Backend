using Microsoft.EntityFrameworkCore;
using Toothy.Domain.Interfaces;
using Toothy.Infrastructure.Context;

namespace Toothy.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ToothyDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ToothyDbContext context)
    { 
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task ActualizarAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<T> AgregarAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task EliminarAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<T?> ObtenerPorIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }
}
