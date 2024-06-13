using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ProductImageRepository;
public class ProductImagesRepositiory : GenericRepository<ProductsImages>, IProductImagesRepositiory
{
    public ProductImagesRepositiory(APIContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProductsImages>> getProductImagesByProductId(int Id)
    {
        return await _context
            .Set<ProductsImages>()
            .AsNoTracking()
            .Where(pi => pi.productId == Id)
            .ToListAsync();
    }
}
