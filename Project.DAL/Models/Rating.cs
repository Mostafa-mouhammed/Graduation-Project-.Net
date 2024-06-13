using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
[PrimaryKey(nameof(userId),nameof(productId))]
public class Rating
{
    [Required]
    public string userId { get; set; } = string.Empty;
    [Required]
    public int productId { get; set; }
    [Required]
    [Range(1,5)]
    public int rate { get; set; }
    [MaxLength(600)]
    public string? reviewText { get; set; } = string.Empty;
    [Required]
    [MinLength(1)]
    [MaxLength(150)]
    public string reviewTitle { get; set; } = string.Empty;
    public DateTime date { get; set; } = DateTime.UtcNow;

    [ForeignKey("userId")]
    public User user { get; set; } = null!;
    [ForeignKey("productId")]
    public Product product { get; set; } = null!;
}
