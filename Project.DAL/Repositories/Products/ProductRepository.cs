
using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.DTOs.Products;
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
            .Where(p => orderItems.Select(o => o.ProductId)
            .Contains(p.Id))
            .AsQueryable();
        return await query.ToListAsync();
    }


public async Task<IEnumerable<Product>>? GetProductsAdminPagination(ProductQueryDTO q)
{
        IQueryable<Product> query = _context
         .Set<Product>()
         .AsNoTracking()
         .Where(p => (q.subCategoryId > 0) ? p.subCategoryId == q.subCategoryId : p.subCategoryId != 0)
         .Where(p => (q.brandId > 0) ? p.brandId == q.brandId : p.brandId != 0)
         .Where(p => p.Name.Contains(q.keyword))
         .Include(p => p.Ratings)
         .OrderByDescending(p =>
           q.sort == "Low" ? -p.Price
         : q.sort == "High" ? p.Price
         : q.sort == "New" ? p.Id
         : q.sort == "Discount" ? p.Discount
         : p.Ratings.Average(r => r.rate))
         .Skip((q.page - 1) * q.limit)
         .Take(q.limit)
         .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>?> GetProductsGeneralPagination(ProductQueryDTO q)
{
        IQueryable<Product> query = _context
         .Set<Product>()
         .AsNoTracking()
         .Where(p => p.isDeleted == false)
         .Where(p => (q.subCategoryId > 0) ? p.subCategoryId == q.subCategoryId : p.subCategoryId != 0)
         .Where(p => (q.brandId > 0) ? p.brandId == q.brandId : p.brandId != 0)
         .Where(p => p.Name.Contains(q.keyword))
         .Include(p => p.Ratings)
         .OrderByDescending(p => 
           q.sort == "Low" ? -p.Price
         : q.sort == "High" ? p.Price
         : q.sort == "New" ? p.Id
         : q.sort == "Discount" ? p.Discount
         : p.Ratings.Average(r => r.rate))
         .Skip((q.page - 1) * q.limit)
         .Take(q.limit)
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

public async Task<int> GetTotalGeneralPagination(ProductQueryDTO q)
{
    return await _context
      .Set<Product>()
      .AsNoTracking()
      .Where(p => p.isDeleted == false)
      .Where(p => (q.subCategoryId > 0) ? p.subCategoryId == q.subCategoryId : p.subCategoryId != 0)
      .Where(p => (q.brandId > 0) ? p.brandId == q.brandId : p.brandId != 0)
      .Where(p => p.Name.Contains(q.keyword))
      .CountAsync();
}

public void softDeleteByBrand(int id)
{
    _context.Set<Product>()
         .Where(p => p.brandId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, true));
}

public void retriveDeletedByBrand(int id)
{
    _context.Set<Product>()
         .Where(p => p.brandId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, false));
}

public void softDeleteByCategory(int id)
{
    _context.Set<Product>()
         .Include(p => p.subCategory)
         .Where(p => p.subCategory.categoryId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, true));
}

public void retriveDeletedByCategory(int id)
{
    _context.Set<Product>()
         .Include(p => p.subCategory)
         .Where(p => p.subCategory.categoryId == id)
         .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, false));
}

    public void softDeleteBySubCategory(int id)
    {
        _context.Set<Product>()
             .Where(p => p.subCategoryId == id)
             .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, true));
    }

    public void retriveDeletedBySubCategory(int id)
    {
        _context.Set<Product>()
             .Where(p => p.subCategoryId == id)
             .ExecuteUpdate(p => p.SetProperty(p => p.isDeleted, false));
    }


}