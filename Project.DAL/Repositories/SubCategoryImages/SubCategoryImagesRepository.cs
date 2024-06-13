using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.SubCategoryImagesRepostiory;
public class SubCategoryImagesRepository : GenericRepository<SubCategoryImages>, ISubCategoryImagesRepository
{
    public SubCategoryImagesRepository(APIContext context) : base(context)
    {
    }

    public async Task<SubCategoryImages> getSubCategoryImage(int subCategoryId, int productId)
    {
        return await _context.Set<SubCategoryImages>()
            .FirstOrDefaultAsync(sci => sci.subCategoryId == subCategoryId && sci.productId == productId);
    }
}
