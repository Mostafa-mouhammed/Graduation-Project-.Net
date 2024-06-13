using Microsoft.AspNetCore.Http;
using Project.DAL.Models;
using System.ComponentModel.DataAnnotations;
namespace Project.BL.Dtos.Product;

public record ProductSimpleInsertDTO(
    [Required] string Name,
    [Required] string Image,
    [Required] int Quantity,
    [Required] string description,
    [Required] double Price,
    [Required] int subCategoryId,
    [Required] int Discount,
    [Required] int brandId,
    [Required] IEnumerable<string> productImages
    );