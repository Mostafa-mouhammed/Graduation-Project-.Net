using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;

[PrimaryKey(nameof(productId),nameof(userId))]
public class WishList
{
    [ForeignKey("product")]
    public int productId { get; set; }
    [ForeignKey("user")]
    public string userId { get; set; }
    public User user { get; set; }
    public Product product { get; set; }
}