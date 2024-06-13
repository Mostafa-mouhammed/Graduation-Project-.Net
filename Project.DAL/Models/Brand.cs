using System.ComponentModel.DataAnnotations;

namespace Project.DAL.Models;
public class Brand
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string image { get; set; } = string.Empty;
    public bool isDeleted { get; set; } = false;
    public IEnumerable<Product> products { get; set; } = null!;
}