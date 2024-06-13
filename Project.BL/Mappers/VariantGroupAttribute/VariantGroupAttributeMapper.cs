using Project.BL.Dtos.VariantGroupAttributes;
using Project.BL.Mappers.AttributesMapper;
using Project.BL.Mappers.ValuesMapper;
using Project.DAL.Models;

namespace Project.BL.Mappers.VariantGroupAttributeMapper;
public class VariantGroupAttributeMapper : IVariantGroupAttributeMapper
{
    private readonly IAttributeMapper _attributeMapper;

    public VariantGroupAttributeMapper(IAttributeMapper attributeMapper)
    {
        _attributeMapper = attributeMapper;
    }
    public VariantGroupAttributes insertToModel(int variantGroupId, int attributeId)
    {
        return new VariantGroupAttributes()
        {
            attributeId = attributeId,
            variantGroupId = variantGroupId,
        };
    }

    public IEnumerable<VariantGroupAttributes> insertToModelList(int variantGroupId, IEnumerable<int> attributeIds)
    {
        return attributeIds.Select(attributeId => insertToModel(variantGroupId, attributeId));
    }

    public VariantGroupAttributeReadDTO modelToRead(IEnumerable<VariantGroupAttributes> model)
    {
        var x = model.Select(x => x.attributes);
        return new VariantGroupAttributeReadDTO(model.First().variantGroupId,_attributeMapper.modelToWithitsValuesReadDTO(x));
    }

}
