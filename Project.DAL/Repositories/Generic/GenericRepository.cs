

using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;

namespace Project.DAL.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly APIContext _context;

    public GenericRepository(APIContext context)
    {
        _context = context;
    }

    public async Task Add(T t)
    {
       await _context.Set<T>().AddAsync(t);
    }

    public async Task AddRange(IEnumerable<T> t)
    {
        await _context.Set<T>().AddRangeAsync(t);
    }

    public void Delete(T t)
    {
       _context.Set<T>().Remove(t);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T>? Getone(int id)
    {
       return await _context.Set<T>().FindAsync(id);
    }

    public void Update(T t)
    {
        _context.Set<T>().Update(t);
    }
}
