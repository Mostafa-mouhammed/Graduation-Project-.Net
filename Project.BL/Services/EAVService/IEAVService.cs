using Project.BL.Dtos.EAVProducts;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.EAVService;
public interface IEAVService
{
    Task<StatuscodeDTO> getOtherAttributesofProduct(int productId);
    Task<StatuscodeDTO>? addEAV(IEnumerable<int> valueList, int groupId, int productId);
    Task<StatuscodeDTO>? getProductIdbyValues(int groupId, IEnumerable<int> valueList);

}
