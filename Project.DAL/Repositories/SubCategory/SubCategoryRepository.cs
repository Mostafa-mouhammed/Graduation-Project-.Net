using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.SubCategoryRepository;
public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
    public SubCategoryRepository(APIContext context) : base(context)
    {
    }

    public async Task<SubCategory> getOneSubCategory(int Id)
    {
        IQueryable<SubCategory> query = _context
            .Set<SubCategory>()
            .AsNoTracking()
            .Where(sc => sc.Id == Id)
            .Include(sc => sc.subCategoryImages)
            .AsQueryable();

           return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SubCategory>> getSubcategoriesbyCategory(int Id)
    {
        IQueryable<SubCategory> query = _context
            .Set<SubCategory>()
            .AsNoTracking()
            .Where(sc => sc.categoryId == Id)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<SubCategory> getSubCategorybyName(string name)
    {

        IQueryable<SubCategory> query = _context
            .Set<SubCategory>()
            .AsNoTracking()
            .Where(sc => sc.Name.Equals(name))
            .AsQueryable();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SubCategory>> getSubCategoryWithProducts(int categoryId)
    {
        IQueryable<SubCategory> query = _context
            .Set<SubCategory>()
            .AsNoTracking()
            .Where(sc => sc.categoryId == categoryId)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<SubCategory>> getSubcategorywithProductsByCategory(int Id)
    {
        return await _context
            .Set<SubCategory>()
            .Where(sc => sc.categoryId == Id)
            .Include (sc => sc.Products.Take(12))
            .ToListAsync(); 
    }
}
