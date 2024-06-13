using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.EAVProducts;
using Project.BL.Dtos.Values;
using Project.BL.Mappers.ValuesMapper;
using Project.DAL.Models;

namespace Project.BL.Mappers.EAVProductsMapper;
public class EAVProductsMapper : IEAVProductsMapper
{
    private readonly IValuesMapper _valueMapper;

    public EAVProductsMapper(IValuesMapper valuesMapper)
    {
        _valueMapper = valuesMapper;
    }

    public IEnumerable<AttributeWithValuesReadDTO> EavOneProductList(IEnumerable<EAVProducts> modelList)
    {
        IEnumerable<int> productsIds = modelList
            .DistinctBy(m => m.productId)
            .Select(m => m.productId);

      return productsIds.Select(p =>
        {
            IEnumerable<Attributes> atrributeProduct = modelList.Where(m => m.productId == p).Select(m => m.value.attribute);
            IEnumerable<Values> valuesProduct = modelList.Where(m => m.productId == p).Select(m => m.value);
          return EAVOneProduct(p, atrributeProduct, valuesProduct);
        });
    }


    //public IEnumerable<AttributeWithValuesReadDTO> EavOneProductList(IEnumerable<EAVProducts> modelList)
    //{
    //    IEnumerable<Attributes> attributeIds = modelList
    //        .DistinctBy(m => m.value.attributeId)
    //        .Select(m => m.value.attribute);

    //    return attributeIds
    //        .Select(a =>EAVOneProduct(a, modelList
    //        .Where(m => m.value.attributeId == a.Id)
    //        .Select(m => m.value)));

    //}

    private AttributeWithValuesReadDTO EAVOneProduct(int productId,IEnumerable<Attributes> attribute, IEnumerable<Values> value)
    {
       IEnumerable<AttributesReadDTO> attributeDTO = attribute.Select(a => new AttributesReadDTO(a.Id, a.Name));
       IEnumerable<ValuesReadDTO> valuesdto = _valueMapper.modelToReadList(value);
       return new AttributeWithValuesReadDTO(productId,attributeDTO, valuesdto);
    }

    public EAVProducts insertToModel(int valueId, int groupId, int productId)
    {
        return new EAVProducts()
        {
            variantGroupId = groupId,
            productId = productId,
            valueId = valueId
        };
    }

    public IEnumerable<EAVProducts> insertToModelList(IEnumerable<int> valuesIds, int groupId, int productId)
    {
        return valuesIds.Select(v => insertToModel(v,groupId,productId));
    }

    public EAVReadDTO modelToRead(EAVProducts model)
    {
        return new EAVReadDTO(
               model.productId,
               model.valueId,
               model?.value?.Name,
               model?.variantGroupId,
               model?.value?.attributeId,
               model?.value?.attribute.Name
            );
    }

    public IEnumerable<EAVReadDTO> modelToReadList(IEnumerable<EAVProducts> modelList)
    {
        return modelList.Select(eav => modelToRead(eav));
    }

}
