using Project.BL.Dtos.ProductTypeAttribute;
using Project.DAL.Models;

namespace Project.BL.Mappers.PTA;
public class PTAMapper : IPTAMapper
{
    public DAL.Models.PTA insertTomodel(PTAInsertDto insert)
    {
        return new DAL.Models.PTA()
        {
            AttributeId = insert.AttributeId,
            ProductsTypesId = insert.ProductsTypesId
        };
    }
}
