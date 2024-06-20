using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Categories;

public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
{
    private readonly APIContext _context;
    public CategoryRepository(APIContext context):base(context){
        _context = context;
    }

    public async Task<Category?> GetCategorybyName(string name)
    {
       IQueryable<Category> query = _context
            .Set<Category>()
            .AsNoTracking()
            .Where(c => c.Name.Equals(name))
            .AsQueryable();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Category?> getOneCategory(int id)
    {
        IQueryable<Category> query = _context
            .Set<Category>()
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Include(c => c.subCategories)
            .Include(c => c.categoriesImages)
            .AsQueryable();

           return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>?> getCateoriesForGeneral()
    {
        IQueryable<Category> query = _context
           .Set<Category>()
           .AsNoTracking()
           .Where(c => c.isDeleted == false)
           .Include(c => c.subCategories)
           .AsQueryable();

            return await query.ToListAsync();
    }

    public void retriveDeletedCategory(int id)
    {
        _context.Set<Category>()
            .FirstOrDefault(c => c.Id == id).isDeleted = false;
    }

    public void softDeleteCategory(int id)
    {
        _context.Set<Category>().FirstOrDefault(c => c.Id == id).isDeleted = true;
    }
}
