using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.VariantGroupAttributeRepository;
public class VariantGroupAttributeRepository : GenericRepository<VariantGroupAttributes>, IVariantGroupAttributeRepository
{
    public VariantGroupAttributeRepository(APIContext context) : base(context)
    {
    }

    public async Task<IEnumerable<VariantGroupAttributes>> getGroupAttributeValues(int Id)
    {
        IQueryable<VariantGroupAttributes> query = _context
            .Set<VariantGroupAttributes>()
            .AsNoTracking()
            .Where(g => g.variantGroupId == Id)
            .Include(g => g.attributes)
            .ThenInclude(g => g.values)
            .AsQueryable();

        return await query.ToListAsync();
    }
}
