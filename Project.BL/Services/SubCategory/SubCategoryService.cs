using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.SubCategory;
using Project.BL.Dtos.SubCategoryImage;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.SubCategoryService;
public class SubCategoryService : ISubCategoryService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public SubCategoryService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> add(SubCategoryInsertDTO insert)
    {
        SubCategory ExiestsubCategory = await _unit.subCategory.getSubCategorybyName(insert.Name);
        if (ExiestsubCategory != null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is already sub category with this name");

        Category Category = await _unit.category.Getone(insert.categoryId);
        if (Category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        SubCategory subCategory = await _mapper.subCategory.insertToModel(insert);
        await _unit.subCategory.Add(subCategory);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created);
    }

    public async Task<StatuscodeDTO> AddSubCategorybanner(SubCategoryImageInsertDTO insert)
    {
        SubCategory subCategory = await _unit.subCategory.Getone(insert.subCategoryId);
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        Product product = await _unit.product.Getone(insert.productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        SubCategoryImages existImage = await _unit.subCategoryImages.getSubCategoryImage(insert.subCategoryId,insert.productId);
        if (existImage != null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is already image with this product id and sub category id");

        SubCategoryImages image = await _mapper.subCategoryImage.insertToModel(insert);
        await _unit.subCategoryImages.Add(image);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created);

    }

    public async Task<StatuscodeDTO> delete(int Id)
    {
        SubCategory subCategory = await _unit.subCategory.Getone(Id);
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        _unit.subCategory.Delete(subCategory);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getAll()
    {
        IEnumerable<SubCategory> subCategoriesModels = await _unit.subCategory.GetAll();
        IEnumerable<SubCategoryAdminReadDTO> subCategoriesRead = _mapper.subCategory.modelToAdminReadList(subCategoriesModels);
        return new StatuscodeDTO(Statuscode.Ok,null, subCategoriesRead);
    }

    public async Task<StatuscodeDTO> getByCategory(int Id)
    {
        Category category = await _unit.category.Getone(Id);
        if (category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        IEnumerable<SubCategory> subCategories = await _unit.subCategory.getSubcategoriesbyCategory(Id);
        IEnumerable<SubCategoryReadDTO> subCategoriesDTO = _mapper.subCategory.modelToReadList(subCategories);

        return new StatuscodeDTO(Statuscode.Ok, null,subCategoriesDTO);
    }

    public async Task<StatuscodeDTO> getOne(int Id)
    {
        SubCategory subCategory = await _unit.subCategory.getOneSubCategory(Id);
        if(subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound,"There is no sub category with this id");

        IEnumerable<SubCategoryImageReadDTO> images = _mapper.subCategoryImage.modelToReadList(subCategory.subCategoryImages); 
        SubCategoryDetailsDTO subCategoryDetials=  _mapper.subCategory.modelToDetails(subCategory, images);
        return new StatuscodeDTO(Statuscode.Ok, null, subCategoryDetials);
    }

    public async Task<StatuscodeDTO> getSubCateoriesWithProductsByCategory(int Id)
    {
        IEnumerable<SubCategory> subCategories = await _unit.subCategory.getSubcategorywithProductsByCategory(Id);
        IEnumerable<SubCategoryWithProductDTO> subcategoireswithproducts = _mapper.subCategory.modelToWithProductsList(subCategories);

        return new StatuscodeDTO(Statuscode.Ok,null, subcategoireswithproducts);
    }

    public async Task<StatuscodeDTO> Retrieve(int Id)
    {
        SubCategory subCategory = await _unit.subCategory.Getone(Id);
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        subCategory.isDeleted = false;
        _unit.product.retriveDeletedBySubCategory(Id);

        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> SoftDelete(int Id)
    {
        SubCategory subCategory = await _unit.subCategory.Getone(Id);
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        subCategory.isDeleted = true;
        _unit.product.softDeleteBySubCategory(Id);

        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> update(int Id,SubCategoryUpdateDTO update)
    {
        SubCategory subCategory = await _unit.subCategory.Getone(Id);
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        subCategory.Name = update.Name;
        subCategory.Description = update.Description;
        subCategory.categoryId = update.categoryId;
        subCategory.image = await _mapper.image.ConvertImage(update.image);

        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }
}
