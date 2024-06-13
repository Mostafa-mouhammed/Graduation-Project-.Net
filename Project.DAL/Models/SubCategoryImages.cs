using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;

[PrimaryKey(nameof(subCategoryId), nameof(productId))]
public class SubCategoryImages
{
    [ForeignKey("subCategory")]
    public int subCategoryId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public SubCategory subCategory { get; set; } = null!;

    [ForeignKey("product")]
    public int productId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product product { get; set; } = null!;
    public string imageURL { get; set; }
}