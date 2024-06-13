using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.DAL.Models;

public class User:IdentityUser
{
    [Required]
    [MinLength(2)]
    [MaxLength(25)]
    public string firstName { get; set; } = string.Empty;
    [Required]
    [MinLength(2)]
    [MaxLength(25)]
    public string lastName { get; set; } = string.Empty;
    public string fullName => $"{firstName} {lastName}";
    [MaxLength(400)]
    public string? address { get; set; } = string.Empty;
    public string image { get; set; } = "https://localhost:7173/images/default-user.jpg";
    public IEnumerable<Order> Orders { get; set; } = null!;
    public IEnumerable<Rating> rating { get; set; } = null!;
    public IEnumerable<WishList> wishList { get; set; } = null!;
    public Cart? Cart { get; set; } = null!;
}
