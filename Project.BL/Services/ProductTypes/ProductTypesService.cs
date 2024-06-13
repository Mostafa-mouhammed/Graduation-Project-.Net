using Project.BL.Dtos.ProductTypes;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.ProductTypes;
public class ProductTypesService : IProductTypesService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public ProductTypesService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> addProductTypes(ProductTypesInsertDTO insert)
    {
        /* check if this type is exesit already */
        ProductsTypes ExiestedproductsType = await _unit.productTypes.getProductTypeByName(insert.name)!;

        if (ExiestedproductsType != null)
            return new StatuscodeDTO(Statuscode.BadRequest,"There is already type with this name");

        /* if not map it to model object then add it */
        ProductsTypes productsType = _mapper.productType.insertToModel(insert);
        await _unit.productTypes.Add(productsType);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created);
    }

    public async Task<StatuscodeDTO> deleteProductTypes(int Id)
    {
        /* check if this type is exesit already */
        ProductsTypes ExiestedproductsType = await _unit.productTypes.Getone(Id)!;

        if (ExiestedproductsType == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no type with this id");

        /* delete type */
        _unit.productTypes.Delete(ExiestedproductsType);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getAllProductTypes()
    {
        IEnumerable<ProductsTypes> productsTypes = await _unit.productTypes.GetAll();
        IEnumerable<ProductTypesReadDTO> productsTypesReads = _mapper.productType.modelToReadList(productsTypes);
        return new StatuscodeDTO(Statuscode.Ok, null, productsTypes);
    }

    public async Task<StatuscodeDTO> updateProductTypes(int Id,ProductTypesUpdateDTO update)
    {
        /* check if this type is exesit already */
        ProductsTypes ExiestedproductsType = await _unit.productTypes.Getone(Id)!;

        if (ExiestedproductsType == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no type with this id");

        /* update type proprites */
        ExiestedproductsType.Name = update.name;
        _unit.productTypes.Update(ExiestedproductsType);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }
}
