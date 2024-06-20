using Project.DAL.DTOs.Products;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Productsrepository;

public interface IProductRepository:IGenericRepository<Product>
{
    Task<IEnumerable<Product>?> GetProductsAdminPagination(ProductQueryDTO query);
    Task<int> GetTotalAdminPagination(int categoryId, int brandId);
    Task<int> GetTotalGeneralPagination(ProductQueryDTO query);
    Task<IEnumerable<Product>?> GetProductsGeneralPagination(ProductQueryDTO query);
    Task<Product?> GetOneProduct(int id);
    void softDeleteByCategory(int id);
    void retriveDeletedByCategory(int id);
    void softDeleteBySubCategory(int id);
    void retriveDeletedBySubCategory(int id);
    void softDeleteByBrand(int id);
    void retriveDeletedByBrand(int id);
    Task<IEnumerable<Product>> getProductsByOrderItems(IEnumerable<OrderItem> orderItems);
}
