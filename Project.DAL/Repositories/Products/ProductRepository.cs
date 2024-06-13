
using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Productsrepository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(APIContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> getProductsByOrderItems(IEnumerable<OrderItem> orderItems)
    {
        IQueryable<Product> query = _context
            .Set<Product>()
            .AsNoTracking()
            .Where(p => orderItems.Select(o => o.ProductId)
            .Contains(p.Id))
            .AsQueryable();
        return await query.ToListAsync();
    }



public async Task<IEnumerable<Product>>? GetProductsAdminPagination(int page, int limit, string sort, int subCategoryId, int brandId)
{
     IQueryable<Product> query = _context.Set<Product>()
    .AsNoTracking()
    .Where(p => (subCategoryId > 0) ? p.subCategoryId == subCategoryId : p.subCategoryId != 0)
    .Where(p => (brandId > 0) ? p.brandId == brandId : p.brandId != 0)
    .DistinctBy(p => p.variantGroupId)
    .Include(p => p.Ratings)
    .OrderByDescending(p => sort == "Low" ? -p.Price : sort == "High" ? p.Price : sort == "New" ? p.Id : sort == "Discount" ? p.Discount : p.Ratings.Average(r => r.rate))
    .Skip((page - 1) * limit)
    .Take(limit)
    .AsQueryable();

    return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>?> GetProductsGeneralPagination(int page, int limit, string sort, int categoryId, int brandId)
{
        IQueryable<Product> query =  _context.Set<Product>()
         .AsNoTracking()
         .Where(p => (categoryId > 0) ? p.subCategoryId == categoryId : p.subCategoryId != 0)
         .Where(p => (brandId > 0) ? p.brandId == brandId : p.brandId != 0)
         .Include(p => p.Ratings)
         .OrderByDescending(p => 
           sort == "Low" ? -p.Price
         : sort == "High" ? p.Price
         : sort == "New" ? p.Id
         : sort == "Discount" ? p.Discount
         : p.Ratings.Average(r => r.rate))
         .Skip((page - 1) * limit)
         .Take(limit)
         .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<Product?> GetOneProduct(int id)
{
    return await _context.Set<Product>()
        .Where(p => p.Id == id)
        .AsNoTracking()
        .Include(p => p.subCategory)
        .Include(p => p.Brand)
        .Include(p => p.Ratings)
        .Include(p => p.Images)
        .Include(p => p.EAVProducts)
        .FirstOrDefaultAsync();
}

public async Task<int> GetTotalAdminPagination(int categoryId, int brandId)
{
    return await _context.Set<Product>()
      .AsNoTracking()
      .Where(p => (categoryId > 0) ? p.subCategoryId == categoryId : p.subCategoryId != 0)
      .Where(p => (brandId > 0) ? p.brandId == brandId : p.brandId != 0)
      .CountAsync();
}

public async Task<int> GetTotalGeneralPagination(int categoryId, int brandId)
{
    return await _context.Set<Product>()
   .AsNoTracking()
   .Where(p => p.isDeleted == false)
   //.DistinctBy(p => p.variantGroupId)
   .Where(p => (categoryId > 0) ? p.subCategoryId == categoryId : p.subCategoryId != 0)
   .Where(p => (brandId > 0) ? p.brandId == brandId : p.brandId != 0)
   .CountAsync();
}

public void retriveDeletedByBrand(int id)
{
    _context.Set<Product>()
         .Where(p => p.brandId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, false));
}

public void retriveDeletedByCategory(int id)
{
    _context.Set<Product>()
         .Where(p => p.subCategoryId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, false));
}

public void softDeleteByBrand(int id)
{
    _context.Set<Product>()
         .Where(p => p.brandId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, true));
}

public void softDeleteByCategory(int id)
{
    _context.Set<Product>()
         .Where(p => p.subCategoryId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, true));
}

}