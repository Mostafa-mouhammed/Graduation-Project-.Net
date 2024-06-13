using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Models;
[PrimaryKey(nameof(AttributeId),nameof(ProductsTypesId))]
public class PTA
{
    [ForeignKey("productsTypes")]
    public int ProductsTypesId { get; set; }
    [ForeignKey("attributes")]
    public int AttributeId { get; set; }
    public ProductsTypes productsTypes { get; set; }
    public Attributes attributes { get; set; }
}
