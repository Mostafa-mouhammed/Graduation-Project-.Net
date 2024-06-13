using System.ComponentModel.DataAnnotations;

namespace Project.DAL.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string image { get; set; } = string.Empty;
    public bool isDeleted { get; set; } = false;
    public IEnumerable<SubCategory> subCategories { get; set; } = null!;
    public IEnumerable<CategoriesImages> categoriesImages { get; set; } = null!;
}
