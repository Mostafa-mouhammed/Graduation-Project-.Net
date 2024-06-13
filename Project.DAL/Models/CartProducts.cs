using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;

[PrimaryKey(nameof(CartId), nameof(ProductId))]
public class CartProducts
{
    [ForeignKey("product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    [ForeignKey("cart")]
    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public int CartProductQuantity { get; set; }
}
