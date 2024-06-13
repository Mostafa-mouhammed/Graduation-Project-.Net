using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public IEnumerable<CartProducts> cartProducts { get; set; } = null!;
    public decimal? Total => (decimal)cartProducts.Sum(p => p.Product.Price * p.CartProductQuantity);
}
