using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.VariantGroup;
using Project.DAL.Models;

namespace Project.BL.Services.VaraityGroup;
public interface IVaraityGroupService
{
    Task<StatuscodeDTO> getAll();
    Task<StatuscodeDTO> getOne(int id);
    Task<StatuscodeDTO> getGroupAttributesValues(int id);
    Task<StatuscodeDTO> getOnebyName(string name);
    Task<StatuscodeDTO> addOne(VariantGroupInsertDto insert);
    Task<StatuscodeDTO> update(int Id, VariantGroupUpdate update);
    Task<StatuscodeDTO> delete(int id);
}
