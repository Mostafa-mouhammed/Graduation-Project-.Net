using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.SubCategoryRepository;
public interface ISubCategoryRepository : IGenericRepository<SubCategory>
{
    Task<SubCategory> getSubCategorybyName(string name);
    Task<SubCategory> getOneSubCategory(int Id);
    Task<IEnumerable<SubCategory>> getSubcategoriesbyCategory(int Id);
    Task<IEnumerable<SubCategory>> getSubcategorywithProductsByCategory(int Id);
    Task<IEnumerable<SubCategory>> getSubCategoryWithProducts(int categoryId);

}
