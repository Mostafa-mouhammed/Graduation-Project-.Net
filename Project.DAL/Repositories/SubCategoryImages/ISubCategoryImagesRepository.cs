using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.SubCategoryImagesRepostiory;
public interface ISubCategoryImagesRepository : IGenericRepository<SubCategoryImages>
{
    Task<SubCategoryImages> getSubCategoryImage(int subCategoryId, int productId);
}
