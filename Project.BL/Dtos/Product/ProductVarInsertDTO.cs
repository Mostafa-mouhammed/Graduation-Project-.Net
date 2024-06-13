using Microsoft.AspNetCore.Http;
using Project.BL.Dtos.EAVProducts;
using System.ComponentModel.DataAnnotations;

namespace Project.BL.Dtos.Product;
public record ProductVarInsertDTO(
    [Required] string Name,
     string? Image,
    [Required] int Quantity,
    [Required] string description,
    [Required] double Price,
    [Required] int subCategoryId,
    [Required] int Discount,
    [Required] int brandId,
    IEnumerable<string>? productImages,
    int? ExiestImagesProductId,
    [Required] IEnumerable<int> values,
    [Required] int varGroupId
    );
