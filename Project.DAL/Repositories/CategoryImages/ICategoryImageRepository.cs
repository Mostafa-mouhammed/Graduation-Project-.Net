using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.CategoryImagesRepository;
public interface ICategoryImageRepository : IGenericRepository<CategoriesImages>
{
    Task<CategoriesImages> getCategoryImage(int categoryId, int subCategoryId);
}
