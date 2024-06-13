using Project.BL.Dtos.Values;

namespace Project.BL.Dtos.Attribute;
public record AttributeWithitsValuesReadDTO(AttributesReadDTO attribute, IEnumerable<ValuesReadDTO> values);