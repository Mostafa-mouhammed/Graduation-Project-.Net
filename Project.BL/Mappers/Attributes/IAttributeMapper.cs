using Project.BL.Dtos.Attribute;
using Project.DAL.Models;

namespace Project.BL.Mappers.AttributesMapper;
public interface IAttributeMapper
{
    AttributesReadDTO modelToRead(Attributes model);
    IEnumerable<AttributesReadDTO> modelToReadList(IEnumerable<Attributes> model);
    Attributes insertToModel(AttributeInsertDTO insert);
    AttributeWithitsValuesReadDTO modelToWithitsValuesReadDTO(Attributes model);
    IEnumerable<AttributeWithitsValuesReadDTO> modelToWithitsValuesReadDTO(IEnumerable<Attributes> model);
}
