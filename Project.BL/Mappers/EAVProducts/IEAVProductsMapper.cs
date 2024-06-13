using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.EAVProducts;
using Project.DAL.Models;

namespace Project.BL.Mappers.EAVProductsMapper;
public interface IEAVProductsMapper
{
    EAVReadDTO modelToRead(EAVProducts model);
    IEnumerable<EAVReadDTO> modelToReadList(IEnumerable<EAVProducts> model);

    EAVProducts insertToModel(int valueId, int groupId, int productId);
    IEnumerable<EAVProducts> insertToModelList(IEnumerable<int> valuesIds, int groupId, int productId);
    IEnumerable<AttributeWithValuesReadDTO> EavOneProductList(IEnumerable<EAVProducts> modelList);
    
}
