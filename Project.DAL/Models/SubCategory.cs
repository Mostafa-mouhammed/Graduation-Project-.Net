using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
public class SubCategory
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
    [ForeignKey("category")]
    public int categoryId { get; set; }
    public Category category { get; set; } = null!;
    public IEnumerable<Product> Products { get; set; } = null!;
    public IEnumerable<SubCategoryImages> subCategoryImages { get; set; } = null!;
}
