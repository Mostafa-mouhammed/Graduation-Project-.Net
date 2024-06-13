using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Categories;

public interface ICategoryRepository:IGenericRepository<Category>
{
    Task<Category>? getOneCategory(int id);
    Task<IEnumerable<Category>>? getCateoriesForGeneral();
    Task<Category>? GetCategorybyName(string name);
    void softDeleteCategory(int id);
    void retriveDeletedCategory(int id);
}
