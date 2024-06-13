using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ProductTypesRepository;
public class ProductTypesRepository : GenericRepository<ProductsTypes>, IProductTypesRepository
{
    public ProductTypesRepository(APIContext context) : base(context)
    {
    }

    public async Task<ProductsTypes?>? getProductTypeByName(string name)
    {
      return await _context.Set<ProductsTypes>()
            .FirstOrDefaultAsync(pt => name.Equals(pt.Name));
    }
}
