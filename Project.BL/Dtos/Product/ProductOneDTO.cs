using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Category;
using Project.BL.Dtos.ProductImages;
using Project.DAL.Models;
namespace Project.BL.Dtos.Product;
public record ProductOneDTO(
    int id,
    string name,
    string desctiption,
    string image,
    int discount,
    double rate,
    int quantity,
    double Price,
    bool isDeleted,
    ProductType productType,
    int? variantGroupId,
    BrandReadDTO brand,
    CategoryReadDTO category,
    IEnumerable<ProductImagesReadDTO> productImages,
    IEnumerable<AttributeWithValuesReadDTO>? versions
    );