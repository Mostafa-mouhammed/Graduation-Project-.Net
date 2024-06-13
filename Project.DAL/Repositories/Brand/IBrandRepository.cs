using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Brandrepo;
public interface IBrandRepository : IGenericRepository<Brand>
{
    Task<Brand>? getByName(string brandName);
    Task<IEnumerable<Brand>> getGeneralBrands();
    void softDeleteBrand(int id);
    void retriveDeletedBrand(int id);
}
