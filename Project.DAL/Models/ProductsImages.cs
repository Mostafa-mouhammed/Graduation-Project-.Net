using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;

[PrimaryKey(nameof(productId), nameof(imageURL))]
public class ProductsImages
{
    [ForeignKey("product")]
    public int productId { get; set; }
    public Product product { get; set; } = null!;
    public string imageURL { get; set; } 
}