using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ProductTypeAttributeRepository;

public class PTARepository : GenericRepository<PTA>, IPTARepository
{
    public PTARepository(APIContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Attributes>> getAllAtrributesbyType(int productType)
    {
        return await _context.Set<PTA>()
            .Where(pta => pta.ProductsTypesId == productType)
            .Include(pta => pta.attributes)
            .Select(pta => pta.attributes)
            .ToListAsync();
    }

    public async Task<PTA> getPTAbyattrIdProudctType(int ProductTypeId, int AttribuiteId)
    {
        return await _context.Set<PTA>()
            .FirstOrDefaultAsync(
            pta => pta.ProductsTypesId == ProductTypeId &&
            pta.AttributeId == AttribuiteId
            );
    }
}
