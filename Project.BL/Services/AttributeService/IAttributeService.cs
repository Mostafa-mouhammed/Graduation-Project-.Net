using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.AttributeService;
public interface IAttributeService 
{
    Task<StatuscodeDTO> GetAllAttributes();
    Task<StatuscodeDTO> AddAttribute(AttributeInsertDTO insert);
    Task<StatuscodeDTO> updateAttribute(int Id, AttributeUpdateDTO update);
    Task<StatuscodeDTO> deleteAttribute(int Id);
}
