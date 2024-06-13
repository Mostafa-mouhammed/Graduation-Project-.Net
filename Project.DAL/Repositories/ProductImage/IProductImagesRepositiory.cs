using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ProductImageRepository;
public interface IProductImagesRepositiory : IGenericRepository<ProductsImages>
{
    Task<IEnumerable<ProductsImages>> getProductImagesByProductId(int Id);
}
