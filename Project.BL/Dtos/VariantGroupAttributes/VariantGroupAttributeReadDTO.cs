using Project.BL.Dtos.Attribute;

namespace Project.BL.Dtos.VariantGroupAttributes;
public record VariantGroupAttributeReadDTO(int GroupId, IEnumerable<AttributeWithitsValuesReadDTO> AttributeWithValues);