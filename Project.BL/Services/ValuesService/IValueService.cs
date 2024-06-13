using Project.BL.Dtos.Values;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.ValuesService;
public interface IValueService
{
    Task<StatuscodeDTO> GetAllValuess();
    Task<StatuscodeDTO> getValuesOfGroup(int Id);
    Task<StatuscodeDTO> AddValues(ValueInsertDTO insert);
    Task<StatuscodeDTO> updateValues(int Id, ValueUpdateDTO update);
    Task<StatuscodeDTO> deleteValues(int Id);
}
