using Project.BL.Dtos.VariantGroupAttributes;
using Project.DAL.Models;

namespace Project.BL.Mappers.VariantGroupAttributeMapper;
public interface IVariantGroupAttributeMapper
{
    VariantGroupAttributes insertToModel(int variantGroupId,int attributeId);
    IEnumerable<VariantGroupAttributes> insertToModelList(int variantGroupId, IEnumerable<int> attributeIds);
    VariantGroupAttributeReadDTO modelToRead(IEnumerable<VariantGroupAttributes> model);

}
