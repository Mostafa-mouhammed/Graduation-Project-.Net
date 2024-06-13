using Project.BL.Dtos.Values;
using Project.DAL.Models;

namespace Project.BL.Mappers.ValuesMapper;
public interface IValuesMapper
{
    ValuesReadDTO modelToRead(Values model);
    IEnumerable<ValuesReadDTO> modelToReadList(IEnumerable<Values> model);
    Values insertToModel(ValueInsertDTO insert);
}
