using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Category;
using Project.BL.Dtos.ProductImages;
using Project.DAL.Models;
namespace Project.BL.Dtos.Product;
public record ProductOneDTO(
    int Id,
    string Name,
    string desctiption,
    string Image,
    int Discount,
    double rate,
    int Quantity,
    double Price,
    ProductType productType,
    int? variantGroupId,
    BrandReadDTO brand,
    CategoryReadDTO Category,
    IEnumerable<ProductImagesReadDTO> ImagesReadDTOs,
    IEnumerable<AttributeWithValuesReadDTO>? versions
    );