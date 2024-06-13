using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ValuesRepository;
public class ValuesRepository : GenericRepository<Values>, IValuesRepository
{
    public ValuesRepository(APIContext context) : base(context)
    {
    }

    public async Task<Values> getValuebyName(string valueName)
    {
        IQueryable<Values> query = _context
            .Set<Values>()
            .Where(v => v.Name == valueName)
            .AsQueryable();

        return await query.FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<Values>> getValuesInList(IEnumerable<int> valuesIds)
    {
        IQueryable<Values> query = _context
            .Set<Values>()
            .AsNoTracking()
            .Where(v => valuesIds.Contains(v.Id))
            .AsQueryable();

            return await query.ToListAsync();
    }
}
