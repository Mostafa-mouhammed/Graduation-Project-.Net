using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project.DAL.Models;
public class Attributes
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public IEnumerable<Values> values { get; set; } = null!;
    public IEnumerable<VariantGroupAttributes> variantGroupAttributes { get; set; }

}
