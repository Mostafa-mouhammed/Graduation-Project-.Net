using Project.BL.Dtos.EAVProducts;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.EAVService;
public class EAVService : IEAVService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public EAVService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO>? addEAV(IEnumerable<int> valueList, int groupId, int productId)
    {
        if (valueList.Count() > 0)
            return  new StatuscodeDTO(Statuscode.BadRequest,"your items is empty");

        IEnumerable<EAVProducts> EAVProducts = _mapper.EAV.insertToModelList(valueList, groupId, productId);
        await _unit.EAV.AddRange(EAVProducts);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getOtherAttributesofProduct(int groupId)
    {
        IEnumerable<EAVProducts> eav = await _unit.EAV.getEAVbyGroupId(groupId);
        return new StatuscodeDTO(Statuscode.Ok, null, eav);
    }

    public async Task<StatuscodeDTO>? getProductIdbyValues(int groupId, IEnumerable<int> valueList)
    {
        int ProductId = 0;

        IEnumerable<EAVProducts> eav = await _unit.EAV.getEavbyGroupOnly(groupId);

        List<int> productsIds = eav.DistinctBy(eav => eav.productId).Select(eav => eav.productId).ToList();


        foreach (var id in productsIds)
        {
            IEnumerable<int> ProductValues = eav
                .Where(p => p.productId == id)
                .Select(p => p.valueId);

            /* if the values match a product assign it to the productId */
           bool isValuesMatchesProduct = valueList.All(v => ProductValues.Contains(v));
            if (isValuesMatchesProduct)
                ProductId = id;
        }
        if (ProductId == 0)
            return new StatuscodeDTO(Statuscode.NotFound, "there is no product match these values");

        return new StatuscodeDTO(Statuscode.Ok,null, ProductId);
    }
}
