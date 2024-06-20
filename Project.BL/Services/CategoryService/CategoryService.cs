using Project.BL.Dtos.Category;
using Project.BL.Dtos.CategoryImageDtos;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public CategoryService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> AddCategory(CategoryInsertDTO category)
    {
        var Category = await _unitRepository.category.GetCategorybyName(category.Name);

        if (Category != null)
            return new StatuscodeDTO(Statuscode.NotFound, "This category is already exiest"); ;

        Category CreatedCategory = await _mapper.category.insertToModel(category);
        _unitRepository.category.Add(CreatedCategory);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.Created);
    }

    public async Task<StatuscodeDTO> GetAllAdminCategories()
    {
        IEnumerable<Category>? modelCategories = await _unitRepository.category.GetAll();
        IEnumerable<categoryAdminDTO> readCategories = _mapper.category.listModelToReadAdmin(modelCategories);
        return new StatuscodeDTO(Statuscode.Ok, null, readCategories);
    }

    public async Task<StatuscodeDTO> GetOneCategory(int id)
    {
        Category? category = await _unitRepository.category.getOneCategory(id);
        if (category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        IEnumerable<CategoryImageReadDTO> categoryImagesDTO = _mapper.categoryImages.modelToReadList(category.categoriesImages);

        CategoryDetailsDTO? CategoryDetails = _mapper.category.modelToDetail(category, categoryImagesDTO);
        return new StatuscodeDTO(Statuscode.Ok, "There is no category with this id", CategoryDetails);
    }

    public async Task<StatuscodeDTO> UpdateCategory(int id, CategoryInsertDTO insert)
    {
        Category? Category = await _unitRepository.category.Getone(id);
        if (Category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        Category.Name = insert.Name;
        Category.Description = insert.Description;
        Category.image = insert.image;
        _unitRepository.category.Update(Category);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }
    public async Task<StatuscodeDTO> DeleteCategory(int id)
    {
        Category? Category = await _unitRepository.category.Getone(id);

        if (Category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        _unitRepository.category.Delete(Category);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> SoftDeleteCategory(int id)
    {
        Category? Category = await _unitRepository.category.Getone(id);

        if (Category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        Category.isDeleted = true;
        _unitRepository.product.softDeleteByCategory(id);

        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> GetAllGeneralCategories()
    {
        IEnumerable<Category>? modelCategories = await _unitRepository.category.getCateoriesForGeneral();
        IEnumerable<CategoryWithSubs> categoryWithSubs = _mapper.category.modelToCategorySubsList(modelCategories);
        return new StatuscodeDTO(Statuscode.Ok, null, categoryWithSubs);
    }

    public async Task<StatuscodeDTO> RetreiveDeletedCategory(int id)
    {
        Category? Category = await _unitRepository.category.Getone(id);

        if (Category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        Category.isDeleted = false;
        _unitRepository.product.retriveDeletedByCategory(id);

        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> AddCategorybanner(CategoryImageInsertDTO insert)
    {
        Category? Category = await _unitRepository.category.Getone(insert.categoryId);
        if (Category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");


        SubCategory? subCategory = await _unitRepository.subCategory.Getone(insert.subCategoryId);
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        CategoriesImages? Exiestcategory = await _unitRepository.CategoryImages.getCategoryImage(insert.categoryId,insert.subCategoryId);
        if (Exiestcategory != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is already category image with category id and sub category id");

        CategoriesImages image = await _mapper.categoryImages.insertToModel(insert);
        await _unitRepository.CategoryImages.Add(image);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created);
    }
}
