namespace Project.DAL.DTOs.Products;
public class ProductQueryDTO()
{
    public int page { get; set; }
    public int limit { get; set; }
    public string sort { get; set; }
    public int subCategoryId { get; set; }
    public int brandId { get; set; }
    public string keyword { get; set; }
};