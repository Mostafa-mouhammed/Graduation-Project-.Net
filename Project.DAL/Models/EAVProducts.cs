using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
[PrimaryKey(nameof(productId),nameof(valueId))]
public class EAVProducts
{

    [Required]
    [ForeignKey("product")]
    public int productId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product product { get; set; } = null!;

    [Required]
    [ForeignKey("variantGroup")]
    public int? variantGroupId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public VariantGroup variantGroup { get; set; } = null!;

    [Required]
    [ForeignKey("value")]
    public int valueId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Values value { get; set; } = null!;
}
