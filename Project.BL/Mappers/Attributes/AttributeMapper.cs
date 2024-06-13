using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Values;
using Project.BL.Mappers.ValuesMapper;
using Project.DAL.Models;

namespace Project.BL.Mappers.AttributesMapper;
public class AttributeMapper : IAttributeMapper
{
    private readonly IValuesMapper _valuesMapper;

    public AttributeMapper(IValuesMapper valuesMapper)
    {
        _valuesMapper = valuesMapper;
    }
    public Attributes insertToModel(AttributeInsertDTO insert)
    {
        return new Attributes()
        {
            Name = insert.Name
        };
    }

    public AttributesReadDTO modelToRead(Attributes model)
    {
        return new AttributesReadDTO(
            model.Id,
            model.Name
            );
    }

    public IEnumerable<AttributesReadDTO> modelToReadList(IEnumerable<Attributes> modelList)
    {
        return modelList.Select(m => modelToRead(m));
    }

    public AttributeWithitsValuesReadDTO modelToWithitsValuesReadDTO(Attributes model)
    {
        AttributesReadDTO attributesRead = modelToRead(model);
        IEnumerable<ValuesReadDTO> valuesReadDTOs = _valuesMapper.modelToReadList(model.values);
        return new AttributeWithitsValuesReadDTO(attributesRead, valuesReadDTOs);
    }

    public IEnumerable<AttributeWithitsValuesReadDTO> modelToWithitsValuesReadDTO(IEnumerable<Attributes> model)
    {
        return model.Select(m => modelToWithitsValuesReadDTO(m));
    }
}
