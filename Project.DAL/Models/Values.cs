using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
public class Values
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; 

    [ForeignKey("attribute")]
    public int attributeId { get; set; }
    public Attributes attribute { get; set; } = null!;

    public IEnumerable<EAVProducts> EAVProducts { get; set; } = null!;
}
