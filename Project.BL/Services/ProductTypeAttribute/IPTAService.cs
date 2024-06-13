using Project.BL.Dtos.ProductTypeAttribute;
using Project.BL.Dtos.Statuscode;
using Project.DAL.Models;

namespace Project.BL.Services.ProductTypeAttributeService;
public interface IPTAService
{
    Task<StatuscodeDTO> getAllAtrributesbyType(int productType);
    Task<StatuscodeDTO> addPTA(PTAInsertDto insert);


}
