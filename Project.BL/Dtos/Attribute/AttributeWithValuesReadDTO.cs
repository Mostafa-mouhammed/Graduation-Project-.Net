using Project.BL.Dtos.Values;

namespace Project.BL.Dtos.Attribute;
public record AttributeWithValuesReadDTO(
        int ProductId,
        IEnumerable<AttributesReadDTO> attributesReadDTO,
        IEnumerable<ValuesReadDTO> values
    );