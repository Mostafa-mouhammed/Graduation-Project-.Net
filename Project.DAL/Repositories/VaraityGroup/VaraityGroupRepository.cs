using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.VaraityGroupRepository;
public class VaraityGroupRepository : GenericRepository<VariantGroup>, IVaraityGroupRepository
{
    public VaraityGroupRepository(APIContext context) : base(context)
    {
    }

    public async Task<VariantGroup?>? getGroupbyName(string name)
    {
         IQueryable<VariantGroup> query = _context
            .Set<VariantGroup>()
            .AsNoTracking()
            .Where(vg => vg.Name.Equals(name))
            .AsQueryable();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<VariantGroup?>? getGroupWithAttributes(int Id)
    {
        IQueryable<VariantGroup> query = _context
            .Set<VariantGroup>()
            .Where(vg => vg.Id == Id)
            .Include(vg => vg.variantGroupAttributes)
            .AsQueryable();

        return await query.FirstOrDefaultAsync();
    }

}
