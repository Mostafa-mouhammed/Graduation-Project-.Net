using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.AttributesRepository;
public class AttributeRepository : GenericRepository<Attributes>, IAttributeRepository
{
    public AttributeRepository(APIContext context) : base(context)
    {
    }

    public async Task<IEnumerable<int>> checkAllAttributesExiest(IEnumerable<int> attributesIds)
    {
        return await _context.Set<Attributes>()
            .Where(a => attributesIds.Contains(a.Id))
            .Select(a => a.Id)
            .ToListAsync();
    }

    public async Task<Attributes> getAttrbuitebyName(string attributes)
    {
        return await _context.Set<Attributes>()
            .FirstOrDefaultAsync(a => a.Name == attributes)!;
    }
}
