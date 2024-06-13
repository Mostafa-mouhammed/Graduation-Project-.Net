using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
[PrimaryKey(nameof(attributeId),nameof(variantGroupId))]
public class VariantGroupAttributes
{
    [ForeignKey("variantGroup")]
    public int variantGroupId { get; set; }
    [ForeignKey("attributes")]
    public int attributeId { get; set; }
    public VariantGroup variantGroup { get; set; }
    public Attributes attributes { get; set; }
}
