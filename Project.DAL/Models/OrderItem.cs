using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    public double ItemPrice { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int ItemQuantity { get; set; }

    [ForeignKey("product")]
    public int ProductId { get; set; }
    public Product product { get; set; } = null!;

    [ForeignKey("order")]
    public int OrderId { get; set; }
    public Order order { get; set; } = null!;
}
