using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Brandrepo;

public class BrandRepository : GenericRepository<Brand> , IBrandRepository
{
    public BrandRepository(APIContext context):base(context)
    {
        
    }

    public async Task<Brand>? getByName(string brandName)
    {
        IQueryable<Brand> query = _context
            .Set<Brand>()
            .AsNoTracking()
            .Where(b => b.Name.Equals(brandName));

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Brand>> getGeneralBrands()
    {

        IQueryable<Brand> query = _context
        .Set<Brand>()
        .AsNoTracking()
        .Where(b => b.isDeleted == false);

        return await query.ToListAsync();
    }

    public async void retriveDeletedBrand(int id)
    {
       _context.Set<Brand>().FindAsync(id).Result.isDeleted = false;
    }

    public void softDeleteBrand(int id)
    {
        _context.Set<Brand>().FirstOrDefault(b => b.Id == id).isDeleted = true;
    }
}
