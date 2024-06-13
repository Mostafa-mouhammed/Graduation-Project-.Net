namespace Project.BL.Dtos.EAVProducts;
public record EAVReadDTO(
        int? productId,
        int? valueId,
        string? valueName,
        int? variantGroupId,
        int? attributeId,
        string? attributeName
    );