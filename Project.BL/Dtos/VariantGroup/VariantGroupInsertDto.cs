namespace Project.BL.Dtos.VariantGroup;
public record VariantGroupInsertDto(string Name,IEnumerable<int> attributesIds);