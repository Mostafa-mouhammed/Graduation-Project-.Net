using Project.DAL.Models;
using System.Text.Json.Serialization;

namespace Project.BL.Dtos.Product;
public record ProductReadDTO(
    int Id,
    string Name,
    string desctiption,
    string Image,
    int Quantity,
    int Discount,
    double rate,
    double Price,
    int subCategoryId,
    string productType,
    int brandId,
    int? variantGroupId
    );