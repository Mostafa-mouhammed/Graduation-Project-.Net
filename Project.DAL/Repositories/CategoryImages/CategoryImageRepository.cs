using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.CategoryImagesRepository;
public class CategoryImageRepository : GenericRepository<CategoriesImages>, ICategoryImageRepository
{
    public CategoryImageRepository(APIContext context) : base(context)
    {
    }

    public async Task<CategoriesImages> getCategoryImage(int categoryId, int subCategoryId)
    {
        return await _context.Set<CategoriesImages>()
            .FirstOrDefaultAsync(ci => ci.CategoryId == categoryId && ci.subCategoryId == subCategoryId);
    }
}
