namespace Project.DAL.Models;
public class VariantGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Product> varietyProducts { get; set; }
    public IEnumerable<VariantGroupAttributes> variantGroupAttributes { get; set; }

}
