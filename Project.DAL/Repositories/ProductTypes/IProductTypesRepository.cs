using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ProductTypesRepository;
public interface IProductTypesRepository : IGenericRepository<ProductsTypes>
{
    Task<ProductsTypes>? getProductTypeByName(string name);
}
