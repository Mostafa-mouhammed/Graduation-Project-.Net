using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
[PrimaryKey(nameof(CategoryId), nameof(subCategoryId))]
public class CategoriesImages
{

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Category Category { get; set; } = null!;


    [ForeignKey("subcategory")]
    public int subCategoryId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public SubCategory subcategory { get; set; } = null!;

    public string imageURL { get; set; } 
}

