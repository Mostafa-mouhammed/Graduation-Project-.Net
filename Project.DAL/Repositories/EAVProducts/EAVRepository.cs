using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.EAVProductsRepository;
public class EAVRepository : GenericRepository<EAVProducts>, IEAVRepository
{
    public EAVRepository(APIContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EAVProducts>> getEAVbyGroupId(int groupId)
    {
        return await _context.Set<EAVProducts>()
            .Where(eav => eav.variantGroupId == groupId)
            .Include(eav => eav.value)
            .ThenInclude(eav => eav.attribute)
            .ToListAsync();
    }

    public async Task<IEnumerable<EAVProducts>> getEavbyGroupOnly(int groupId)
    {
        return await _context.
             Set<EAVProducts>()
            .Where(eav => eav.variantGroupId == groupId)
            .ToListAsync();
    }
}