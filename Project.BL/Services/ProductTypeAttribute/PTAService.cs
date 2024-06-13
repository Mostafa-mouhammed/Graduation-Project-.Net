using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.ProductTypeAttribute;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.ProductTypeAttributeService;
public class PTAService : IPTAService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public PTAService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> getAllAtrributesbyType(int productType)
    {
        ProductsTypes productsTypes = await _unit.productTypes.Getone(productType);
        if (productsTypes == null)
            return new StatuscodeDTO(Statuscode.NotFound,"There is no product type with this id");

        IEnumerable<Attributes> attributes = await _unit.productTypeAttribute.getAllAtrributesbyType(productType);
        IEnumerable<AttributesReadDTO> attributesReads = _mapper.attribute.modelToReadList(attributes);
        return new StatuscodeDTO(Statuscode.Ok,null, attributesReads);
    }

    public async Task<StatuscodeDTO> addPTA(PTAInsertDto insert)
    {
        Attributes attributes = await _unit.attribute.Getone(insert.AttributeId)!;
        if (attributes == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no attribute with this id");

        ProductsTypes productType = await _unit.productTypes.Getone(insert.ProductsTypesId)!;
        if (productType == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product Type with this id");

        PTA productsTypes =
             await _unit.productTypeAttribute.getPTAbyattrIdProudctType(insert.ProductsTypesId, insert.AttributeId);
        if (productsTypes != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is already productType-attribute with this id's");

        PTA PTAmodel =  _mapper.PTA.insertTomodel(insert);
        await _unit.productTypeAttribute.Add(PTAmodel);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created);
    }
}
