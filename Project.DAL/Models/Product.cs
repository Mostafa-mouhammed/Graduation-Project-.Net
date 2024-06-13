using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project.DAL.Models;
public class Product
{
    [Key]
    public int Id { get; set; }
    [MinLength(3)]
    [MaxLength(150)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Discription { get; set; } = string.Empty;
    [Required]
    public string Image { get; set; } = string.Empty;
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    [Required]
    [Range(0.1, double.MaxValue)]
    public double Price { get; set; }
    [Required]
    [Range(0, 100)]
    public int Discount { get; set; } = 0;
    [Range(0, 5)]
    public double rate { get; set; } = 0.0;
    public bool isDeleted { get; set; } = false;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductType productType { get; set; } = ProductType.Simple;

    [Required]
    [ForeignKey("subCategory")]
    public int subCategoryId { get; set; }
    public SubCategory subCategory { get; set; } = null!;

    [Required]
    [ForeignKey("Brand")]
    public int brandId { get; set; }
    public Brand Brand { get; set; } = null!;

    [ForeignKey("variantGroup")]
    public int? variantGroupId { get; set; } = null!;
    public VariantGroup? variantGroup { get; set; } = null!;
    public IEnumerable<EAVProducts> EAVProducts { get; set; } = null!;
    public IEnumerable<Rating> Ratings { get; set; } = null!;
    public IEnumerable<WishList> wishLists { get; set; } = null!;
    public IEnumerable<ProductsImages> Images { get; set; } = null!;
    public IEnumerable<OrderItem> orderItems { get; set; } = null!;
}

public enum ProductType
{
    Simple,
    variety
}
