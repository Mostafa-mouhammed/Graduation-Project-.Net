using Project.BL.Dtos.ProductTypeAttribute;
using Project.DAL.Models;

namespace Project.BL.Mappers.PTA;
public interface IPTAMapper
{
    DAL.Models.PTA insertTomodel(PTAInsertDto insert);
}
