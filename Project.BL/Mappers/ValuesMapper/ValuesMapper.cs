using Project.BL.Dtos.Values;
using Project.DAL.Models;

namespace Project.BL.Mappers.ValuesMapper;
public class ValuesMapper : IValuesMapper
{
    public Values insertToModel(ValueInsertDTO insert)
    {
        return new Values()
        {
            Name = insert.Name,
            attributeId = insert.attributeId,
        };
    }

    public ValuesReadDTO modelToRead(Values model)
    {
        return new ValuesReadDTO(model.Id, model.Name,model.attributeId);
    }

    public IEnumerable<ValuesReadDTO> modelToReadList(IEnumerable<Values> modelList)
    {
        return  modelList.Select(m => modelToRead(m));
    }
}
