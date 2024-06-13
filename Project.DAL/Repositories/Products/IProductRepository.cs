using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Productsrepository;

public interface IProductRepository:IGenericRepository<Product>
{
    Task<IEnumerable<Product>?> GetProductsAdminPagination(int page,int limit,string sort,int categoryId,int brandId);
    Task<int> GetTotalAdminPagination(int categoryId, int brandId);
    Task<int> GetTotalGeneralPagination(int categoryId, int brandId);
    Task<IEnumerable<Product>?> GetProductsGeneralPagination(int page,int limit,string sort,int categoryId, int brandId);
    Task<Product?> GetOneProduct(int id);
    void softDeleteByCategory(int id);
    void retriveDeletedByCategory(int id);
    void softDeleteByBrand(int id);
    void retriveDeletedByBrand(int id);
    Task<IEnumerable<Product>> getProductsByOrderItems(IEnumerable<OrderItem> orderItems);
}
